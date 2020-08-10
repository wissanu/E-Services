using Git_system.Models;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    [Backend_AuthorizeAttrinbite]
    public class Backend_LegalConfigController : BackendController
    {
        //
        // GET: /Backend_LegalConfig/

        public ActionResult Home()
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);
            WebSettingConfig webSettingConfig = new WebSettingConfig();
            return View(webSettingConfig.HtmlDecode());
        }

        [HttpPost]
        [ValidateInput(false)]//มีการใช้ ckeditor
        [ValidateAntiForgeryToken]
        public ActionResult Home(WebSettingConfig webSettingConfig)
        {
            CheckPermissionWihtRedirect(true, false, false, false, false);

            webSettingConfig.legalCourseEN = webSettingConfig.legalCourseEN != null ? webSettingConfig.legalCourseEN : "";
            webSettingConfig.legalCourseTH = webSettingConfig.legalCourseTH != null ? webSettingConfig.legalCourseTH : "";

            webSettingConfig.legalEN = webSettingConfig.legalEN != null ? webSettingConfig.legalEN : "";
            webSettingConfig.legalTH = webSettingConfig.legalTH != null ? webSettingConfig.legalTH : "";

            webSettingConfig.legalMembershipEN = webSettingConfig.legalMembershipEN != null ? webSettingConfig.legalMembershipEN : "";
            webSettingConfig.legalMembershipTH = webSettingConfig.legalMembershipTH != null ? webSettingConfig.legalMembershipTH : "";

            webSettingConfig.legalProductEN = webSettingConfig.legalProductEN != null ? webSettingConfig.legalProductEN : "";
            webSettingConfig.legalProductTH = webSettingConfig.legalProductTH != null ? webSettingConfig.legalProductTH : "";

            webSettingConfig.Save();
            Git_system.App_Code.LogManageDatabase.add_to_database(LoginInformation_Backend().Name, "แก้ไขข้อกำหนดเงื่อนไข", 1);//add to logfile
            return View();
        }
    }
}