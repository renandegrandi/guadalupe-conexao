using Guadalupe.Conexao.Api.Core;
using System;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class PersonDto : IDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
