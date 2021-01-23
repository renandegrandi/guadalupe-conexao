using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Service;
using Guadalupe.Conexao.App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static Guadalupe.Conexao.App.Repository.DTO.NoticeDto;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class HomeViewModel : ViewModel, IDisposable
    {
        #region Fields

        private bool _isRefreshing;

        #endregion

        #region Dependencies

        private readonly INoticeRepository _noticeRepository;
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Properties
        public bool IsRefreshing { 
            get 
            { 
                return _isRefreshing; 
            } 
            private set 
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public IReadOnlyCollection<Notice> News { get; private set; }
        public ICommand RefreshNewsCommandAsync => new Command(async () =>
        {
            await UpdateAndRefreshViewCommandAsync()
                .ConfigureAwait(false);
        });
        public ICommand ImageTappedCommandAsync => new Command<Guid>(async (Guid notice) =>
        {
            try
            {
                await _navigation.PushModalAsync(new NoticeView(notice));
            }
            catch (Exception ex)
            {
                Log.Warning("HomeViewModel", ex.Message);
            }
        });
        #endregion

        #region Construtores
        public HomeViewModel(INavigation navigation, 
            INoticeRepository noticeRepository,
            ISessionService sessionService,
            IUserRepository userRepository,
            IPopupService popupService) : base(navigation, popupService)
        {
            _noticeRepository = noticeRepository;
            _sessionService = sessionService;
            _userRepository = userRepository;

            News = _noticeRepository.GetAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            UpdateAndRefreshViewCommandAsync()
                .SafeFireAndForget(false);
        }

        public void Dispose()
        {
            _cancellationToken.ThrowIfCancellationRequested();
        }
        #endregion

        #region Private Method

        private async Task UpdateAndRefreshViewCommandAsync() 
        {
            try
            {
                IsRefreshing = true;

                var user = _sessionService.GetUser();

                var newsUpdated = await ConexaoHttpClient.GetByDateAsync(user.NoticeLastUpdate, _cancellationToken);

                user.SetNoticeUpdated();

                await _userRepository.UpdateAsync(user);

                var idDeletedNews = newsUpdated.Where((n) => n.State.Equals(UserNoticeState.Removed))
                    .Select((n) => n.Id)
                    .ToArray();

                await _noticeRepository.RemoveAsync(idDeletedNews)
                    .ConfigureAwait(false);

                var idUpdatedNews = newsUpdated.Where((n) => n.State.Equals(UserNoticeState.Modified))
                    .Select((n) => n.Id)
                    .ToArray();

                var noticesToUpdated = await _noticeRepository.GetAsync(idUpdatedNews)
                    .ConfigureAwait(false);

                //TODO: Por enquanto não vou implementar as atualizações de noticias, não vamos ter atualização de feeds.

                var newNotices = newsUpdated
                    .Where((n) => !noticesToUpdated.Any((notice) => notice.Id == n.Id))
                    .ToList();

                var noticesToInclude = newNotices.Union(newsUpdated.Where((n) => n.State == UserNoticeState.Included))
                    .Select((n) => new Notice
                    {
                        Id = n.Id,
                        Posted = n.Posted,
                        Image = n.Image,
                        Message = n.Message,
                        IdPostedBy = n.IdPostedBy,
                        PostedBy = new Person(n.PostedBy.Id, n.PostedBy.Email, n.PostedBy.ProfileImage, n.PostedBy.Name)
                    })
                    .ToList();

                await _noticeRepository.InsertAsync(noticesToInclude)
                    .ConfigureAwait(false);

                News = await _noticeRepository.GetAsync()
                    .ConfigureAwait(false);

                OnPropertyChanged(nameof(this.News));
            }
            catch (UnauthorizedException) 
            {
                await _navigation.PushAsync(new AutenticationView());
            }
            catch (Exception ex)
            {
                Log.Warning(nameof(HomeViewModel), ex.Message);

                await _popupService.ShowErrorMessageAsync(ConexaoHttpClient.PrettyMessage);
            }

            IsRefreshing = false;
        }

        #endregion
    }
}
