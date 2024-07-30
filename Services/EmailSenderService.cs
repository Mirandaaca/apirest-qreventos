using Microsoft.IdentityModel.Tokens;
using QR___Evento.Interfaces.Services;
using System.Buffers.Text;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace QR___Evento.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly IFrom64BaseStringToImage _from64BaseStringToImage;
        public EmailSenderService(IConfiguration configuration, IFrom64BaseStringToImage from64BaseStringToImage)
        {
            _configuration = configuration;
            _from64BaseStringToImage = from64BaseStringToImage;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Leer el archivo de configuracion appsettings.json
            var emailConfig = _configuration.GetSection("EmailSettings");
            string fromEmail = emailConfig["Email"];
            string password = emailConfig["Password"];
            string host = emailConfig["Host"];
            int port = int.Parse(emailConfig["Port"]);
            bool enableSSL = bool.Parse(emailConfig["EnableSSL"]);
            // Set up SMTP client
            SmtpClient client = new SmtpClient(host, port)
            {
                EnableSsl = enableSSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, password)
            };
            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Tu código QR</h1>");
            mailBody.AppendFormat("<p>Adjunto encontrarás tu código QR.<p/>");
            mailBody.AppendFormat("<p>Guarda el código QR para el día del evento, no lo compartas con nadie.</p>");
            mailMessage.Body = mailBody.ToString();

            //Attach QR code
            mailMessage.Attachments.Add(new Attachment(_from64BaseStringToImage.ConvertToImage(htmlMessage), "QRCode.png", "image/png"));
            // Send email
            await client.SendMailAsync(mailMessage);
        }
    }
}
