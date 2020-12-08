using Guadalupe.Conexao.Api.Core.DomainObject;
using System;

namespace Guadalupe.Conexao.Api.Domain
{
    public class Notice : Entity, IAggregateRoot
    {
        #region Properties

        public string Message { get; private set; }
        public string Image { get; private set; }
        public Guid IdPostedBy { get; private set; }
        public Person PostedBy { get; private set; }

        #endregion

        #region Constructor

        private Notice() : base() { }

        public Notice(string message, Person postedBy) : this()
        {
            Message = message;
            PostedBy = postedBy;
            IdPostedBy = postedBy.Id;
        }

        public Notice(string message, string image, Person postedBy)  : this(message, postedBy)
        {
            Image = image;
        }

        #endregion

        public new Notice ModifyRegistrationDate(DateTime date) 
        {
            base.ModifyRegistrationDate(date);

            return this;
        }
    }
}
