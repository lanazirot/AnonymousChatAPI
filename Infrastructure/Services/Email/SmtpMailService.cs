using Application.Interfaces.Email;
using Application.Models.AWS;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Infrastructure.Services.Email {
    public class SmtpMailService : IMailService {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        private readonly SMTPKeys _smtpKeys;
        private readonly SupportMailEntity _supportMail;

        public SmtpMailService(IOptions<SMTPKeys> smtpKeys, IOptions<SupportMailEntity> supportEmails) {
            _smtpKeys = smtpKeys.Value;
            _supportMail = supportEmails.Value;

            _smtpServer = _smtpKeys.SMTPHost!;
            _smtpPort = _smtpKeys.SMTPPort;
            _smtpUsername = _smtpKeys.SMTPUsername!;
            _smtpPassword = _smtpKeys.SMTPPassword!;
        }

        public void SendSupportMail(string subject, string body) {
            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;



            var mailMessage = new MailMessage {
                From = new MailAddress(_smtpUsername)
            };
            mailMessage.To.Add(_supportMail.SupportMail!);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            client.Send(mailMessage);
        }
    }
}
