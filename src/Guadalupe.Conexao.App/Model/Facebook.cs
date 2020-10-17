using System.ComponentModel;

namespace Guadalupe.Conexao.App.Model
{
    public class Facebook
    {

        #region Enum

        public enum Perfil
        {
            [Description("id")]
            Id,
            [Description("ids_for_business")]
            IdBusiness,
            [Description("name")]
            Name,
            [Description("name_format")]
            NameFormat,
            [Description("first_name")]
            FirstName,
            [Description("middle_name")]
            MiddleName,
            [Description("short_name")]
            ShortName,
            [Description("last_name")]
            LastName,
            [Description("picture")]
            Picture
        }

        public enum Permissions 
        {
            [Description("user_age_range")]
            UserAgeRange,
            [Description("user_birthday")]
            UserBirthday,
            [Description("user_friends")]
            UserFriends,
            [Description("user_gender")]
            UserGender,
            [Description("user_hometown")]
            UserHometown,
            [Description("email")]
            Email,
        }

        #endregion
    }
}
