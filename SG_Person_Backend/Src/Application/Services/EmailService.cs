using Microsoft.Extensions.Options;
using SG_Person_Backend.Src.Application.Interfaces;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SendGrid;

using SendGrid.Helpers.Mail;

using System.Net.Mail;

namespace SG_Person_Backend.Src.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (_emailSettings.UseSendGrid)
            {
                await SendWithSendGrid(email, subject, htmlMessage);
            }
            else
            {
                await SendWithSMTP(email, subject, htmlMessage);
            }
        }

        public async Task SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            var subject = "Confirme seu email";
            var htmlMessage = $"Por favor, confirme sua conta <a href='{callbackUrl}'>clicando aqui</a>.";
            await SendEmailAsync(email, subject, htmlMessage);
        }

        public async Task SendPasswordResetEmailAsync(string email, string callbackUrl)
        {
            var subject = "Redefina sua senha";
            var htmlMessage = $"Redefina sua senha <a href='{callbackUrl}'>clicando aqui</a>.";
            await SendEmailAsync(email, subject, htmlMessage);
        }

        private async Task SendWithSendGrid(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_emailSettings.SendGridKey);
            var from = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Falha ao enviar email via SendGrid: {StatusCode}", response.StatusCode);
            }
        }

        private async Task SendWithSMTP(string email, string subject, string htmlMessage)
        {
            // Implementação alternativa com SMTP padrão
            // Pode usar MailKit ou System.Net.Mail
            _logger.LogInformation("Simulando envio de email para {Email} com assunto {Subject}", email, subject);
            await Task.Delay(500); // Simula o envio
        }
    }

    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public bool UseSendGrid { get; set; }
        public string SendGridKey { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
