using Git_system.Models.ECom.API;
using Git_system.MultiResources;
using System.Collections.Generic;

namespace Git_system.Controllers.EComAPI
{
    public class PaymentTypeController : MainController
    {
        private class PayType
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public PayType(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public PayType()
            {
            }
        }

        private List<PayType> getPaymentType()
        {
            var paymentType = new List<PayType>();
            paymentType.Add(new PayType(1, Multi._MembershipRegisterString5));
            if (Git_system.Models.ExtensionMethod.GetForCreditCardSetting().Enable_CreditCard == true)
                paymentType.Add(new PayType(2, Multi._MembershipRegisterString6));
            return paymentType;
        }

        // GET api/<controller>
        public DataAndStatus Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, getPaymentType());
            return dataAndStatus;
        }
    }
}