using Git_system.Models.ECom;
using HtmlAgilityPack;

// For extension methods.
using System.Linq;

namespace Git_system.Helper
{
    public class EmailMethodHelper
    {
        public static string removeDomNodes(string html, string selecter = "paymentDeliverPrice")
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            doc.DocumentNode.SelectNodes("//tr").Where(n =>
            {
                return n.InnerHtml.Contains("{" + selecter + "}");
            }).ToList().ForEach(n => n.Remove());

            return doc.DocumentNode.OuterHtml;
        }
    }
}

namespace Git_system.Helper.Email.ExtensionMethod
{
    using Git_system.Models;
    using System;
    using System.Collections.Generic;

    public class Table
    {
        public string Key { get; set; }

        public List<string> Value { get; set; }

        public string Align { get; set; }

        public Table()
        {
        }

        public Table(string key, List<string> value)
        {
            this.Key = key;
            this.Value = value;
        }

        public Table(string key, List<string> value, string align)
        {
            this.Key = key;
            this.Value = value;
            this.Align = align;
        }
    }

    public class TableBody
    {
        public string Name { get; set; }
        public Dictionary<string, string> Styles { get; set; }

        public TableBody()
        {
        }

        public TableBody(string name)
        {
            this.Name = name;
            setDefaultStyle();
        }

        public TableBody(string name, Dictionary<string, string> styles)
        {
            this.Name = name;
            setDefaultStyle();
            if (styles != null)
            {
                foreach (KeyValuePair<string, string> set_style in styles)
                {
                    if (this.Styles.ContainsKey(set_style.Key))
                        this.Styles[set_style.Key] = set_style.Value;
                    else
                        this.Styles.Add(set_style.Key, set_style.Value);
                }
            }
        }

        public string getStyles()
        {
            string styles = "";
            if (this.Styles != null)
                foreach (KeyValuePair<string, string> obj_style in this.Styles)
                {
                    string str_style = "";
                    str_style += string.Format("{0}: {1};",
                            obj_style.Key,
                            obj_style.Value);
                    styles += str_style + " ";
                }
            return styles;
        }

        private void setDefaultStyle()
        {
            this.Styles = new Dictionary<string, string>()
                {
                    {"border-left", "2px solid #ddd"},
                    {"border-right", "2px solid #ddd"},
                    {"border-collapse", "collapse"},
                    {"padding", "5px"}
                };
        }
    }

    public class Helper
    {
        public string createTable(params Table[] tableLists)
        {
            string table = "";
            table = "<table border='0' cellspacing='0' cellpadding='0' width='100%' style='border: 2px solid #ddd; border-collapse: collapse;  padding: 5px;'><thead style='background-color: #efefef; color: #2b3c94;'><tr>";
            foreach (Table tableList in tableLists)
            {
                table = table + "<th style='border: 2px solid #ddd; border-collapse: collapse; padding: 5px;'>" + tableList.Key + "</th>";
            }
            table = table + "</tr></thead><tbody>";

            for (int i = 0; i < tableLists.FirstOrDefault().Value.Count(); i++)
            {
                table = table + "<tr>";
                foreach (Table tableList in tableLists)
                {
                    table = table + "<td align='" + tableList.Align + "' style='border: 2px solid #ddd; border-collapse: collapse; padding: 5px;'>" + tableList.Value.ElementAt(i) + "</td>";
                }
                table = table + "</tr>";
            }
            table = table + "</tbody></table>";
            return table;
        }

        public string createTable(TableBody[] header, List<TableBody[]> bodies)
        {
            string table = "";
            table += "<table border='0' cellspacing='0' cellpadding='0' width='100%' style='border: 2px solid #ddd; border-collapse: collapse;  padding: 5px;'>";

            // Header
            string str_table_header = "<thead style='background-color: #efefef; color: #2b3c94;'><tr>";
            foreach (TableBody table_body in header)
            {
                str_table_header += string.Format("<th style='{0}'>{1}</th>", table_body.getStyles(), table_body.Name);
            }
            str_table_header += "</tr></thead>";

            // Body
            string str_table_bodies = "<tbody>";
            foreach (TableBody[] table_bodies in bodies)
            {
                str_table_bodies += "<tr>";
                foreach (TableBody table_body in table_bodies)
                {
                    str_table_bodies += string.Format("<td style='{0}'>{1}</td>", table_body.getStyles(), table_body.Name);
                }
                str_table_bodies += "</tr>";
            }
            str_table_bodies += "</tbody>";

            table += str_table_header + str_table_bodies;
            table += "</table>";
            return table;
        }

