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
using static Guadalupe.Conexao.App.Repository.DTO.NoticeDto;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class HomeViewModel : ViewModel, IDisposable
    {
        #region Dependencies

        private readonly INoticeRepository _noticeRepository;
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Properties
        public string Message { get; private set; }
        public bool IsRefreshing { get; private set; }
        public IReadOnlyCollection<Notice> News { get; private set; }
        public ICommand RefreshNewsCommandAsync => new Command(async () =>
        {
            await UpdateAndRefreshViewCommandAsync()
                .ConfigureAwait(false);

            IsRefreshing = false;
            this.OnPropertyChanged(nameof(IsRefreshing));
        });
        
        #endregion

        #region Construtores
        public HomeViewModel(INavigation navigation, 
            INoticeRepository noticeRepository,
            ISessionService sessionService,
            IUserRepository userRepository) : base(navigation)
        {
            _noticeRepository = noticeRepository;
            _sessionService = sessionService;
            _userRepository = userRepository;

            News = _noticeRepository.GetAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            foreach (var notice in News.Where((a) => !string.IsNullOrEmpty(a.Image)))
            {
                notice.Image = $"{Configuration.Assets}{notice.Image}";
            }

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
                        PostedBy = new Person
                        {
                            Id = n.PostedBy.Id,
                            ProfileImage = "",
                            Name = n.PostedBy.Name
                        }
                    })
                    .ToList();

                await _noticeRepository.InsertAsync(noticesToInclude)
                    .ConfigureAwait(false);

                News = await _noticeRepository.GetAsync()
                    .ConfigureAwait(false);

                foreach (var notice in News.Where((a) => !string.IsNullOrEmpty(a.Image)))
                {
                    notice.Image = $"{Configuration.Assets}{notice.Image}";
                }

                OnPropertyChanged(nameof(this.News));
            }
            catch (UnauthorizedException) 
            {
                await _navigation.PushAsync(new AutenticationView());
            }
            catch (Exception ex)
            {
                this.Message = ConexaoHttpClient.PrettyMessage;
                OnPropertyChanged(nameof(this.Message));
            }
        }

        #endregion

    }
}
