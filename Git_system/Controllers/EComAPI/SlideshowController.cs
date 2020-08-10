using Git_system.Models.ECom.API;
using System.Linq;

namespace Git_system.Controllers.EComAPI
{
    public class SlideshowController : MainController
    {
        // GET api/<controller>
        [AllowCrossSite]
        public DataAndStatus Get()
        {
            DataAndStatus dataAndStatus = new DataAndStatus();
            var language_code = _LanguageTypeId == 1 ? "th" : "en";
            dataAndStatus = new DataAndStatus(1, db.Slideshows.Where(s => s.Status == true).ToList().ToAPI(language_code));
            return dataAndStatus;
        }
    }
}