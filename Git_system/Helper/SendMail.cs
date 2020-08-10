using System;
using System.Net.Mail;
using System.Threading;

namespace Git_system.Helper
{
    public class SendMail
    {
        private string smtp_Host = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Host"];
        private string smtp_User = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_User"];
        private string smtp_Password = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Password"];
        private short smtp_Port = Convert.ToInt16(System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Port"]);
        private bool smtp_EnableSsl = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_EnableSsl"]);

        private string emailName = System.Web.Configuration.WebConfigurationManager.AppSettings["email_Name"];

        public void fSendMail(string toMail, string subjectMail, string bodyMail)
        {
            string Subject = subjectMail;
            string Body = bodyMail.ToString();
            string ToEmail = toMail;

            string SMTPUser = smtp_User, SMTPPassword = smtp_Password;

            var mainHtml = @"
            <?xml version=""1.0"" encoding=""utf-8""?>
            <!DOCTYPE html>
            <html>
            <head>
            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
            </head>
            <body>
                {0}
            </body>
            </html>
            ";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SMTPUser, emailName);
            mail.To.Add(ToEmail);
            mail.Subject = Subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = string.Format(mainHtml, Body).Replace("\r\n", "").Replace("\t", "").Replace("    ", "");
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtp_Host;
            smtp.Port = smtp_Port;
            smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            smtp.EnableSsl = smtp_EnableSsl;

            smtp.Send(mail);
        }

        public void SendToMail(string toMail, string subjectMail, string bodyMail)
        {
            Thread thread = new Thread(new ThreadStart(() => fSendMail(toMail, subjectMail, bodyMail)));
            thread.Start();
        }
    }
}