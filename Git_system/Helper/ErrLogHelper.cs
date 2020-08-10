using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Web;

namespace Git_system.Helper
{
    public static class ErrLogHelper
    {
        private static object getObject(this Exception e)
        {
            return new
            {
                Message = e.Message,
                Data = e.Data,
                InnerException = e.InnerException,
                HelpLink = e.HelpLink,
                StackTrace = e.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Select(s => s.Trim()).ToArray(),
                HResult = e.HResult,
                Source = e.Source
            };
        }

        public static void saveToLog(this HttpServerUtilityBase server, Exception e, string name = "System")
        {
            try
            {
                var errObj = e.getObject();
                string errorLog = Newtonsoft.Json.JsonConvert.SerializeObject(errObj, Formatting.Indented);
                var filename = string.Format("{0} {1}.json", name, System.DateTime.UtcNow.ToString("o").Replace(":", "-"));
                string path = server.MapPath("~/Log/");
                string pathAndFilename = path + filename;

                saveToFile(pathAndFilename, errorLog);
            }
            catch { }
        }

        private static void logTransaction(String transactionId, String status, String description, object objLog)
        {
            try
            {
                var objTransaction = new
                {
                    transaction_id = transactionId,
                    status = status,
                    description = description,
                    data = objLog
                };

                string log = Newtonsoft.Json.JsonConvert.SerializeObject(objTransaction, Formatting.Indented);
                var filename = string.Format("{0} {1}.json", "CreditCard", System.DateTime.UtcNow.ToString("o").Replace(":", "-"));
                string path = AppDomain.CurrentDomain.BaseDirectory + "Log\\Transaction\\";
                string pathAndFilename = path + filename;

                saveToFile(pathAndFilename, log);
            }
            catch { }
        }

        public static void logTransaction(Hashtable collection, String transactionId, String status, String description)
        {
            logTransaction(transactionId, status, description, collection);
        }

        public static void logTransaction(Exception exception, String transactionId, String status, String description)
        {
            logTransaction(transactionId, status, description, exception.getObject());
        }

        public static void logTransaction(Hashtable collection, Exception exception, String transactionId, String status, String description)
        {
            var objLog = new
            {
                collection = collection,
                exception = exception.getObject()
            };
            logTransaction(transactionId, status, description, objLog);
        }

        private static void saveToFile(String pathAndFilename, String text)
        {
            //this code segment write data to file.
            FileStream fs1 = new FileStream(pathAndFilename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(text);
            writer.Close();
        }
    }
}