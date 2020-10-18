using System;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class NewDto
    {
        #region Properties

        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime Posted { get; set; }
        public UserDto PostedBy { get; set; }
        public States State { get; set; }

        #endregion

        #region Enums

        public enum States
        {
            Included,
            Modified,
            Removed
        }

        #endregion
    }
}
