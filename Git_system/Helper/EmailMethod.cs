using Git_system.App_Code;
using Git_system.Helper.Email.ExtensionMethod;
using Git_system.Models;
using RazorEngine.Templating; // For extension methods.
using System;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Git_system.Helper
{
    public class EmailMethod : EmailMethodHelper
    {
        private static SendMail sm = new SendMail();
        private static Database_MainEntities1 db = new Database_MainEntities1();

        [Obsolete("used Helper check_culture")]
        private static string check_culture(string LanguageType, Membership membership)
        {
            return Models.Helper.check_culture(LanguageType, membership);
        }

        public static void SendEmailForRegister(Membership membership, string _LanguageType)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: membership);

            string key = Cryptography.SHA1(string.Concat(membership.Email, "3*as09-3=128098okmaeji0IIlo-"));
            var paramiterMail = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(membership.Email));

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + @"/home/ConfirmMembership?K=" + key + "&P=" + paramiterMail.ToString();
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(1).HtmlDecodeHtmlCode();

            string Subject, Body;
            if (culture.Equals("th"))
            {
                Subject = @emailMessage.TitleTH;
                Body = emailMessage.MessageTH.ToMessageEmailDecode(new Pay(), membership, culture: culture, urls: urllink);
            }
            else
            {
                Subject = @emailMessage.TitleEN;
                Body = emailMessage.MessageEN.ToMessageEmailDecode(new Pay(), membership, culture: culture, urls: urllink);
            }
            sm.SendToMail(@membership.Email.ToString(), @Subject.ToString(), @Body.ToString());
        }

        public static void SendEmailForPayment(Membership membership, string _LanguageType, Pay pay)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: membership);

            SendEmailForPayment(membership: membership, pay: pay, culture: culture);
        }

        private static void SendEmailForPayment(Membership membership, Pay pay, string culture)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(2).HtmlDecodeHtmlCode();

            string Subject = "";
            string mailBody = null;

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + @"/home/confirmpayment/" + pay.Id;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            if (culture.Equals("th"))
            {
                Subject = @emailMessage.TitleTH;
                mailBody = @emailMessage.MessageTH.ToMessageEmailDecode(pay, membership, culture: culture, urls: urllink);
            }
            else
            {
                Subject = @emailMessage.TitleEN;
                mailBody = @emailMessage.MessageEN.ToMessageEmailDecode(pay, membership, culture: culture, urls: urllink);
            }

            sm.SendToMail(@membership.Email.ToString(), @Subject.ToString(), @mailBody.ToString());
            membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(Subject); if (sender != null)sender.send(); });
        }

        public static void SendEmailForMembershipRegister(Membership membership, string _LanguageType, Pay pay)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: membership);

            SendEmailForMembershipRegister(membership: membership, pay: pay, culture: culture);
        }

        private static void SendEmailForMembershipRegister(Membership membership, Pay pay, string culture)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(3).HtmlDecodeHtmlCode();

            string Subject = "";
            string mailBody = null;

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + @"/home/confirmpayment/" + pay.Id;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            if (culture.Equals("th"))
            {
                Subject = @emailMessage.TitleTH;
                mailBody = @emailMessage.MessageTH.ToMessageEmailDecode(pay, membership, culture: culture, urls: urllink);
            }
            else
            {
                Subject = @emailMessage.TitleEN;
                mailBody = @emailMessage.MessageEN.ToMessageEmailDecode(pay, membership, culture: culture, urls: urllink);
            }

            sm.SendToMail(@membership.Email.ToString(), @Subject.ToString(), @mailBody.ToString());
            membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(Subject); if (sender != null)sender.send(); });
        }

        public static void SendEmailForResetPassword(Membership membership, string _LanguageType)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: membership);

            long date = System.DateTime.UtcNow.Ticks;
            long dateout = date / 10000000;//1 sec
            dateout = dateout / 3600; //60 min
            string User = membership.Id.ToString("X");
            string Key = dateout.ToString("X");
            string Token = Git_system.App_Code.Cryptography.SHA1(string.Concat(User, Key));
            string url = @"User=" + User + @"&Token=" + Token;

            //send mail
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(4).HtmlDecodeHtmlCode();

            string Subject = "";
            string mailBody = null;

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + @"/Home/ResetPassword?" + @url;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            if (culture.Equals("th"))
            {
                Subject = @emailMessage.TitleTH;
                mailBody = @emailMessage.MessageTH.ToMessageEmailDecode(new Pay(), membership, culture: culture, urls: urllink);
            }
            else
            {
                Subject = @emailMessage.TitleEN;
                mailBody = @emailMessage.MessageEN.ToMessageEmailDecode(new Pay(), membership, culture: culture, urls: urllink);
            }

            sm.SendToMail(@membership.Email.ToString(), @Subject.ToString(), @mailBody.ToString());
        }

        public static void SendEmailForResetPasswordBackend(UserAdmin useradmin)
        {
            long date = System.DateTime.UtcNow.Ticks;
            long dateout = date / 10000000;//1 sec
            dateout = dateout / 3600; //60 min
            string User = useradmin.Id.ToString("X");
            string Key = dateout.ToString("X");
            string Token = Git_system.App_Code.Cryptography.SHA1(string.Concat(User, Key));
            string url = @"User=" + User + @"&Token=" + Token;

            //send mail
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(4).HtmlDecodeHtmlCode();

            string Subject = "";

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + @"/Backend/Reset?" + @url;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            string emailMassageText = @"&lt;p&gt;กรุณาคลิก {urllink} เพื่อตั้ง รหัสผ่านใหม่&lt;/p&gt;

&lt;p&gt;หากไม่สามารถคลิก URL ข้างต้นได้ ให้สำเนา (Copy) URL ดังกล่าวแล้วนำไปวาง (Paste) ใน ช่อง Address ของ Web Browser&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;&amp;nbsp;&lt;/p&gt;

&lt;p&gt;-------------------------------------------------------------------------------&lt;/p&gt;

&lt;p&gt;สถาบันวิจัยและพัฒนาอัญมณีและเครื่องประดับแห่งชาติ (องค์การมหาชน)&lt;/p&gt;";

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.Replace("{urllink}", urllink);

            Subject = @emailMessage.TitleTH;

            sm.SendToMail(useradmin.Email.ToString(), @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailForMembershipRegisterToAdmin(Membership membership)
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(5)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.ToMessageEmailDecode(new Pay(), membership, culture: "th", urls: "");

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailForPaymentToAdmin(Membership membership, Pay pay)
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(6)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.ToMessageEmailDecode(pay, membership, culture: "th", urls: "");

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailForPaymentMembershipRegisterToAdmin(Membership membership, Pay pay)
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(7)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.ToMessageEmailDecode(pay, membership, culture: "th", urls: "");

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailForConfirmPaymentToAdmin(Membership membership, ConfirmPayment confirmPayment)
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(8)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            confirmPayment.ConfirmPaymentBankType = db.ConfirmPaymentBankTypes.Find(confirmPayment.ConfirmPaymentBankTypeId);
            emailMassageText = emailMassageText.ToMessageEmailDecode(confirmPayment, membership, culture: "th", urls: "");

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailWhenChangeCRM(Membership membership)
        {
            string cultureStr = membership.isLocal() ? "th" : "en-GB";
            string subject = "";

            ResourceManager resourceManager = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            subject = resourceManager.GetString("Information_of_membership", new System.Globalization.CultureInfo(cultureStr));

            string body = emailChangeCRM(membership, cultureStr);
            body = System.Net.WebUtility.HtmlDecode(body);
            sm.SendToMail(membership.Email, subject, @body.ToString());
        }

        private static string emailChangeCRM(Membership membership, string culture)
        {
            var razorBody = emailChangeCRM_body(membership, culture);
            return mainTemplateHtml(razorBody, culture);
        }

        private static string emailChangeCRM_body(Membership membership, string culture)
        {
            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());

            // body
            string body = @"
                <p><b>
                    @Multi.Information_of_membership
                </b></p>
                @Multi.IdCRM: @Model.IdCRM
                @Multi.Exp: @Model.ExpCRM
            ".Replace(
                "@Multi.Information_of_membership",
                rsm.GetString("Information_of_membership", cul)
            ).Replace(
                "@Multi.Exp",
                rsm.GetString("Membership_RegisterDateExp", cul)
            ).Replace(
                "@Multi.IdCRM",
                rsm.GetString("Membership_Id", cul)
            );

            string expDateTime = membership.ExpCRM != null ?
                System.Convert.ToDateTime(membership.ExpCRM).ToString("dd/MM/yyy") :
                rsm.GetString("not_defined", cul);

            var model = new { IdCRM = membership.IdCRM, ExpCRM = expDateTime };
            return RazorEngine.Engine.Razor.RunCompile(body, string.Concat(body, model.ToString()).GetHashCode().ToString("X"), null, model);
        }

        #region eCom

        /// <summary>
        /// Send email when membership pay.
        /// </summary>
        /// <param name="membership">The membership bought the item.</param>
        /// <param name="_LanguageType">Language of display</param>
        /// <param name="order">The Order bought by membership</param>
        /// <param name="orderDetail">Detail of the order bougth</param>
        public static void sendEmailForEComPayment(Membership membership, string _LanguageType, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: membership);

            sendEmailForEComPayment(membership: membership, culture: culture, order: order, orderDetail: orderDetail);
        }

        private static void sendEmailForEComPayment(Membership membership, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture = "th")
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Find(9).HtmlDecodeHtmlCode();

            string Subject = "";
            string mailBody = null;

            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string urlstring = Request_Url + "/Home/ProductConfirmPayment#/?OrderId=" + order.Id;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, urlstring);
            string urllink = String.Format(@"<a href=""{0}"">{0}</a>", fullUrl);

            if (culture.Equals("th"))
            {
                Subject = @emailMessage.TitleTH;
                mailBody = @emailMessage.MessageTH.toMessageEmailDecode(order, orderDetail, culture, urllink);
            }
            else
            {
                Subject = @emailMessage.TitleEN;
                mailBody = @emailMessage.MessageEN.toMessageEmailDecode(order, orderDetail, culture, urllink);
            }

            sm.SendToMail(membership.Email.ToString(), Subject.ToString(), mailBody.ToString());
            membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(Subject); if (sender != null)sender.send(); });
        }

        public static void sendEmailForEComDeliverSent(EComOrder order)
        {
            string culture = Models.Helper.check_culture(order.Membership);

            sendEmailForEComDeliverSent(membership: order.Membership, culture: culture, order: order);
        }

        private static void sendEmailForEComDeliverSent(Membership membership, EComOrder order, string culture = "th")
        {
            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            string body = @"

            <div>
              <strong>@Multi.Shipment_send_success</strong>
              <br><br>

              <p>@Multi.Order_number : <strong>@order.Id</strong></p>

              <p>@Multi._HomeString8 : <strong>@order.Datetime</strong></p>

              <strong>@Multi.OrderList</strong>
              <br><br>
              <br><br>
              @order.toOrderDetailTable(culture)
              {{getOrderDetailTable}}

              <div style='width:100%; clear: both; overflow: hidden;'>
              <p>
                <b>@Multi.Shipment_number</b>
                <br>
                @order.DeliverNumber
              </p>
              <p>
                <b>@Multi.Name_send</b>
                <br>
                @order.DeliverSendName
              </p>
              <p>
                <b>@Multi.Datetime_send</b>
                <br>
                @order.DeliverSendDate
              </p>
              </div>
              <br><br>
            </div>

            ".Replace(
                "@order.toOrderDetailTable(culture)",
                order.toOrderDetailTable(culture)
            ).Replace(
                "@order.Id",
                order.Id.ToString()
            ).Replace(
                "@order.Datetime",
                order.Datetime.toStringDateByCulture("HH:mm d MMM yyyy", culture)
            ).Replace(
                "@Multi._HomeString8",
                rsm.GetString("_HomeString8", cul)
            ).Replace(
                "@Multi.Order_number",
                rsm.GetString("Order_number", cul)
            ).Replace(
                "@order.DeliverNumber",
                order.DeliverNumber
            ).Replace(
                "@order.DeliverSendName",
                order.DeliverSendName
            ).Replace(
                "@order.DeliverSendDate",
                order.DeliverSendDate.GetValueOrDefault(DateTime.Now).toStringDateByCulture("HH:mm d MMM yyyy", culture)
            ).Replace(
                "@Multi.Shipment_send_success",
                rsm.GetString("Shipment_send_success", cul)
            ).Replace(
                "@Multi.OrderList",
                rsm.GetString("_HomeString1", cul)
            ).Replace(
                "@Multi.Shipment_number",
                rsm.GetString("Shipment_number", cul)
            ).Replace(
                "@Multi.Name_send",
                rsm.GetString("Name_send", cul)
            ).Replace(
                "@Multi.Datetime_send",
                rsm.GetString("Datetime_send", cul)
            ).Replace(
                "{{getOrderDetailTable}}",
                getOrderDetailTable(order, getOrderDetail(order), culture)
            );

            string emailHtml = System.Net.WebUtility.HtmlDecode(mainTemplateHtml(body, culture));

            // send mail.
            string cultureStr = membership.isLocal() ? "th" : "en-GB";
            string subject = "";

            ResourceManager resourceManager = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            subject = resourceManager.GetString("The_shipment_of_you", new System.Globalization.CultureInfo(cultureStr));

            sm.SendToMail(membership.Email, subject, emailHtml.ToString());
            membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(subject); if (sender != null)sender.send(); });
        }

        private static string getOrderDetailTable(EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture)
        {
            var str = @"
                <table align='right' border='0' cellpadding='0' cellspacing='0' style='width:100%'>
	                <tbody>
		                <tr>
			                <td>
			                <table align='right' border='0' cellpadding='0' cellspacing='0'>
				                <tbody>
					                <tr>
						                <td>@Multi.Delivery_cost&nbsp;<big><strong>{paymentDeliverPrice}</strong></big></td>
					                </tr>
				                </tbody>
			                </table>
			                </td>
		                </tr>
		                <tr>
			                <td>
			                <table align='right' border='0' cellpadding='0' cellspacing='0'>
				                <tbody>
					                <tr>
						                <td>@Multi._MembershipRegisterString9&nbsp;<big><strong>{paymentSumPrice}</strong></big></td>
					                </tr>
				                </tbody>
			                </table>
			                </td>
		                </tr>
		                <tr>
			                <td>
			                <table align='right' border='0' cellpadding='0' cellspacing='0'>
				                <tbody>
					                <tr>
						                <td>@Multi.Vat&nbsp;<big><strong>{paymentVatPrice}</strong></big></td>
					                </tr>
				                </tbody>
			                </table>
			                </td>
		                </tr>
		                <tr>
			                <td>
			                <table align='right' border='0' cellpadding='0' cellspacing='0'>
				                <tbody>
					                <tr>
						                <td>{order_promotiom_list}</td>
					                </tr>
				                </tbody>
			                </table>
			                </td>
		                </tr>
		                <tr>
			                <td>
			                <table align='right' border='0' cellpadding='0' cellspacing='0'>
				                <tbody>
					                <tr>
						                <td>@Multi._HomeString9 <big><strong>{paymentTotalPrice}</strong></big></td>
					                </tr>
				                </tbody>
			                </table>
			                </td>
		                </tr>
	                </tbody>
                </table>
                ";

            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            str = str.Replace(
                            "@Multi.Delivery_cost",
                            rsm.GetString("Delivery_cost", cul)
                        ).Replace(
                            "@Multi._MembershipRegisterString9",
                            rsm.GetString("_MembershipRegisterString9", cul)
                        ).Replace(
                            "@Multi.Vat",
                            rsm.GetString("Vat", cul)
                        ).Replace(
                            "@Multi._HomeString9",
                            rsm.GetString("_HomeString9", cul)
                        );
            str = str.toMessageOrderDetailDecode(order, orderDetail, culture);
            return str;
        }

        //
        //
        //
        //========================================================= sendEmailForEComConfirmOrderSuccess
        public static void sendEmailForEComConfirmOrderSuccess(EComOrder order)
        {
            string culture = Models.Helper.check_culture(order.Membership);

            sendEmailForEComConfirmOrderSuccess(membership: order.Membership, culture: culture, order: order);
        }

        private static void sendEmailForEComConfirmOrderSuccess(Membership membership, EComOrder order, string culture = "th")
        {
            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            string body = @"

            <div>
              <strong>@Multi.Order_your_payment_successful</strong>
              <br><br>

              <p>@Multi.Order_number : <strong>@order.Id</strong></p>

              <p>@Multi._HomeString8 : <strong>@order.Datetime</strong></p>

              <strong>@Multi.OrderList</strong>
              <br><br>
              @order.toEComPaymentListString(culture)
              {{getOrderDetailTable}}
            </div>

            ".Replace(
                "@order.toEComPaymentListString(culture)",
                order.toEComPaymentListString(culture)
            ).Replace(
                "@Multi.Order_your_payment_successful",
                rsm.GetString("Order_your_payment_successful", cul)
            ).Replace(
                "@Multi.OrderList",
                rsm.GetString("_HomeString1", cul)
            ).Replace(
                "@order.Id",
                order.Id.ToString()
            ).Replace(
                "@order.Datetime",
                order.Datetime.toStringDateByCulture("HH:mm d MMM yyyy", culture)
            ).Replace(
                "@Multi._HomeString8",
                rsm.GetString("_HomeString8", cul)
            ).Replace(
                "@Multi.Order_number",
                rsm.GetString("Order_number", cul)
            ).Replace(
                "@order.Price",
                Models.Helper.PriceAndCurrency(
                                order.Price,
                                order.Currency,
                                culture: culture,
                                sensitiveZero: true
                            )
            ).Replace(
                "@Multi.Total_price",
                rsm.GetString("Total_price", cul)
            ).Replace(
                "{{getOrderDetailTable}}",
                getOrderDetailTable(order, getOrderDetail(order), culture)
            );

            string emailHtml = System.Net.WebUtility.HtmlDecode(mainTemplateHtml(body, culture));

            // send mail.
            string cultureStr = membership.isLocal() ? "th" : "en-GB";
            string subject = rsm.GetString("Order_your_payment_successful", cul);

            ResourceManager resourceManager = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());
            subject = resourceManager.GetString("Order_your_payment_successful", new System.Globalization.CultureInfo(cultureStr));

            sm.SendToMail(membership.Email, subject, emailHtml.ToString());
            membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(subject); if (sender != null)sender.send(); });
        }

        private static Models.ECom.API.Process.OrderDetail getOrderDetail(EComOrder order)
        {
            Models.ECom.API.Process.OrderDetail orderDetail = new Models.ECom.API.Process.OrderDetail();
            if (order.JOrderDetail == "" || order.JOrderDetail == null)
                orderDetail = (new Git_system.Models.ECom.API.Process.Order()).checkOrderDetail(order.EComOrderItems.ToList(), order.MembershipId);
            else
                orderDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ECom.API.Process.OrderDetail>(order.JOrderDetail);
            return orderDetail;
        }

        public static void sendEmailForEComPaymentToAdmin(EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail = null)
        {
            if (orderDetail == null)
                orderDetail = getOrderDetail(order);

            sendEmailForEComPaymentToAdmin(membership: order.Membership, order: order, orderDetail: orderDetail);
        }

        private static void sendEmailForEComPaymentToAdmin(Membership membership, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture = "th")
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(10)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.toMessageEmailDecode(order, orderDetail, culture);

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void sendEmailForEComConfirmOrderToAdmin(EComOrder order)
        {
            var orderDetail = getOrderDetail(order);
            sendEmailForEComConfirmOrderToAdmin(membership: order.Membership, order: order, orderDetail: orderDetail);
        }

        private static void sendEmailForEComConfirmOrderToAdmin(Membership membership, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture = "th")
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(11)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH;

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.toMessageEmailDecode(order, orderDetail, culture);

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        public static void SendEmailForPaymentSuccess(Pay pay)
        {
            string culture = Models.Helper.check_culture(membership: pay.Membership);

            SendEmailForPaymentSuccess(pay: pay, culture: culture);
        }

        public static void SendEmailForPaymentSuccess(Pay pay, string _LanguageType)
        {
            string culture = Models.Helper.check_culture(LanguageType: _LanguageType, membership: pay.Membership);

            SendEmailForPaymentSuccess(pay: pay, culture: culture);
        }

        private static void SendEmailForPaymentSuccess(string culture, Pay pay)
        {
            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());

            string Subject = rsm.GetString("Order_your_payment_successful", cul);

            string body = @"

