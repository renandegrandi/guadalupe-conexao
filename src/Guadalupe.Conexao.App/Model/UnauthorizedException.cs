using System;
using System.Collections.Generic;
using System.Text;

namespace Guadalupe.Conexao.App.Model
{
    class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }
    }
}
