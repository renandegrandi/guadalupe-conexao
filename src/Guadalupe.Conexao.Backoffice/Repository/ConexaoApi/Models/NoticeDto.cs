using System;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models
{
    public class NoticeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime Posted { get; set; }
    }
}