<p><strong><big>{fullname}</big></strong> @Multi.Order_your_payment_successful</p>

<p>@Multi.Order_number <big><strong>{order_id}</strong></big></p>

<p>@Multi._HomeString8<big><strong> {order_datetime}</strong></big></p>

<p>{paymentList}</p>

<table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:100%"">
	<tbody>
		<tr>
			<td>
			<table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"">
				<tbody>
					<tr>
						<td>@Multi._MembershipRegisterString9&nbsp;<big><strong>{paymentSumPrice}</strong></big></td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
		<tr>
			<td>
			<table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"">
				<tbody>
					<tr>
						<td>@Multi.Vat&nbsp;<big><strong>{paymentVatPrice}</strong></big></td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
		<tr>
			<td>
			<table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"">
				<tbody>
					<tr>
						<td align=""right"">{order_promotiom_list}</td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
		<tr>
			<td>
			<table align=""right"" border=""0"" cellpadding=""0"" cellspacing=""0"">
				<tbody>
					<tr>
						<td>@Multi.Total_price <big><strong>{paymentTotalPrice}</strong></big></td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
	</tbody>
</table>

".Replace(
                "@Multi.Order_your_payment_successful",
                rsm.GetString("Order_your_payment_successful", cul)
            ).Replace(
                "@Multi.OrderList",
                rsm.GetString("_HomeString1", cul)
            ).Replace(
                "@Multi._HomeString8",
                rsm.GetString("_HomeString8", cul)
            ).Replace(
                "@Multi.Order_number",
                rsm.GetString("Order_number", cul)
            ).Replace(
                "@Multi.Total_price",
                rsm.GetString("Total_price", cul)
).Replace(
                            "@Multi._MembershipRegisterString9",
                            rsm.GetString("_MembershipRegisterString9", cul)
                        ).Replace(
                            "@Multi.Vat",
                            rsm.GetString("Vat", cul)
                        ).Replace(
                            "@Multi._HomeString9",
                            rsm.GetString("_HomeString9", cul)
                        );

            string emailMassageText = System.Net.WebUtility.HtmlDecode(mainTemplateHtml(body, culture));

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.ToMessageEmailDecode(pay, pay.Membership, culture: culture);

            sm.SendToMail(pay.Membership.Email, @Subject.ToString(), @emailMassageText.ToString());
            pay.Membership.Devices.ToList().ForEach(d => { var sender = d.toDevice().getSender(Subject); if (sender != null)sender.send(); });
        }

        //
        //
        //
        // ------------------------------------------- SendEmailForPaymentSuccessToAdmin
        public static void SendEmailForPaymentSuccessToAdmin(Pay pay)
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(12)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH.Replace("{eComPaymentList}", "{paymentList}");
            emailMassageText = removeDomNodes(emailMassageText, "paymentDeliverPrice");

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.ToMessageEmailDecode(pay, pay.Membership, culture: "th");

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        //
        //
        //
        //========================================================= sendEmailForEComOrderSuccessToAdmin
        public static void sendEmailForEComOrderSuccessToAdmin(EComOrder order)
        {
            var orderDetail = getOrderDetail(order);
            sendEmailForEComOrderSuccessToAdmin(membership: order.Membership, order: order, orderDetail: orderDetail);
        }

        private static void sendEmailForEComOrderSuccessToAdmin(Membership membership, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture = "th")
        {
            //send mail

            EmailMessage emailMessage = new EmailMessage();
            emailMessage = db.EmailMessages.Where(e => e.Id.Equals(12)).FirstOrDefault().HtmlDecodeHtmlCode();
            string Subject = emailMessage.TitleTH;
            string emailMassageText = emailMessage.MessageTH.Replace("{paymentList}", "{eComPaymentList}");

            emailMassageText = System.Net.WebUtility.HtmlDecode(emailMassageText);
            emailMassageText = emailMassageText.toMessageEmailDecode(order, orderDetail, culture);

            sm.SendToMail(emailMessage.EmailAlert, @Subject.ToString(), @emailMassageText.ToString());
        }

        #endregion eCom

        private static string mainTemplateHtml(string body, string culture)
        {
            var cul = new System.Globalization.CultureInfo(culture);
            ResourceManager rsm = new ResourceManager("Git_system.MultiResources.Multi", Assembly.GetExecutingAssembly());

            string Request_Url = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, System.Web.HttpContext.Current.Request.Url.Authority);

            string htmlTemplate = string.Format(@"
                <html>
                    <head>
                    </head>
                    <body>
                        <table align='' border='0' cellpadding='0' cellspacing='0' style='width:100%'>
	                        <tbody>
		                        <tr>
			                        <td><img alt='' src='{0}/Images/GIT_Headder_New.png' style='float:right' /></td>
		                        </tr>
	                        </tbody>
                        </table>
                        <hr />

                        <div style='width:100%; padding:15px 0px; overflow: hidden;'>
                            @Model.body
                        </div>

                        <hr />
                        <p>@Multi.GemInstituteName</p>
                    </body>
                </html>", Request_Url).Replace(
                    "@Multi.GemInstituteName",
                    rsm.GetString("GemInstituteName", cul)
                );

            return RazorEngine.Engine.Razor.RunCompile(htmlTemplate, string.Concat(htmlTemplate, body).GetHashCode().ToString("X"), null, new { body = @body });
        }
    }
}