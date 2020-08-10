using Git_system.Models;
using Git_system.Models.ECom.API;
using System.Collections.Generic;
using System.Linq;

namespace Git_system.Controllers.EComAPI
{
    public class ElectronicFilesHistoryController : MainController
    {
        private List<EComProduct> getEletronicProductByMembership(int membershipId)
        {
            //var content = db.EComProducts.Where(p => p.ElectronicFileStatus == true && p.EComOrderItems.Count() > 0).ToList().Where(p => p.EComOrderItems.ToList().ConvertAll(c => c.EComOrder).Where(c => c.PaymentProcessStatusId == 3 && c.MembershipId == membershipId).Count() > 0).Where(p => p.EComOrderItems.Count() > 0 ? Array.Exists(p.EComOrderItems.ToList().ConvertAll(c => c.EComOrder).ToArray(), e => e.MembershipId == membershipId) : false).ToList();

            var content = db.EComProducts.Where(p => p.ElectronicFileStatus == true && p.EComOrderItems.Count() > 0).ToList().Where(p => p.EComOrderItems.ToList().ConvertAll(c => c.EComOrder).Where(c => c.PaymentProcessStatusId == 3 && c.MembershipId == membershipId).Count() > 0).ToList();

            return content;
        }

        // GET api/<controller>
        [AllowCrossSite]
        public DataAndStatus Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            var language_code = _LanguageTypeId == 1 ? "th" : "en";
            dataAndStatus = new DataAndStatus(1, getEletronicProductByMembership(_MembershipId).ToAPIOneLanguageWithHistory(new Membership(_MembershipId, (short)_MembershipTypeId), _LanguageTypeId));
            return dataAndStatus;
        }
    }
}