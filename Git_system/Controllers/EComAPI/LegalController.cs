using Git_system.Models.ECom.API;

namespace Git_system.Controllers.EComAPI
{
    public class LegalController : MainController
    {
        private string getLegal(int languageCode)
        {
            Git_system.Models.WebSettingConfig webSettingConfig = new Git_system.Models.WebSettingConfig();
            return languageCode == 1 ? webSettingConfig.legalProductTH : webSettingConfig.legalProductEN;
        }

        // GET api/<controller>
        public DataAndStatus Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            dataAndStatus = new DataAndStatus(1, getLegal(_LanguageTypeId));
            return dataAndStatus;
        }
    }
}