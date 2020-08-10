using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    public class SEOHelper
    {
        private readonly HttpContextBase _httpContext;

        public SEOHelper(HttpContextBase httpContextBase)
        {
            _httpContext = httpContextBase;
        }
    }

    public class RobotsController : Controller
    {
        //
        // GET: /Robots/

        public ActionResult TxT()
        {
            return View();
        }

        public FileContentResult RobotsText()
        {
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine("User-agent: *");
            // change this to however you want to detect a production URL
            var isProductionUrl = Request.Url != null && !Request.Url.ToString().ToLowerInvariant().Contains("elasticbeanstalk");
            if (isProductionUrl)
            {
                contentBuilder.AppendLine("Disallow: /Backend");
                contentBuilder.AppendLine("Disallow: /backend");
                contentBuilder.AppendLine("Disallow: /Images");
                contentBuilder.AppendLine("Disallow: /images");
            }
            else
            {
                contentBuilder.AppendLine("Disallow: /");
            }
            return File(Encoding.UTF8.GetBytes(contentBuilder.ToString()), "text/plain");
        }
    }
}