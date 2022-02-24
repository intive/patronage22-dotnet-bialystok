using Patronage.Contracts.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Patronage.DataAccess.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string email, string subject, string message)
        {
            string fromMail = "dotnetpatronage@gmail.com";
            string fromPassword = "zlhrnneygyzhyiib";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromMail);
            mail.Subject = subject;
            mail.To.Add(new MailAddress(email));
            mail.Body = "<html><body> " + message + " </body></html>";
            mail.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(mail);
        }
    }
}
