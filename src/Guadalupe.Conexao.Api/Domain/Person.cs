using Guadalupe.Conexao.Api.Core.DomainObject;

namespace Guadalupe.Conexao.Api.Domain
{
    public class Person : Entity
    {
        #region Properties

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string ProfileImage { get; private set; }

        #endregion

        #region Constructors

        private Person() : base() { }

        public Person(string email) : this()
        {
            Email = email;
        }


        public Person(string email, string name) : this()
        {
            Email = email;
            Name = name;
        }
        

        #endregion

        public Person AdicionarProfileImage(string image) 
        {
            ProfileImage = image;

            return this;
        }
    }
}
