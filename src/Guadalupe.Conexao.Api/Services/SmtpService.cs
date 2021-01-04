using Guadalupe.Conexao.Api.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    sealed class SmtpService : ISmtpService
    {
        private readonly SmtpConfig _config;
        private readonly ILogger<SmtpService> _logger;

        public SmtpService(IOptions<SmtpConfig> config, 
            ILogger<SmtpService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task SendAsync(string email, string subject, string body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("É obrigatório informar o E-mail");

                if (string.IsNullOrWhiteSpace(subject))
                    throw new ArgumentException("É obrigatório informar o Titulo do E-Mail");

                if (string.IsNullOrWhiteSpace(body))
                    throw new ArgumentException("É obrigatório informar o Corpo do E-mail");

                var message = new MailMessage(new MailAddress(_config.FromEmail, _config.DisplayName, System.Text.Encoding.UTF8), new MailAddress(email));

                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient(_config.PrimaryDomain, _config.PrimaryPort)) 
                {
                    client.Credentials = new NetworkCredential(_config.Username, _config.Password);
                    client.EnableSsl = true;

                    await client.SendMailAsync(message)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
