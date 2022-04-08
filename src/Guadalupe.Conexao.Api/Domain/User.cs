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
        public string FCMToken { get; private set; }

        #endregion

        private User() { }
        
        public User(Person person) : this ()
        {
            Person = person;
            IdPerson = person.Id;
            SetCodeAccessIfNecessary();
        }

        public User(Person person, string codeAcess) : this(person)
        {
            CodeAccess = codeAcess;
        }

        private void SetCodeAccessIfNecessary() 
        {
            if (string.IsNullOrWhiteSpace(CodeAccess))
                CodeAccess = GenerateCodeAccess();
        }

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
        public User ChangeFcmToken(string token) 
        {
            FCMToken = token;
            return this;
        }

        #endregion
    }
}
