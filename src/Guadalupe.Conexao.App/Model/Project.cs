using System;

namespace Guadalupe.Conexao.App.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Contact { get; set; } = "17991415357";
    }
}
