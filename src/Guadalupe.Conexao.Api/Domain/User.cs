using Guadalupe.Conexao.Api.Core.DomainObject;

namespace Guadalupe.Conexao.Api.Domain
{
    public class User : Person, IAggregationRoot
    {
        #region Constructor

        public User(string email) : base(email)
        {

        }

        #endregion
    }
}
