using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace Git_system.Helper
{
    public class CreditCard
    {
        private static String creditCardAccessKey = WebConfigurationManager.AppSettings["CREDIT_CARD_ACCESS_KEY"];
        private static String creditCardSecretKey = WebConfigurationManager.AppSettings["CREDIT_CARD_SECRET_KEY"];
        private static String creditCardProfileId = WebConfigurationManager.AppSettings["CREDIT_CARD_PROFILE_ID"];
        private static String creditCardUrl = WebConfigurationManager.AppSettings["CREDIT_CARD_URL"];

        public static String getURL()
        {
            return creditCardUrl;
        }

        public static NameValueCollection getRequest(string reference, double amount, string currency)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("access_key", creditCardAccessKey);
            data.Add("profile_id", creditCardProfileId);
            data.Add("transaction_uuid", System.Guid.NewGuid().ToString());//??
            data.Add("signed_field_names", "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency");
            data.Add("unsigned_field_names", "");
            data.Add("signed_date_time", DateTime.UtcNow.ToString("yyyy-MM-dd\'T\'HH:mm:ss\'Z\'", new System.Globalization.CultureInfo("en")));
            data.Add("locale", "en");

            data.Add("transaction_type", "sale");
            data.Add("reference_number", reference);//number
            data.Add("amount", amount.ToString("0.00"));//price
            data.Add("currency", currency);

            data.Add("signature", sign(data.ToDictionary()));
            return data;
        }

        public static String sign(System.Collections.Hashtable paramsHashtable)
        {
            return sign(buildDataToSign(paramsHashtable), creditCardSecretKey);
        }

        public static String sign(IDictionary<string, string> paramsArray)
        {
            return sign(buildDataToSign(paramsArray), creditCardSecretKey);
        }

        private static String sign(String data, String secretKey)
        {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string, string> paramsArray)
        {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (String signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return commaSeparate(dataToSign);
        }

        private static String buildDataToSign(System.Collections.Hashtable paramsArray)
        {
            String[] signedFieldNames = ((string)paramsArray["signed_field_names"]).Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (String signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign)
        {
            string str = String.Join(",", dataToSign);
            return str;
        }
    }
}