using System;

namespace Guadalupe.Conexao.Api.Core.DomainObject
{
    public abstract class Entity
    {
        #region Properties

        public Guid Id { get; private set; }
        public DateTime Registration { get; private set; }
        public DateTime? Modification { get; private set; }
        public DateTime? Removal { get; private set; }

        #endregion

        #region Constructor

        protected Entity() : this (Guid.NewGuid())
        {

        }

        protected Entity(Guid id) 
        {
            Id = id;
            Registration = DateTime.Now;
        }

        #endregion

        protected void ModifyRegistrationDate(DateTime date)
        {
            Registration = date;
        }
    }
}