        public string add_strong_string(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : string.Format("<big><strong>{0}</strong></big>", str);
        }
    }

    public static class ExtensionMethod
    {
        private static string checkEmail(this string content, Membership membership)
        {
            return content.Replace("{email}", membership.Email);
        }

        private static string checkFullname(this string content, Membership membership)
        {
            return content.Replace("{fullname}", string.Format("{0} {1}", membership.Firstname, membership.Lastname));
        }

        private static CheckPayment checkPayment(this string content, Pay pay, Membership membership, params string[] urls)
        {
            CheckPayment checkPayment = new CheckPayment(content, pay, membership, urls);
            return checkPayment;
        }

        private class CheckPayment
        {
            public string content { get; set; }

            public Pay pay { get; set; }

            public Membership membership { get; set; }

            public string[] urls { get; set; }

            public CheckPayment()
            {
            }

            public CheckPayment(string content, Pay pay, Membership membership, params string[] urls)
            {
                this.content = content;
                this.pay = pay;
                this.membership = membership;
                this.urls = urls;
            }
        }

        private static string totalPrice(this CheckPayment checkPayment, string culture)
        {
            Pay pay = checkPayment.pay;
            return pay.Id != 0 ? checkPayment.content.Replace("{paymentTotalPrice}", Models.Helper.PriceAndCurrency(pay.Price, pay.Currency, true, culture)) : checkPayment.content;
        }

        private static string sumPrice(this CheckPayment checkPayment, string culture)
        {
            if (checkPayment.content.Contains("{paymentSumPrice}"))
            {
                var vat = Git_system.Models.Form.ExtensionMethod.getBackendVatSetting();
                var pay = checkPayment.pay;
                var sum_price = Controllers.FrontendController.getSumPriceForPayments(checkPayment.membership, pay.PayItems.ToList());
                return pay.Id != 0 ? checkPayment.content.Replace("{paymentSumPrice}", Models.Helper.PriceAndCurrency(sum_price, pay.Currency, true, culture)) : checkPayment.content;
            }
            else
                return checkPayment.content;
        }

        private static string vatPrice(this CheckPayment checkPayment, string culture)
        {
            var vat = Git_system.Models.Form.ExtensionMethod.getBackendVatSetting();
            var pay = checkPayment.pay;
            var vatPrice = Controllers.FrontendController.getVatForPayments(checkPayment.membership, pay.PayItems.ToList());
            return pay.Id != 0 ? checkPayment.content.Replace("{paymentVatPrice}", Models.Helper.PriceAndCurrency(vatPrice, pay.Currency, true, culture)) : checkPayment.content;
        }

        private static string order_datetime(this CheckPayment checkPayment, string culture)
        {
            Pay pay = checkPayment.pay;
            if (culture.Equals("th"))
                return pay.Id != 0 ? checkPayment.content.Replace("{order_datetime}", pay.Date.ToString("HH:mm d MMM yyyy", new System.Globalization.CultureInfo(culture))) : checkPayment.content;
            else
                return pay.Id != 0 ? checkPayment.content.Replace("{order_datetime}", pay.Date.ToString("hh:mm tt d MMM yyyy", new System.Globalization.CultureInfo(culture))) : checkPayment.content;
        }

        private static string order_id(this CheckPayment checkPayment)
        {
            Pay pay = checkPayment.pay;
            return pay.Id != 0 ? checkPayment.content.Replace("{order_id}", pay.Id.ToString()) : checkPayment.content;
        }

