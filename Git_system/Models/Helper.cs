using System;

namespace Git_system.Models
{
    public static partial class Helper
    {
        public static string DateToSting(DateTime ds, DateTime de)
        {
            return DateToSting(ds, de, false);
        }

        public static string DateToSting(DateTime ds, DateTime de, bool short_date = false)
        {
            string culture = "";
            culture = System.Globalization.CultureInfo.CurrentCulture.ToString();
            return DateToSting(ds, de, short_date, culture);
        }

        public static string DateToSting(DateTime ds, DateTime de, bool short_date = false, string culture = "")
        {
            string dateStringOut = "";
            if (ds != null && de != null)
            {
                // set default datetime
                if (ds == new DateTime())
                    ds = de;
                else if (de == new DateTime())
                    de = ds;

                if (ds.Year == de.Year)
                    if (ds.Month == de.Month)
                        if (ds.Date == de.Date)
                            dateStringOut = short_date ? String.Format("{0}", de.ToStringDateByCulture("d/M/yyyy", culture))
                                                       : String.Format("{0}", de.ToStringDateByCulture("d MMM yyyy", culture));
                        else //same year, month not day
                            dateStringOut = short_date ? String.Format("{0} - {1}", ds.Day.ToString(), de.ToStringDateByCulture("d/M/yyyy", culture))
                                                       : String.Format("{0} - {1}", ds.Day.ToString(), de.ToStringDateByCulture("d MMM yyyy", culture));
                    else//same year not month
                        dateStringOut = short_date ? String.Format("{0} - {1}", ds.ToStringDateByCulture("d/M", culture), de.ToStringDateByCulture("d/M/yyyy", culture))
                                                   : String.Format("{0} - {1}", ds.ToStringDateByCulture("d MMM", culture), de.ToStringDateByCulture("d MMM yyyy", culture));
                else//not same year
                    dateStringOut = short_date ? String.Format("{0} - {1}", ds.ToStringDateByCulture("d/M/yyyy", culture), de.ToStringDateByCulture("d/M/yyyy", culture))
                                               : String.Format("{0} - {1}", ds.ToStringDateByCulture("d MMM yyyy", culture), de.ToStringDateByCulture("d MMM yyyy", culture));
            }
            else
            {
                if (ds != null)
                    dateStringOut = ds.ToStringDateByCulture("d MMM yyyy", culture);
                else if (de != null)
                    dateStringOut = de.ToStringDateByCulture("d MMM yyyy", culture);
                else
                    DateTime.Now.ToStringDateByCulture("d MMM yyyy", culture);
            }

            return dateStringOut;
        }

        /// <summary>
        /// Give datetime with format
        /// </summary>
        /// <param name="dt">Datetime will to convert to string</param>
        /// <param name="stringDatetime">datetime format</param>
        /// <param name="culture">datetime with culture</param>
        /// <returns>datetime with format</returns>
        public static string toStringDateByCulture(this DateTime dt, string stringDatetime, string culture)
        {
            return dt.ToStringDateByCulture(stringDatetime, culture);
        }

        private static string ToStringDateByCulture(this DateTime dt, string stringDatetime, string culture)
        {
            return dt.ToString(stringDatetime, new System.Globalization.CultureInfo(culture));
        }

        public static string PriceAndCurrency(double price, string currency, bool sensitiveZero = false, string culture = "")
        {
            string current_culture = System.Globalization.CultureInfo.CurrentCulture.Name;
            if (currency == null)
                currency = "THB";
            if (culture != current_culture && !String.IsNullOrEmpty(culture))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            }
            string priceAndCurrency_string = "";
            string currency_string = "";
            if (currency.ToLower() == "thb")
                currency_string = MultiResources.Multi.THB;
            else if (currency.ToLower() == "usd")
                currency_string = MultiResources.Multi.USD;
            if (price > 0)
                priceAndCurrency_string = String.Format("{0} {1}", price.ToString("#,##0.00"), currency_string);
            else
                priceAndCurrency_string = sensitiveZero ? "0" : MultiResources.Multi.Free;
            if (culture != current_culture && !String.IsNullOrEmpty(culture))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(current_culture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            }
            return priceAndCurrency_string;
        }

        public static string getCurrency(Membership membership)
        {
            string currency = "";
            if (membership == null)
                membership = membership.CheckMembershipType();
            currency = (membership.isLocal()) ? "THB" : "USD";
            return currency;
        }

        public static string check_culture(string LanguageType, Membership membership)
        {
            string culture = "th";
            if (membership == null)
                membership = membership.CheckMembershipType(__LanguageType: LanguageType); // check membership;
            if (!(LanguageType.ToUpper() == "TH" && membership.isLocal()))
                culture = "en-GB";
            return culture;
        }

        public static string check_culture(Membership membership)
        {
            string culture = "th";
            if (membership == null)
                membership = membership.CheckMembershipType(); // check membership;
            if (!membership.isLocal())
                culture = "en-GB";
            return culture;
        }
    }
}