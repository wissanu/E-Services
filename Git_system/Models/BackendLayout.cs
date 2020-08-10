namespace Git_system.Models.BackendLayout
{
    public class BackendMenuLayout
    {
        public AdminAndSecurity AdminAndSecurity = new AdminAndSecurity();
        public string AdminAndSecurityMain = "";
        public Register Register = new Register();
        public string RegisterMain = "";
        public Membership Membership = new Membership();
        public string MembershipMain = "";
        public Traning Traning = new Traning();
        public string TraningMain = "";
        public E_Commerce E_Commerce = new E_Commerce();
        public string E_CommerceMain = "";
        public E_service E_service = new E_service();
        public string E_serviceMain = "";
    }

    public class AdminAndSecurity
    {
        public string AdminManage = "";
        public string PermissionsManage = "";
        public string ChangePassword = "";
        public string Logs = "";
    }

    public class Register
    {
        public string RegisterManage = "";
    }

    public class Membership
    {
        public string MembershipManage = "";
        public string PaymentProcess = "";
        public string ConfirmPayment = "";
        public string ConfigPrice = "";
    }

    public class Traning
    {
        public string News = "";
        public string Product_SKU = "";
        public string Product_Group = "";
        public string Product = "";
        public string PaymentProcess = "";
        public string ConfirmPayment = "";
    }

    public class E_Commerce
    {
        public string Slide = "";
        public string Product = "";
        public string Category = "";
        public string Stock = "";
        public string Order = "";
        public string Payment = "";
        public string DeliverType = "";
        public string Deliver = "";
        public string DeliverPromotion = "";
        public string Promotion = "";
        public string Statistic = "";
    }

    public class E_service
    {
        public string EmailSetting = "";
        public string EmailSettingAdmin = "";
        public string PaySetting = "";
        public string LegalSetting = "";
        public string BenefitSetting = "";
    }
}