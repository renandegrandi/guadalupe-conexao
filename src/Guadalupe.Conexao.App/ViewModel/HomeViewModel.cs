using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
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
        private readonly CancellationToken _cancelationToken;

        #endregion

        #region Properties
        public DateTime? LastUpdate { get; set; }
        public string Message { get; set; }
        public bool IsRefreshing { get; private set; }
        public IReadOnlyCollection<Notice> News { get; private set; }
        public ICommand RefreshNewsCommandAsync => new Command(async () =>
        {
            await UpdateAndRefreshViewCommandAsync();

            IsRefreshing = false;
            this.OnPropertyChanged(nameof(IsRefreshing));
        });
        
        #endregion

        #region Construtores
        public HomeViewModel(INavigation navigation, INoticeRepository noticeRepository) : base(navigation)
        {
            _cancelationToken = new CancellationToken();
            _noticeRepository = noticeRepository;

            News = _noticeRepository.GetAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            UpdateAndRefreshViewCommandAsync()
                .SafeFireAndForget(false);
        }

        public void Dispose()
        {
            _cancelationToken.ThrowIfCancellationRequested();
        }
        #endregion

        #region Private Method

        private async Task UpdateAndRefreshViewCommandAsync() 
        {
            try
            {
                var newsUpdated = await _noticeRepository.GetByDateAsync(this.LastUpdate, _cancelationToken);

                var idDeletedNews = newsUpdated.Where((n) => n.State.Equals(UserNoticeState.Removed))
                    .Select((n) => n.Id)
                    .ToArray();

                await _noticeRepository.RemoveAsync(idDeletedNews);

                var idUpdatedNews = newsUpdated.Where((n) => n.State.Equals(UserNoticeState.Modified))
                    .Select((n) => n.Id)
                    .ToArray();

                var noticesToUpdated = await _noticeRepository.GetAsync(idUpdatedNews);

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
                            Image = "",
                            Name = n.PostedBy.Name
                        }
                    })
                    .ToList();

                await _noticeRepository.InsertAsync(noticesToInclude);

                News = await _noticeRepository.GetAsync();

                OnPropertyChanged(nameof(this.News));
            }
            catch (Exception)
            {
                this.Message = "Falha para carregaro aplicativo!";
                OnPropertyChanged(nameof(this.Message));
            }
        }

        #endregion

    }
}
