using Guadalupe.Conexao.Api.Core.DomainObject;
using System;

namespace Guadalupe.Conexao.Api.Domain
{
    public class User : Entity, IAggregateRoot
    {
        #region Propriedades

        public Guid IdPerson { get; set; }
        public Person Person { get; private set; }
        public string CodeAccess { get; private set; }
        public string RefreshToken { get; private set; }

        #endregion

        #region Constructor

        private User() : base() { }

        public User(Person person) : this()
        {
            Person = person;
            IdPerson = person.Id;
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
