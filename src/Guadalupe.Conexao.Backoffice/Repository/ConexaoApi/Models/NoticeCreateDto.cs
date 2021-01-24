namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models
{
    public class NoticeCreateDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public bool SendNotification { get; set; }
    }
}
