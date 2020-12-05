using System;

namespace Guadalupe.Conexao.App.Model
{
    public class DomainException: Exception
    {
        public DomainException() : base () { }

        public DomainException(string message) : base(message) { }
    }
}
