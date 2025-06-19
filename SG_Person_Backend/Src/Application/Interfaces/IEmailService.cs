namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task SendConfirmationEmailAsync(string email, string callbackUrl);
        Task SendPasswordResetEmailAsync(string email, string callbackUrl);
    }
}
