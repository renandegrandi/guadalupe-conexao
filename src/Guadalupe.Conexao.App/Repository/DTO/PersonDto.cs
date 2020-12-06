using System;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class PersonDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}
