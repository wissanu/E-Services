using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class EComBankTypeAPI
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        public string Bankname { get; set; }

        public string Branch { get; set; }

        public string AccountNo { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static List<EComBankTypeAPI> ToAPI(this List<ConfirmPaymentBankType> contents, int languageTypeID)
        {
            List<EComBankTypeAPI> rContents = new List<EComBankTypeAPI>();
            foreach (ConfirmPaymentBankType content in contents)
            {
                rContents.Add(content.ToAPI(languageTypeID: languageTypeID));
            }
            return rContents;
        }

        public static EComBankTypeAPI ToAPI(this ConfirmPaymentBankType content, int languageTypeID)
        {
            EComBankTypeAPI rContent = new EComBankTypeAPI();
            rContent.Id = content.Id;
            rContent.Name = languageTypeID == 1 ? content.NameTH : content.NameEN;
            rContent.Detail = languageTypeID == 1 ? content.DetailTH : content.DetailEN;
            rContent.Bankname = languageTypeID == 1 ? content.BanknameTH : content.BanknameEN;
            rContent.Branch = languageTypeID == 1 ? content.BranchTH : content.BranchEN;
            rContent.AccountNo = content.AccountNo;
            return rContent;
        }
    }
}