        private static string order_promotiom_list(this CheckPayment checkPayment, string culture)
        {
            if (checkPayment.pay.Id == 0 || checkPayment.membership.Id == 0)
                return checkPayment.content;
            List<Git_system.Models.Form.ProductSKUOut> skus = Controllers.FrontendController.check_promotion(checkPayment.membership.MembershipRegisterTypeId, checkPayment.pay.PayItems.ToList());
            Helper helper = new Helper();
            List<String> promotionList = new List<String>();
            foreach (Git_system.Models.Form.ProductSKUOut sku in skus)
            {
                if (culture.Equals("th"))
                    promotionList.Add(string.Format("<label>{0} : </label>{1}", sku.NameTH, helper.add_strong_string(Git_system.Models.Helper.PriceAndCurrency(sku.Price, checkPayment.pay.Currency, true, culture))));
                else
                    promotionList.Add(string.Format("<label>{0} : </label>{1}", sku.NameEN, helper.add_strong_string(Git_system.Models.Helper.PriceAndCurrency(sku.Price, checkPayment.pay.Currency, true, culture))));
            }
            return checkPayment.content.Replace("{order_promotiom_list}", string.Join("<br />", promotionList));
        }

        private static string paymentList(this CheckPayment checkPayment, string culture)
        {
            Pay pay = checkPayment.pay;
            string content = checkPayment.content;
            Membership membership = checkPayment.membership;

            if (content.Contains("{paymentList}"))
            {
                if (pay.Id != 0 && pay.Type == 2)
                {
                    string tablePay = "";
                    Helper helper = new Helper();

                    TableBody[] header ={
                        new TableBody(culture.Equals("th") ? "หลักสูตร" : "Course"),
                        new TableBody(culture.Equals("th") ? "สถานที่" : "Location"),
                        new TableBody(culture.Equals("th") ? "วัน" : "Duration"),
                        new TableBody(culture.Equals("th") ? "เวลา" : "Time"),
                        new TableBody(culture.Equals("th") ? "จำนวน" : "Quantity"),
                        new TableBody(culture.Equals("th") ? "ราคาต่อหน่วย" : "Normal Price")
                        };
                    List<TableBody[]> bodies = new List<TableBody[]>();
                    int productId_previous = 0;
                    foreach (PayItem payitem in pay.PayItems)
                    {
                        if (payitem.Course.ProductId != productId_previous)
                        {
                            productId_previous = payitem.Course.ProductId;
                            TableBody[] headerOfCourse ={
                                new TableBody(
                                    culture.Equals("th") ? payitem.Course.Product.TitleTH : payitem.Course.Product.TitleEN,
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"},
                                        {"font-weight", "bold"}
                                    }),
                                new TableBody("",
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"}
                                    }),
                                new TableBody("",
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"}
                                    }),
                                new TableBody("",
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"}
                                    }),
                                new TableBody("",
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"}
                                    }),
                                new TableBody("",
                                    new Dictionary<string,string>()
                                    {
                                        {"border-top", "2px solid #ddd"}
                                    })
                                };
                            bodies.Add(headerOfCourse);
                        }
                        TableBody[] body = {
                            new TableBody(
                                culture.Equals("th") ? payitem.Course.TitleTH : payitem.Course.TitleEN
                            ),
                            new TableBody(
                                culture.Equals("th") ? payitem.Course.Product.LocationTH : payitem.Course.Product.LocationEN
                            ),
                            new TableBody(
                                Models.Helper.DateToSting(
                                    System.Convert.ToDateTime(payitem.Course.DateStart),
                                    System.Convert.ToDateTime(payitem.Course.DateEnd),
                                    culture: culture
                                ),
                                new Dictionary<string,string>()
                                {
                                    {"text-align", "center"},
                                    {"white-space", "nowrap"}
                                }
                            ),
                            new TableBody(
                                culture.Equals("th") ?
                                    System.String.Format(
                                        "{0} - {1}",
                                        System.Convert.ToDateTime(payitem.Course.DateStart).ToString("HH:mm"),
                                        System.Convert.ToDateTime(payitem.Course.DateEnd).ToString("HH:mm")
                                    ) :
                                    System.String.Format(
                                        "{0} - {1}",
                                        System.Convert.ToDateTime(payitem.Course.DateStart).ToString("hh:mm tt"),
                                        System.Convert.ToDateTime(payitem.Course.DateEnd).ToString("hh:mm tt")
                                    ),
                                new Dictionary<string,string>()
                                {
                                    {"text-align", "center"},
                                    {"white-space", "nowrap"}
                                }
                            ),
                            new TableBody(
                                payitem.Quantity.ToString(),
                                new Dictionary<string,string>()
                                {
                                    {"text-align", "center"},
                                    {"white-space", "nowrap"}
                                }
                            ),
                            new TableBody(
                                helper.add_strong_string(
                                    Models.Helper.PriceAndCurrency(
                                        payitem.Course.Product.toTotalPriceForMembership(membership),
                                        pay.Currency,
                                        culture: culture
                                    )
                                ),
                                new Dictionary<string,string>()
                                {
                                    {"text-align", "right"},
                                    {"white-space", "nowrap"}
                                }
                            ),
                            };
                        bodies.Add(body);
                    }
                    tablePay = helper.createTable(header, bodies);
                    content = content.Replace("{paymentList}", tablePay);
                }
                else
                {
                    content = content.Replace("{paymentList}", string.Empty);
                }
            }
            return content;
        }

        private static string MembershipType(this CheckPayment checkPayment)
        {
            string content = checkPayment.content;
            if ((content.Contains("{paymentMembershipType}")) && checkPayment.pay != null)
            {
                Membership membership = checkPayment.membership;
                string membershipType = (membership.isLocal()) ? checkPayment.pay.PayItems.FirstOrDefault().Course.TitleTH : checkPayment.pay.PayItems.FirstOrDefault().Course.TitleEN;
                content = content.Replace("{paymentMembershipType}", membershipType);
            }

            return content;
        }

        private class CheckConfirmPayment
        {
            public string content { get; set; }

            public ConfirmPayment confirmPayment { get; set; }

            public Membership membership { get; set; }

            public string[] urls { get; set; }

            public CheckConfirmPayment()
            {
            }

            public CheckConfirmPayment(string content, ConfirmPayment confirmPayment, Membership membership, params string[] urls)
            {
                this.content = content;
                this.confirmPayment = confirmPayment;
                this.membership = membership;
                this.urls = urls;
            }
        }

        private static CheckConfirmPayment checkConfirmPayment(this string content, ConfirmPayment confirmPayment, Membership membership, params string[] urls)
        {
            CheckConfirmPayment checkConfirmPayment = new CheckConfirmPayment(content, confirmPayment, membership, urls);
            return checkConfirmPayment;
        }

        private static string confirmPrice(this CheckConfirmPayment checkConfirmPayment, string culture)
        {
            string content = checkConfirmPayment.content;
            content = content.Replace("{confirmPrice}", checkConfirmPayment.confirmPayment.Id != 0 ? Models.Helper.PriceAndCurrency(checkConfirmPayment.confirmPayment.Total, checkConfirmPayment.confirmPayment.Currency, culture: culture) : "");
            return content;
        }

        private static string confirmDate(this CheckConfirmPayment checkConfirmPayment)
        {
            string content = checkConfirmPayment.content;
            content = content.Replace("{confirmDate}", checkConfirmPayment.confirmPayment.Id != 0 ? checkConfirmPayment.confirmPayment.Date.ToString() : "");
            return content;
        }

        private static string confirmDetailList(this CheckConfirmPayment checkConfirmPayment, string culture)
        {
            string content = checkConfirmPayment.content;
            if (content.Contains("{confirmDetailList}") && checkConfirmPayment.confirmPayment != null)
            {
                List<ConfirmPayment> confirmPayments = new List<ConfirmPayment>();
                confirmPayments.Add(checkConfirmPayment.confirmPayment);
                string table = "";
                Helper helper = new Helper();
                table = helper.createTable(
                    new Table("ชื่อลูกค้า", confirmPayments.Select(s => s.Name.Replace(" ", "&nbsp;")).ToList()),
                    new Table("เวลาที่ยืนยัน", confirmPayments.Select(s => s.Date.ToString()).ToList(), "center"),
                    new Table("ราคาที่ยืนยัน", confirmPayments.Select(s => Models.Helper.PriceAndCurrency(s.Total, s.Currency, culture: culture, sensitiveZero: true)).ToList(), "right"),
                    new Table("ชื่อบัญชี", confirmPayments.Select(s => s.ConfirmPaymentBankType.NameTH.ToString()).ToList()),
                    new Table("ธนาคาร", confirmPayments.Select(s => s.ConfirmPaymentBankType.BanknameTH.ToString()).ToList(), "center"),
                    new Table("สาขา", confirmPayments.Select(s => s.ConfirmPaymentBankType.BranchTH.ToString()).ToList(), "center"),
                    new Table("เลขบัญชี", confirmPayments.Select(s => s.ConfirmPaymentBankType.AccountNo.ToString()).ToList(), "center"));
                content = content.Replace("{confirmDetailList}", table);
            }

            return content;
        }

        private static string bankList(this string content, Membership membership, string Culture)
        {
            if (content.Contains("{bankList}"))
            {
                Database_MainEntities1 db = new Database_MainEntities1();

                System.Collections.Generic.List<ConfirmPaymentBankType> confirmPaymentBankType = db.ConfirmPaymentBankTypes.ToList();
                if (Culture.Equals("th"))
                {
                    string table = "";
                    Helper helper = new Helper();
                    table = helper.createTable(
                        new Table("ชื่อบัญชี", confirmPaymentBankType.Select(s => s.NameTH.ToString()).ToList()),
                        new Table("ธนาคาร", confirmPaymentBankType.Select(s => s.BanknameTH.ToString()).ToList(), "center"),
                        new Table("สาขา", confirmPaymentBankType.Select(s => s.BranchTH.ToString()).ToList(), "center"),
                        new Table("เลขบัญชี", confirmPaymentBankType.Select(s => s.AccountNo.ToString()).ToList(), "center"));
                    content = content.Replace("{bankList}", table);
                }
                else
                {
                    string table = "";
                    Helper helper = new Helper();
                    table = helper.createTable(
                        new Table("Account name", confirmPaymentBankType.Select(s => s.NameEN.ToString()).ToList()),
                        new Table("Bank name", confirmPaymentBankType.Select(s => s.BanknameEN.ToString()).ToList(), "center"),
                        new Table("Branch", confirmPaymentBankType.Select(s => s.BranchEN.ToString()).ToList(), "center"),
                        new Table("Account no.", confirmPaymentBankType.Select(s => s.AccountNo.ToString()).ToList(), "center"));
                    content = content.Replace("{bankList}", table);
                }
            }

            return content;
        }

        private static string urllink(this string content, params string[] urls)
        {
            for (int i = 0; i < urls.Count(); i++)
            {
                content = content.Replace("{urllink" + i + "}", urls[i]);
            }
            return content;
        }

        public static string ToMessageEmailDecode(this string content, ConfirmPayment confirmPayment, Membership membership, string culture, params string[] urls)
        {
            content = content.checkConfirmPayment(confirmPayment, membership, urls).confirmPrice(culture);// {confirmPrice}
            content = content.checkConfirmPayment(confirmPayment, membership, urls).confirmDate();// {confirmDate}
            content = content.checkConfirmPayment(confirmPayment, membership, urls).confirmDetailList(culture);// {confirmDetailList}
            content = content.ToMessageEmailDecode(confirmPayment.Pay, membership, culture, urls);
            return content;
        }

        public static string ToMessageEmailDecode(this string content, Pay pay, Membership membership, string culture, params string[] urls)
        {
            content = content.checkEmail(membership);// {email}
            content = content.checkFullname(membership);// {fullname}
            content = content.checkPayment(pay, membership, urls).totalPrice(culture);// {paymentTotalPrice}
            content = content.checkPayment(pay, membership, urls).sumPrice(culture);// {paymentSumPrice}
            content = content.checkPayment(pay, membership, urls).vatPrice(culture);// {paymentVatPrice}
            content = content.checkPayment(pay, membership, urls).paymentList(culture);// {paymentList}
            content = content.checkPayment(pay, membership, urls).MembershipType();// {paymentMembershipType}
            content = content.bankList(membership, culture);// {bankList}
            content = content.urllink(urls);// {urllink0} {urllink1} ... {urllink2}
            content = content.checkPayment(pay, membership, urls).order_id();// {order_id}
            content = content.checkPayment(pay, membership, urls).order_datetime(culture);// {order_datetime}
            content = content.checkPayment(pay, membership, urls).order_promotiom_list(culture);// {order_promotiom_list}

            return content;
        }

        #region eComEmailDecode

        private static string dateToString(this DateTime date, string culture)
        {
            if (culture.Equals("th"))
                return date.ToString("HH:mm d MMM yyyy", new System.Globalization.CultureInfo(culture));
            else
                return date.ToString("hh:mm tt d MMM yyyy", new System.Globalization.CultureInfo(culture));
        }

        public static string toMessageEmailDecode(this string content, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture, params string[] urls)
        {
            Database_MainEntities1 db = new Database_MainEntities1();
            order.Membership = order.Membership ?? db.Memberships.Find(order.MembershipId);

            content = content.checkEmail(order.Membership);// {email}
            content = content.checkFullname(order.Membership);// {fullname}
            content = content.eComPaymentList(order, culture);// {eComPaymentList}

            //Add order detail
            content = content.toMessageOrderDetailDecode(order, orderDetail, culture);

            content = content.Replace("{order_id}", order.Id.ToString());// {order_id}
            content = content.Replace("{order_datetime}", order.Datetime.dateToString(culture));// {order_datetime}

            content = content.bankList(order.Membership, culture);// {bankList}
            content = content.urllink(urls);// {urllink0} {urllink1} ... {urllink2}
            return content;
        }

        private static string getPromotionListString(Models.ECom.API.Process.OrderDetail orderDetail, string culture)
        {
            List<string> promotions = orderDetail.PromotionDetail.ConvertAll(c => string.Format("{0}&nbsp;<big><strong>{1}</strong></big>", culture == "th" ? c.NameTH : c.NameEN, Models.Helper.PriceAndCurrency(
                                c.Price,
                                orderDetail.DeliverCurrency,
                                culture: culture,
                                sensitiveZero: true
                            )));
            var str = string.Join("<br />", promotions);
            return str;
        }

        public static string toMessageOrderDetailDecode(this string content, EComOrder order, Models.ECom.API.Process.OrderDetail orderDetail, string culture)
        {
            content = content.Replace("{order_promotiom_list}", getPromotionListString(orderDetail, culture));// {order_promotiom_list}

            content = content.Replace("{paymentTotalPrice}", Models.Helper.PriceAndCurrency((order.Id != 0) ? order.Price : orderDetail.summaryOrderTotalPrice(), orderDetail.DeliverCurrency, culture: culture, sensitiveZero: true));// {paymentTotalPrice}
            content = content.Replace("{paymentSumPrice}", Models.Helper.PriceAndCurrency(orderDetail.ProductPrice, orderDetail.DeliverCurrency, culture: culture, sensitiveZero: true));// {paymentSumPrice}
            content = content.Replace("{paymentVatPrice}", Models.Helper.PriceAndCurrency(orderDetail.ProductVat, orderDetail.DeliverCurrency, culture: culture, sensitiveZero: true));// {paymentVatPrice}
            content = content.Replace("{paymentDeliverPrice}", Models.Helper.PriceAndCurrency(orderDetail.DeliverPrice, orderDetail.DeliverCurrency, culture: culture, sensitiveZero: false));// {paymentDeliverPrice}
            return content;
        }

        private static string eComPaymentList(this string content, EComOrder order, string culture)
        {
            if (content.Contains("{eComPaymentList}"))
            {
                content = content.Replace("{eComPaymentList}", eComPaymentListString(order, culture));
            }
            return content;
        }

        public static string toEComPaymentListString(this EComOrder order, string culture)
        {
            return eComPaymentListString(order, culture);
        }

        private static string eComPaymentListString(EComOrder order, string culture)
        {
            string Request_Url = System.Web.HttpContext.Current.Request.Url.Authority;
            string fullUrl = String.Format("{0}://{1}", System.Web.HttpContext.Current.Request.Url.Scheme, Request_Url);

            Membership membership = order.Membership;

            string tablePay = "";
            Helper helper = new Helper();

            TableBody[] header ={
                        new TableBody(culture.Equals("th") ? "รหัสสินค้า" : "Number"),
                        new TableBody(culture.Equals("th") ? "ชื่อสินค้า" : "Name"),
                        new TableBody(culture.Equals("th") ? "จำนวน" : "Quantity"),
                        new TableBody(culture.Equals("th") ? "ราคาต่อหน่วย" : "Price per unit"),
                        new TableBody(culture.Equals("th") ? "ยอดรวม" : "Totoal")
                        };
            List<TableBody[]> bodies = new List<TableBody[]>();
            foreach (EComOrderItem item in order.EComOrderItems)
            {
                TableBody[] body = {
                        new TableBody(
                            string.Format(
                                @"<a href=""{1}/Home/ProductViewDetail#/?ProductId={0}"" target=""_blank"">{0}</a>",
                                item.EComProductId.ToString(),
                                fullUrl),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        ),
                        new TableBody(
                            string.Format(
                                @"<a href=""{0}/Home/ProductViewDetail#/?ProductId={1}"" target=""_blank"">{2}</a>",
                                fullUrl,
                                item.EComProductId.ToString(),
                                culture.Equals("th") ? item.EComProduct.NameTH : item.EComProduct.NameEN)
                        ),
                        new TableBody(
                            item.Quantity.ToString(),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        ),
                        new TableBody(
                            Models.Helper.PriceAndCurrency(
                                item.EComProduct.toTotalPriceForMembership(new Membership((short)membership.MembershipRegisterTypeId)),
                                order.Currency,
                                culture: culture,
                                sensitiveZero: true
                            ),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        ),
                        new TableBody(
                            Models.Helper.PriceAndCurrency(
                                (double)item.EComProduct.toTotalPriceForMembership(new Membership((short)membership.MembershipRegisterTypeId)) * (double)item.Quantity,
                                order.Currency,
                                culture: culture,
                                sensitiveZero: true
                            ),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        )
                        };
                bodies.Add(body);
            }
            tablePay = helper.createTable(header, bodies);
            return tablePay;
        }

        public static string toOrderDetailTable(this EComOrder eComOrder, string culture)
        {
            Membership membership = eComOrder.Membership;

            string tablePay = "";
            Helper helper = new Helper();

            TableBody[] header ={
                        new TableBody(culture.Equals("th") ? "รหัสสินค้า" : "Number"),
                        new TableBody(culture.Equals("th") ? "ชื่อสินค้า" : "Name"),
                        new TableBody(culture.Equals("th") ? "จำนวน" : "Quantity"),
                        };
            List<TableBody[]> bodies = new List<TableBody[]>();
            foreach (EComOrderItem item in eComOrder.EComOrderItems)
            {
                TableBody[] body = {
                        new TableBody(
                            item.EComProductId.ToString(),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        ),
                        new TableBody(
                            culture.Equals("th") ? item.EComProduct.NameTH : item.EComProduct.NameEN
                        ),
                        new TableBody(
                            item.Quantity.ToString(),
                            new Dictionary<string,string>()
                            {
                                {"text-align", "center"},
                                {"white-space", "nowrap"}
                            }
                        )
                        };
                bodies.Add(body);
            }
            tablePay = helper.createTable(header, bodies);
            return tablePay;
        }

        #endregion eComEmailDecode
    }
}