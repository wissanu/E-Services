using Git_system.Models.Form;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.Models
{
    public static partial class ExtensionMethod
    {
        public static EmailMessage HtmlEncode(this EmailMessage content)
        {
            content.MessageTH = System.Net.WebUtility.HtmlEncode(content.MessageTH);
            content.MessageEN = System.Net.WebUtility.HtmlEncode(content.MessageEN);
            return content;
        }

        public static EmailMessage HtmlDecode(this EmailMessage content)
        {
            content.MessageTH = System.Net.WebUtility.HtmlDecode(content.MessageTH).Replace("<br />", "\n");
            content.MessageEN = System.Net.WebUtility.HtmlDecode(content.MessageEN).Replace("<br />", "\n");
            return content;
        }

        public static EmailMessage HtmlDecodeHtmlCode(this EmailMessage content)
        {
            content.MessageTH = System.Net.WebUtility.HtmlDecode(content.MessageTH);
            content.MessageEN = System.Net.WebUtility.HtmlDecode(content.MessageEN);
            return content;
        }

        public static System.Collections.Generic.IEnumerable<EmailMessage> HtmlDecode(this System.Collections.Generic.IEnumerable<EmailMessage> contents)
        {
            foreach (var content in contents)
            {
                content.MessageTH = System.Net.WebUtility.HtmlDecode(content.MessageTH).Replace("<br />", "\n");
                content.MessageEN = System.Net.WebUtility.HtmlDecode(content.MessageEN).Replace("<br />", "\n");
            }
            return contents;
        }

        // TODO remove method
        public static Membership CheckMembershipType(this Membership contents)
        {
            if (contents == null)
            {
                Membership membership = new Membership();

                membership.MembershipRegisterTypeId = 1;
                return membership;
            }

            return CheckMembershipExp(contents);
        }

        public static Membership CheckMembershipType(this Membership contents, string __LanguageType = "TH")
        {
            if (contents == null)
            {
                Membership membership = new Membership();
                membership.MembershipRegisterTypeId = System.Convert.ToInt16(__LanguageType == "TH" ? 1 : 2);
                return membership;
            }

            return CheckMembershipExp(contents);
        }

        public static Membership CheckMembershipExp(this Membership content)
        {
            if (!content.isGuest())
            {
                bool membershipEXP = (content.RegisterDateExp <= System.DateTime.Now);
                if (membershipEXP)
                {
                    content.MembershipRegisterTypeId = content.isLocal() ? (Int16)1 : (Int16)2;
                }
            }
            return content;
        }

        public static bool isGuest(this Membership membership)
        {
            int[] memberhipTypeIdGuestArray = { 1, 2 };
            return memberhipTypeIdGuestArray.Contains(membership.MembershipRegisterTypeId);
        }

        public static bool isIndividual(this Membership membership)
        {
            int[] memberhipTypeIdIndividusllArray = { 3, 4 };
            return memberhipTypeIdIndividusllArray.Contains(membership.MembershipRegisterTypeId);
        }

        public static bool isCorporate(this Membership membership)
        {
            int[] memberhipTypeIdCorporateArray = { 5, 6 };
            return memberhipTypeIdCorporateArray.Contains(membership.MembershipRegisterTypeId);
        }

        /// <summary>
        /// Check membership is local.
        /// Ex.
        ///     If membership have membershipType 1
        ///     return true
        ///     If membership have membershipType 2
        ///     return false
        /// </summary>
        /// <param name="membership">Membership will check local status</param>
        /// <returns>isLocal for membership</returns>
        public static bool isLocal(this Membership membership)
        {
            int[] memberhipTypeIdlocalArray = { 1, 3, 5 };
            return memberhipTypeIdlocalArray.Contains(membership.MembershipRegisterTypeId);
        }

        public static Course ToCourse(this Form.Backend_Course content)
        {
            Course course = new Course();
            course.Id = content.Id;

            course.TitleTH = content.TitleTH;
            course.TitleEN = content.TitleEN;

            course.Active = content.Active;
            course.ProductId = content.ProductId;
            course.ProductSKUActive = content.ProductSKUActive;

            course.Product = content.Product;
            course.PayItems = content.PayItems;

            course.DateStart = System.DateTime.ParseExact(System.String.Format("{0:MM/dd/yyyy}", content.DateStart_Date) + " " + System.String.Format("{0:hh:mm tt}", content.DateStart_Time), "MM/dd/yyyy hh:mm tt", null);
            course.DateEnd = System.DateTime.ParseExact(System.String.Format("{0:MM/dd/yyyy}", content.DateEnd_Date) + " " + System.String.Format("{0:hh:mm tt}", content.DateEnd_Time), "MM/dd/yyyy hh:mm tt", null);
            return course;
        }

        public static Git_system.Models.Form.Backend_EmailSettingModel GetForWebconfig(this Git_system.Models.Form.Backend_EmailSettingModel useradmin)
        {
            string Host = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Host"];
            string User = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_User"];
            string Password = System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Password"];
            short Port = System.Convert.ToInt16(System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_Port"]);
            bool smtp_EnableSsl = System.Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["smtp_EnableSsl"]);

            Git_system.Models.Form.Backend_EmailSettingModel Email = new Git_system.Models.Form.Backend_EmailSettingModel();
            Email.Port = Port;
            Email.Host = Host;
            Email.Password = Password;
            Email.User = User;
            Email.smtp_EnableSsl = smtp_EnableSsl;

            return Email;
        }

        public static void SetToWebconfig(this Git_system.Models.Form.Backend_EmailSettingModel Email)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["smtp_Host"].Value = Email.Host;
            config.AppSettings.Settings["smtp_User"].Value = Email.User;
            config.AppSettings.Settings["smtp_Password"].Value = Email.Password;
            config.AppSettings.Settings["smtp_Port"].Value = Email.Port.ToString();
            config.AppSettings.Settings["smtp_EnableSsl"].Value = Email.smtp_EnableSsl.ToString();
            config.Save();
        }

        public static WebSettingConfig HtmlDecode(this WebSettingConfig content)
        {
            content.legalTH = content.legalTH.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalEN = content.legalEN.Replace("<br />", "\n").Replace("<br />", "\n");

            content.legalMembershipTH = content.legalMembershipTH.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalMembershipEN = content.legalMembershipEN.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalCourseTH = content.legalCourseTH.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalCourseEN = content.legalCourseEN.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalProductTH = content.legalProductTH.Replace("<br />", "\n").Replace("<br />", "\n");
            content.legalProductEN = content.legalProductEN.Replace("<br />", "\n").Replace("<br />", "\n");

            return content;
        }

        public static void Save(this WebSettingConfig content)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings["legalTH"].Value = System.Net.WebUtility.HtmlEncode(content.legalTH.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalEN"].Value = System.Net.WebUtility.HtmlEncode(content.legalEN.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));

            config.AppSettings.Settings["legalMembershipTH"].Value = System.Net.WebUtility.HtmlEncode(content.legalMembershipTH.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalMembershipEN"].Value = System.Net.WebUtility.HtmlEncode(content.legalMembershipEN.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalCourseTH"].Value = System.Net.WebUtility.HtmlEncode(content.legalCourseTH.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalCourseEN"].Value = System.Net.WebUtility.HtmlEncode(content.legalCourseEN.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalProductTH"].Value = System.Net.WebUtility.HtmlEncode(content.legalProductTH.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));
            config.AppSettings.Settings["legalProductEN"].Value = System.Net.WebUtility.HtmlEncode(content.legalProductEN.Replace("\n", "<br />").Replace("\r\n", "<br /><br />"));

            config.Save();
        }

        public static System.Collections.Generic.List<Geography> PlaningToJson(this System.Collections.Generic.List<Geography> items)
        {
            System.Collections.Generic.List<Geography> geographies = new System.Collections.Generic.List<Geography>();
            foreach (Geography item in items)
            {
                Geography geography = new Geography();
                geography.GEO_ID = item.GEO_ID;
                geography.GEO_NAME = item.GEO_NAME;
                geography.Provinces = null;
                geographies.Add(geography);
            }
            return geographies;
        }

        public static System.Collections.Generic.List<Province> PlaningToJson(this System.Collections.Generic.List<Province> items)
        {
            System.Collections.Generic.List<Province> provinces = new System.Collections.Generic.List<Province>();
            foreach (Province item in items)
            {
                Province province = new Province();
                province.PROVINCE_ID = item.PROVINCE_ID;
                province.PROVINCE_CODE = item.PROVINCE_CODE;
                province.PROVINCE_NAME = item.PROVINCE_NAME;
                province.GEO_ID = item.GEO_ID;
                province.Geography = null;
                province.Amphurs = null;
                provinces.Add(province);
            }
            return provinces;
        }

        public static System.Collections.Generic.List<Amphur> PlaningToJson(this System.Collections.Generic.List<Amphur> items)
        {
            System.Collections.Generic.List<Amphur> amphurs = new System.Collections.Generic.List<Amphur>();
            foreach (Amphur item in items)
            {
                Amphur amphur = new Amphur();
                amphur.AMPHUR_ID = item.AMPHUR_ID;
                amphur.AMPHUR_NAME = item.AMPHUR_NAME;
                amphur.POSTCODE = item.POSTCODE;
                amphur.PROVINCE_ID = item.PROVINCE_ID;
                amphur.GEO_ID = item.GEO_ID;
                amphur.AMPHUR_CODE = null;
                amphur.Districts = null;
                amphurs.Add(amphur);
            }
            return amphurs;
        }

        public static System.Collections.Generic.List<District> PlaningToJson(this System.Collections.Generic.List<District> items)
        {
            System.Collections.Generic.List<District> districts = new System.Collections.Generic.List<District>();
            foreach (District item in items)
            {
                District district = new District();
                district.AMPHUR_ID = item.AMPHUR_ID;
                district.DISTRICT_CODE = null;
                district.DISTRICT_ID = item.DISTRICT_ID;
                district.PROVINCE_ID = item.PROVINCE_ID;
                district.GEO_ID = item.GEO_ID;
                district.DISTRICT_NAME = item.DISTRICT_NAME;
                district.Amphur = null;
                districts.Add(district);
            }
            return districts;
        }

        public static System.Collections.Specialized.NameValueCollection ToNameValueCollection<tValue>(this System.Collections.Generic.IDictionary<string, tValue> dictionary)
        {
            var collection = new System.Collections.Specialized.NameValueCollection();
            foreach (var pair in dictionary)
                collection.Add(pair.Key, pair.Value.ToString());
            return collection;
        }

        public static Git_system.Models.Form.Backend_CreditCardSetting GetForCreditCardSetting()
        {
            bool Enable_CreditCard = System.Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["CreditCardSetting_Enable_CreditCard"]);

            Git_system.Models.Form.Backend_CreditCardSetting CreditCardSetting = new Git_system.Models.Form.Backend_CreditCardSetting();
            CreditCardSetting.Enable_CreditCard = Enable_CreditCard;

            return CreditCardSetting;
        }

        public static void SetToWebconfig(this Git_system.Models.Form.Backend_CreditCardSetting CreditCardSetting)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["CreditCardSetting_Enable_CreditCard"].Value = CreditCardSetting.Enable_CreditCard.ToString();
            config.Save();
        }

        public static string toPhoneString(this string phonenumber)
        {
            if (phonenumber == null || phonenumber == "")
                return phonenumber;
            else
                phonenumber = phonenumber.Trim();

            string number = "";
            char[] phoneNumberArray = phonenumber.ToCharArray();

            for (int i = 0; i < phoneNumberArray.Length; i++)
            {
                string NumberForIndex = null;

                if (i == 0)
                {
                    if (phoneNumberArray[i].ToString().Equals("0"))
                        NumberForIndex = "+66";
                }
                else
                    NumberForIndex = phoneNumberArray[i].ToString();

                number = string.Concat(number, NumberForIndex);
            }
            return number;
        }

        /// <summary>
        /// Calculator product prcie
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="membership">membership</param>
        /// <returns>price of membership</returns>
        private static double getPriceForMembership(this Product product, Membership membership)
        {
            double priceForMembership = 0;
            switch (membership.checkMembershipTypeReturnType())
            {
                case 1:
                    priceForMembership = product.Price;
                    break;

                case 2:
                    priceForMembership = product.PriceInter;
                    break;

                case 3:
                    priceForMembership = product.PriceIndividual;
                    break;

                case 4:
                    priceForMembership = product.PriceIndividualInter;
                    break;

                case 5:
                    priceForMembership = product.PriceCorporate;
                    break;

                case 6:
                    priceForMembership = product.PriceCorporateInter;
                    break;
            }
            return priceForMembership;
        }

        /// <summary>
        /// Calculator product prcie
        /// </summary>
        /// <param name="product">product</param>
        /// <param name="membership">membership</param>
        /// <returns>price of membership</returns>
        public static double toPriceForMembership(this Product product, Membership membership)
        {
            double price = 0;
            double setPrice = product.getPriceForMembership(membership);
            if (product.VatTypeId == 1)
            {
                double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
                price = setPrice / (1 + vat_setting / 100);
            }
            else if (product.VatTypeId == 2)
                price = setPrice;
            return price;
        }

        public static double toTotalPriceForMembership(this Product product, Membership membership)
        {
            double price = 0;

            double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
            price = product.toPriceForMembership(membership) * (1 + vat_setting / 100);

            return price;
        }

        public static double toVatPriceForMembership(this Product product, Membership membership)
        {
            double price = 0;
            double vat_setting = Form.ExtensionMethod.getBackendVatSetting();
            price = product.toPriceForMembership(membership) * (vat_setting / 100);

            return price;
        }

        /// <summary>
        /// Check type number of membership
        /// </summary>
        /// <param name="membership">membership</param>
        /// <returns>Type number of membership</returns>
        public static int checkMembershipTypeReturnType(this Membership membership)
        {
            return membership.CheckMembershipType().MembershipRegisterTypeId;
        }

        private static List<String> addStringWithPrefix(String p, String s, List<String> ls)
        {
            return (s != null && s != "") ? ls.AddToList(((p != null && p != "") ? p + "" : p) + s) : ls;
        }

        public static string getFullAddressWithPhoneAndEmail(this Membership membership)
        {
            Frontend_RegisterStep2 fr2Membership = membership.toFrontend_RegisterStep2();

            List<String> phoneList = new List<String>();
            addStringWithPrefix("", fr2Membership.TelNo, phoneList);
            addStringWithPrefix("", fr2Membership.MobileNo, phoneList);
            string phones = string.Join(", ", phoneList.ToArray());

            List<String> listAddress = new List<String>();
            addStringWithPrefix("", membership.getFullAddress(), listAddress);
            addStringWithPrefix("โทรศัพท์ ", phones, listAddress);
            addStringWithPrefix("อีเมล ", membership.Email, listAddress);

            return string.Join(" ", listAddress.ToArray());
        }

        public static string getFullAddress(this Membership membership)
        {
            List<String> listAddress = new List<String>();

            Frontend_RegisterStep2 fr2Membership = membership.toFrontend_RegisterStep2();
            if (fr2Membership.CountryName != null && fr2Membership.CountryName != "")
            {
                addStringWithPrefix("", fr2Membership.Address, listAddress);
                addStringWithPrefix("", fr2Membership.Road, listAddress);
                if (fr2Membership.CountryName.ToLower() == "thailand")
                {
                    if (fr2Membership.Province == 1)
                    {
                        addStringWithPrefix("แขวง", fr2Membership.DistrictName, listAddress);
                        addStringWithPrefix("", fr2Membership.AmphurName, listAddress);
                        addStringWithPrefix("", fr2Membership.ProvinceName, listAddress);
                    }
                    else
                    {
                        addStringWithPrefix("ตำบล", fr2Membership.DistrictName, listAddress);
                        addStringWithPrefix("อำเภอ", fr2Membership.AmphurName, listAddress);
                        addStringWithPrefix("จังหวัด", fr2Membership.ProvinceName, listAddress);
                    }
                }
                else
                {
                    addStringWithPrefix("", fr2Membership.City, listAddress);
                    addStringWithPrefix("", fr2Membership.State, listAddress);
                    addStringWithPrefix("", fr2Membership.CountryName, listAddress);
                }
            }
            //string.Join(" ", listAddress.ToArray());
            return string.Join(" ", listAddress.ToArray());
        }

        public static List<T> AddToList<T>(this List<T> li, T added)
        {
            if (added != null)
                li.Add(added);
            return li;
        }
    }

    public class WebSettingConfig
    {
        public WebSettingConfig()
        {
            this.legalTH = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalTH"]);
            this.legalEN = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalEN"]);
            this.legalMembershipTH = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalMembershipTH"]);
            this.legalMembershipEN = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalMembershipEN"]);
            this.legalCourseTH = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalCourseTH"]);
            this.legalCourseEN = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalCourseEN"]);
            this.legalProductTH = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalProductTH"]);
            this.legalProductEN = System.Net.WebUtility.HtmlDecode(System.Web.Configuration.WebConfigurationManager.AppSettings["legalProductEN"]);
        }

        public string legalTH { get; set; }

        public string legalEN { get; set; }

        public string legalMembershipTH { get; set; }

        public string legalMembershipEN { get; set; }

        public string legalCourseTH { get; set; }

        public string legalCourseEN { get; set; }

        public string legalProductTH { get; set; }

        public string legalProductEN { get; set; }
    }
}