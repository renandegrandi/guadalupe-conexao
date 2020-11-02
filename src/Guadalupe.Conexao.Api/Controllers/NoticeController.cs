using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guadalupe.Conexao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticeController : ControllerBase
    {
        #region Dependencies

        private readonly ILogger<NoticeController> _logger;

        #endregion

        #region Constructor

        public NoticeController(ILogger<NoticeController> logger)
        {
            _logger = logger;
        }

        #endregion
    }
}
