using Guadalupe.Conexao.Api.Core.DomainObject;

namespace Guadalupe.Conexao.Api.Domain
{
    public class User : Entity, IAggregateRoot
    {
        #region Propriedades

        public Person Person { get; private set; }
        public string CodeAccess { get; private set; }

        #endregion

        #region Constructor

        private User() : base() { }

        public User(Person person) : this()
        {
            Person = person;
            CodeAccess = "AAAA";
        }

        #endregion

        #region Public Method

        public User RegenerateCodeAccess() 
        {
            CodeAccess = "AAAB";

            return this;
        }

        #endregion
    }
}
