using System.Net;
using System.Net.Mail;
using System.Text;

namespace TeduShop.Common
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigHelper.GetByKey("SMTPHost");
                var port = int.Parse(ConfigHelper.GetByKey("SMTPPort"));
                var fromEmail = ConfigHelper.GetByKey("FromEmailAddress");
                var password = ConfigHelper.GetByKey("FromEmailPassword");
                var fromName = ConfigHelper.GetByKey("FromName");

                var smtp = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage()
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName),
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                mail.To.Add(new MailAddress(toEmail));

                smtp.Send(mail);
                return true;
            }
            catch (SmtpException ex)
            {
                throw;
            }
        }
    }
}