using System.Web.Mvc;
using System.Web.Routing;

namespace Git_system
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            "Robots.txt",
            "robots.txt",
                new
                {
                    controller = "Robots",
                    action = "RobotsText"
                }
            );

            routes.MapRoute(
                name: "Product",
                url: "Backend/Product/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_Product",
                    action = "Home",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Product Course",
                url: "Backend/Course/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_ProductCourse",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Product Payment Process",
                url: "Backend/Payment/Process/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_PaymentProcess",
                    action = "Home",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Product Confirm Payment",
                url: "Backend/Payment/Confirm/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_PaymentConfirm_payment",
                    action = "Home",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Membership",
                url: "Backend/User/Membership/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_Membership",
                    action = "Home",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Permission",
                url: "Backend/User/Permission/{action}/{id}",
                defaults: new
                {
                    controller = "Backend_Permission",
                    action = "Home",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                name: "Useradmin",
                url: "Backend/User/Useradmin/{action}/{id}",
                defaults: new { controller = "Backend_Useradmin", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Log",
                url: "Backend/Log/{action}/{id}",
                defaults: new { controller = "Backend_Log", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Email setting",
                url: "Backend/Setting/Email/{action}/{id}",
                defaults: new { controller = "Backend_EmailSetting", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment setting",
                url: "Backend/Setting/Payment/{action}/{id}",
                defaults: new { controller = "Backend_PaymentSetting", action = "Home", id = UrlParameter.Optional }
            );

            //-----------------ECommerce-----------------
            //Product
            routes.MapRoute(
                name: "E-Commerce Product",
                url: "Backend/E-Commerce/Product",
                defaults: new { controller = "Backend_ECommerce", action = "Product" }
            );
            routes.MapRoute(
                name: "E-Commerce Product Create",
                url: "Backend/E-Commerce/Product/Create/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Product_Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Product Detail",
                url: "Backend/E-Commerce/Product/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Product_Detail", id = UrlParameter.Optional }
            );
            //Product Category
            routes.MapRoute(
                name: "E-Commerce Product Category",
                url: "Backend/E-Commerce/Category",
                defaults: new { controller = "Backend_ECommerce", action = "ProductCategory" }
            );
            routes.MapRoute(
                name: "E-Commerce Product Category Create",
                url: "Backend/E-Commerce/Category/Create/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "ProductCategory_Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Product Category Detail",
                url: "Backend/E-Commerce/Category/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "ProductCategory_Detail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Product in Product Category",
                url: "Backend/E-Commerce/Category/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "ProductCategory_With_Product", id = UrlParameter.Optional }
            );

            //Ordre
            routes.MapRoute(
                name: "E-Commerce Order",
                url: "Backend/E-Commerce/Order",
                defaults: new { controller = "Backend_ECommerce", action = "Order" }
            );
            routes.MapRoute(
                name: "E-Commerce Order Detail",
                url: "Backend/E-Commerce/Order/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Order_Detail", id = UrlParameter.Optional }
            );
            //Payment
            routes.MapRoute(
                name: "E-Commerce Payment",
                url: "Backend/E-Commerce/Payment",
                defaults: new { controller = "Backend_ECommerce", action = "Payment" }
            );
            routes.MapRoute(
                name: "E-Commerce Payment Detail",
                url: "Backend/E-Commerce/Payment/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Payment_Detail", id = UrlParameter.Optional }
            );
            //Deliver
            routes.MapRoute(
                name: "E-Commerce Deliver",
                url: "Backend/E-Commerce/Deliver",
                defaults: new { controller = "Backend_ECommerce", action = "Deliver" }
            );
            routes.MapRoute(
                name: "E-Commerce Deliver Detail",
                url: "Backend/E-Commerce/Deliver/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Deliver_Detail", id = UrlParameter.Optional }
            );
            //Promotion
            routes.MapRoute(
                name: "E-Commerce Promotion",
                url: "Backend/E-Commerce/Promotion",
                defaults: new { controller = "Backend_ECommerce", action = "Promotion" }
            );
            routes.MapRoute(
                name: "E-Commerce Promotion Create",
                url: "Backend/E-Commerce/Promotion/Create/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Promotion_Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Promotion Detail",
                url: "Backend/E-Commerce/Promotion/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Promotion_Detail", id = UrlParameter.Optional }
            );
            //Statistic
            routes.MapRoute(
                name: "E-Commerce Statistic",
                url: "Backend/E-Commerce/Statistic",
                defaults: new { controller = "Backend_ECommerce", action = "Statistic" }
            );
            routes.MapRoute(
                name: "E-Commerce Statistic LogView",
                url: "Backend/E-Commerce/Statistic/LogView/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Statistic_LogView", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Statistic Keyword",
                url: "Backend/E-Commerce/Statistic/Keyword/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Statistic_Keyword", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Statistic Membership Detail",
                url: "Backend/E-Commerce/Statistic/Membership/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "Statictic_Membership_Detail", id = UrlParameter.Optional }
            );

            //Deliver Promotion
            routes.MapRoute(
                name: "E-Commerce Deliver Promotion",
                url: "Backend/E-Commerce/DeliverPromotion",
                defaults: new { controller = "Backend_ECommerce", action = "DeliverPromotion" }
            );
            routes.MapRoute(
                name: "E-Commerce Deliver Promotion Create",
                url: "Backend/E-Commerce/DeliverPromotion/Create/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "DeliverPromotionCreate", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "E-Commerce Deliver Promotion Detail",
                url: "Backend/E-Commerce/DeliverPromotion/Detail/{id}",
                defaults: new { controller = "Backend_ECommerce", action = "DeliverPromotionDetail", id = UrlParameter.Optional }
            );

            //-----------------ECommerce-----------------

            routes.MapRoute(
                name: "E-Commerce Authentication File",
                url: "ECom/File/{filename}",
                defaults: new { controller = "AuthenticationFile", action = "CheckAuthenticationForUploadFileInToDir", filename = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}