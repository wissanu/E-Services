using System.Web.Optimization;

namespace Git_system.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BackendMain
            bundles.Add(new StyleBundle("~/Content/BackendMain").Include(
                "~/Scripts/vendor/offline/theme.css",
                "~/Scripts/vendor/pace/theme.css",
                "~/Content/bootstrap/bootstrap.css",
                "~/Content/Cameo/font-awesome.min.css",
                "~/Content/Cameo/animate.min.css",
                "~/Content/Cameo/skins/palette.11.css",
                "~/Content/Cameo/fonts/style.1.css",
                "~/Content/Cameo/main.css",
                "~/Content/BackendStyle.css"));

            bundles.Add(new ScriptBundle("~/Scripts/BackendMain").Include(
                "~/Scripts/vendor/modernizr.js"));

            //Backend core scripts
            bundles.Add(new ScriptBundle("~/Scripts/backend/core_scripts").Include(
                "~/Scripts/vendor/jquery-1.11.1.min.js",
                "~/Scripts/bootstrap/bootstrap.min.js",
                "~/Scripts/vendor/jquery.easing.min.js",
                "~/Scripts/vendor/jquery.placeholder.js"));
            //Backend page level scripts
            bundles.Add(new ScriptBundle("~/Scripts/backend/page_level_scripts").Include(
                "~/Scripts/vendor/offline/offline.min.js",
                "~/Scripts/vendor/pace/pace.min.js"));
            //Backend template scripts
            bundles.Add(new ScriptBundle("~/Scripts/backend/template_scripts").Include(
                "~/Scripts/js/off-canvas.js",
                "~/Scripts/js/main.js"));
            // /BackendMain

            //
            //responsive-tables
            bundles.Add(new StyleBundle("~/Content/responsive-tables").Include(
                "~/Scripts/vendor/responsive-tables/theme.css"));
            bundles.Add(new ScriptBundle("~/Scripts/responsive-tables").Include(
                "~/Scripts/vendor/responsive-tables/responsive-tables.js"));

            //
            //switchery
            bundles.Add(new ScriptBundle("~/Scripts/switchery").Include(
                "~/Scripts/vendor/switchery/switchery.js",
                "~/Scripts/CustomJavaScriptForm_switchery.js"));

            //
            //bootstrap-select
            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                "~/Scripts/vendor/bootstrap-select/bootstrap-select.css"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-select").Include(
                "~/Scripts/vendor/bootstrap-select/bootstrap-select.js"));

            //
            //flag-icon
            //http://lipis.github.io/flag-icon-css/
            bundles.Add(new StyleBundle("~/Content/Flag-icon-css/flags/flag-icon").Include(
                "~/Content/Flag-icon-css/css/flag-icon.css"));

            //
            //DataTable exprot file
            bundles.Add(new ScriptBundle("~/Scripts/DataTable_exprot").Include(
                "~/Scripts/DataTable/jquery.dataTables.min.js",
                "~/Scripts/DataTable/TableTool/dataTables.tableTools.min.js",
                "~/Scripts/DataTable/CustomScriptDatatable.min.js",
                "~/Scripts/DataTable/Plugins/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/DataTable_exprot_Custom").Include(
                            "~/Scripts/DataTable/jquery.dataTables.min.js",
                            "~/Scripts/DataTable/TableTool/dataTables.tableTools.min.js",
                            "~/Scripts/DataTable/Plugins/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/DataTable_exprot").Include(
                "~/Scripts/DataTable/TableTool/dataTables.tableTools.css",
                "~/Scripts/DataTable/Plugins/dataTables.bootstrap.css"));

            //
            //bootstrap-slider
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-slider").Include(
                "~/Scripts/vendor/slider/bootstrap-slider.js",
                "~/Scripts/js/components.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap-slider").Include(
                "~/Scripts/vendor/slider/slider.css"));

            //
            //Frontend
            bundles.Add(new StyleBundle("~/Content/Frontend-core_stylesheet").Include(
                "~/Content/Frontend/bootstrap.css",
                "~/Content/Frontend/bootstrap-theme.css",
                "~/Content/Frontend/fonts.css",
                "~/Content/Cameo/font-awesome.min.css",
                "~/Content/GitStyle.css",
                "~/Content/Frontend/Main.css"));

            bundles.Add(new ScriptBundle("~/Scripts/Frontend-BodySection_Core_scripts").Include(
                "~/Scripts/jquery-2.1.4.js",
                "~/Scripts/bootstrap/bootstrap.js",
                "~/Scripts/Frontend/Main.js"));

            //
            //bootstrap-datepicker and bootstrap-datepicker-thai
            //https://github.com/eternicode/bootstrap-datepicker
            //https://github.com/jojosati/bootstrap-datepicker-thai
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap-datepicker_and_bootstrap-datepicker-thai").Include(
                            "~/Scripts/bootstrap-datepicker/bootstrap-datepicker.js",
                            "~/Scripts/bootstrap-datepicker/bootstrap-datepicker-thai.js",
                            "~/Scripts/bootstrap-datepicker/locales/bootstrap-datepicker.th.js"));

            ///
            ///Krajee Bootstrap File Input
            ///http://plugins.krajee.com/file-input
            ///<summary>
            ///Scripts Krajee Bootstrap File Input
            ///</summary>
            bundles.Add(new ScriptBundle("~/Scripts/krajee_fileinput").Include(
                            "~/Scripts/krajee-fileinput/fileinput.min.js"));

            ///
            ///Krajee Bootstrap File Input
            ///http://plugins.krajee.com/file-input
            ///<summary>
            ///Content Krajee Bootstrap File Input
            ///</summary>
            bundles.Add(new StyleBundle("~/Content/krajee_fileinput").Include(
                            "~/Content/krajee-fileinput/fileinput.min.css"));

            ///<summary>
            ///Bootstrap Select
            ///</summary>
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap_select").Include(
                            "~/Scripts/vendor/bootstrap-select/bootstrap-select.js"));

            ///<summary>
            ///Bootstrap Select
            ///</summary>
            bundles.Add(new StyleBundle("~/Content/bootstrap_select").Include(
                            "~/Scripts/vendor/bootstrap-select/bootstrap-select.css"));

            ///<summary>
            ///imagesloaded
            ///</summary>
            bundles.Add(new ScriptBundle("~/Scripts/imagesloaded").Include(
                            "~/Scripts/vendor/isotope/isotope.pkgd.min.js",
                            "~/Scripts/vendor/imagesloaded/imagesloaded.js",
                            "~/Scripts/js/gallery.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}