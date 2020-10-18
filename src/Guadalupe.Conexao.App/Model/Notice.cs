using System;

namespace Guadalupe.Conexao.App.Model
{
    public class Notice
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public User PostedBy { get; set; }
    }
}
