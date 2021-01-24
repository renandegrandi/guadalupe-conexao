using Guadalupe.Conexao.Api.Core;
using System.ComponentModel.DataAnnotations;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class NoticeCreateDto: IDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Image { get; set; }
        public bool SendNotification { get; set; }
    }
}
