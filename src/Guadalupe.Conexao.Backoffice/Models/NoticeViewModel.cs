using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Guadalupe.Conexao.Backoffice.Models
{
    public class NoticeViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar uma mensagem")]
        public string Message { get; set; }

        [Required(ErrorMessage = "É obrigatório informar um titulo")]
        public string Title { get; set; }
        
        public string Image { get; set; }

        public bool SendNotification { get; set; }

        [Required(ErrorMessage = "É obrigatório vincular uma imagem!")]
        public IFormFile ImageFile { get; set; }
        
        public DateTime Posted { get; set; }
    }
}
