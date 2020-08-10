using Git_system.Models;
using Git_system.Models.Form;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_EmailSettingController : BackendController
    {
        //
        // GET: /Backend_EmailSetting/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            Backend_EmailSettingModel Email = new Backend_EmailSettingModel();
            ViewBag.MailConnecting = Email.GetForWebconfig();

            Func<int, bool> checkInArray = te => Array.Exists(new int[] { 1, 2, 3, 4, 9 }, aex => (int)aex == (int)te);
            var emailMessages = db.EmailMessages.ToList().Where(e => checkInArray(e.Id));
            ViewBag.EmailMessage = emailMessages.HtmlDecode().ToArray();

            return View();
        }

        [HttpPost]
        [ActionName("Home")]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        public ActionResult Home(Backend_EmailSettingModel Email, EmailMessage[] emailMessages, string command)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            if (command == "SaveEmail")
            {
                try
                {
                    try
                    {
                        TestSendMail(Email.Host, Email.User, Email.Password, Email.Port, true);
                        Email.smtp_EnableSsl = true;
                        Email.SetToWebconfig();
                        Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขการตั้งค่าอีเมล", 1);//add to logfile
                    }
                    catch
                    {
                        TestSendMail(Email.Host, Email.User, Email.Password, Email.Port, false);
                        Email.smtp_EnableSsl = false;
                        Email.SetToWebconfig();
                        Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขการตั้งค่าอีเมล", 1);//add to logfile
                    }
                    return RedirectToAction("Home");
                }
                catch
                {
                    ModelState.AddModelError("EmailSetting", "การตั้งค่าอีเมลไม่ถูกต้อง");
                }
            }
            else if (command == "SaveMassage")
            {
                foreach (EmailMessage emailMessage in emailMessages)
                {
                    emailMessage.EmailAlert = "";
                    db.Entry(emailMessage.HtmlEncode()).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "ทำการบันทึกข้อความในการส่งอีเมล", 1);//add to logfile

                return RedirectToAction("Home");
            }

            ViewBag.MailConnecting = Email == null ? new Backend_EmailSettingModel().GetForWebconfig() : Email;
            ViewBag.EmailMessage = emailMessages == null ? db.EmailMessages.ToList().HtmlDecode().ToArray() : emailMessages;

            return View();
        }

        public ActionResult Admin()
        {
            Func<int, bool> checkInArray = te => Array.Exists(new int[] { 5, 6, 7, 8, 10, 11, 12 }, aex => (int)aex == (int)te);
            var emailMessages = db.EmailMessages.ToList().Where(e => checkInArray(e.Id)).HtmlDecode();
            ViewBag.EmailMessage = emailMessages.ToArray();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        public ActionResult Admin(EmailMessage[] emailMessages, string command)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);

            if (command == "SaveMassage")
            {
                foreach (EmailMessage emailMessage in emailMessages)
                {
                    emailMessage.TitleEN = emailMessage.TitleEN == null ? " " : emailMessage.TitleEN;
                    emailMessage.MessageEN = emailMessage.MessageEN == null ? " " : emailMessage.MessageEN;
                    db.Entry(emailMessage.HtmlEncode()).State = EntityState.Modified;
                    db.SaveChanges();
                }
                Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "ทำการบันทึกข้อความในการส่งอีเมลแจ้งเตือน", 1);//add to logfile

                return RedirectToAction("Admin");
            }

            ViewBag.EmailMessage = emailMessages;

            return View();
        }

        private void TestSendMail(string Host, string User, string Password, short Port, bool smtp_EnableSsl)
        {
            string Subject = "ทำการเปลี่ยน Mail สำเร็จ";
            string Body = "";
            string ToEmail = "mail@mail.com";

            string SMTPUser = User, SMTPPassword = Password;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(SMTPUser, "Update");
            mail.To.Add(ToEmail);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = Host;
            smtp.Port = Port;
            smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);
            smtp.EnableSsl = smtp_EnableSsl;

            smtp.Send(mail);
        }
    }
}