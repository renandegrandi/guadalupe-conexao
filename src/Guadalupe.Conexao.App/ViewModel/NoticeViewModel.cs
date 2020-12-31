using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using System;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    sealed class NoticeViewModel : ViewModel
    {
        #region Properties

        public Notice Notice { get; private set; }

        #endregion

        public NoticeViewModel(INavigation navigation, INoticeRepository noticeRepository, Guid notice) : base(navigation)
        {
            Notice = noticeRepository.GetAsync(notice)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
