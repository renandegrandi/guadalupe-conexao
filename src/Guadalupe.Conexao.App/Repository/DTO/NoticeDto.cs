using System;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class NoticeDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime Posted { get; set; }
        public Guid IdPostedBy { get; set; }
        public PersonDto PostedBy { get; set; }
        public UserNoticeState State { get; set; }

        #endregion

        #region Enums

        public enum UserNoticeState
        {
            Included,
            Modified,
            Removed
        }

        #endregion
    }
}
