using System;
using System.Collections.Generic;
using System.Text;

namespace Guadalupe.Conexao.App.Model
{
    public class DomainException: Exception
    {
        public DomainException() : base () { }

        public DomainException(string message) : base(message) { }
    }
}
