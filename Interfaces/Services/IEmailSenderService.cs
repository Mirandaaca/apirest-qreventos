namespace QR___Evento.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public Task SendEmailAsync (string email, string subject, string htmlMessage);
    }
}
