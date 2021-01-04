using Guadalupe.Conexao.Api.Core.DomainObject;
using System;
using System.Linq;

namespace Guadalupe.Conexao.Api.Domain
{
    public class User : Entity, IAggregateRoot
    {
        #region Constants

        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        #endregion

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
            CodeAccess = GenerateCodeAccess();
        }

        #endregion

        private string GenerateCodeAccess() 
        {
            return new string(Enumerable.Repeat(chars, 4)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        #region Public Method

        public User RegenerateCodeAccess() 
        {
            CodeAccess = GenerateCodeAccess();

            return this;
        }

        #endregion
    }
}
