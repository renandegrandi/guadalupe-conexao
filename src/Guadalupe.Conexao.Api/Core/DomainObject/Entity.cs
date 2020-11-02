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

        protected Entity()
        {
            Id = Guid.NewGuid();
            Registration = DateTime.Now;
        }

        #endregion
    }
}
