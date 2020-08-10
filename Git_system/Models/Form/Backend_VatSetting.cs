using System.ComponentModel.DataAnnotations;

namespace Git_system.Models.Form
{
    public class Backend_VatSetting
    {
        [Range(0, 100)]
        public double Vat { get; set; }

        public Backend_VatSetting()
        {
            this.Vat = 0;
        }
    }

    public static partial class ExtensionMethod
    {
        public static double getBackendVatSetting()
        {
            double vat = 7.0;
            if ((new Backend_VatSetting().Get()).Vat != 0)
                vat = (new Backend_VatSetting().Get()).Vat;
            return vat;
        }

        public static Git_system.Models.Form.Backend_VatSetting Get(this Git_system.Models.Form.Backend_VatSetting VatSetting)
        {
            double eServiceVat = System.Convert.ToDouble(System.Web.Configuration.WebConfigurationManager.AppSettings["eServiceVat"]);
            VatSetting.Vat = System.Convert.ToDouble(eServiceVat);
            return VatSetting;
        }

        public static void Save(this Git_system.Models.Form.Backend_VatSetting eServiceVatSetting)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["eServiceVat"].Value = eServiceVatSetting.Vat.ToString();
            config.Save();
        }
    }
}