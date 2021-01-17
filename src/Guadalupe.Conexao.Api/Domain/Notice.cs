using Guadalupe.Conexao.Api.Core.DomainObject;
using System;

namespace Guadalupe.Conexao.Api.Domain
{
    public class Notice : Entity, IAggregateRoot
    {
        #region Properties

        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Image { get; private set; }
        public Guid IdPostedBy { get; private set; }
        public Person PostedBy { get; private set; }

        #endregion

        #region Constructor

        private Notice() : base() { }

        private Notice(Guid id) : base(id) { }

        public Notice(string title, string message) : this()
        {
            Title = title;
            Message = message;
        }

        public Notice(string title, string message, string image)  : this(title, message)
        {
            Image = image;
        }

        public Notice(Guid id, string title, string message, string image) : this(id)
        {
            Title = title;
            Message = message;
            Image = image;
        }

        #endregion

        public Notice AddPostedBy(Person postedBy) 
        {
            PostedBy = postedBy;
            IdPostedBy = postedBy.Id;

            return this;
        }

        public Notice AddImage(string image) 
        {
            Image = image;

            return this;
        }

        public new Notice ModifyRegistrationDate(DateTime date) 
        {
            base.ModifyRegistrationDate(date);

            return this;
        }
    }
}
