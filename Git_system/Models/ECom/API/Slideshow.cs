using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class SlideshowAPI
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static SlideshowAPI ToAPI(this Slideshow content, string language_code = "th")
        {
            SlideshowAPI rContent = new SlideshowAPI();
            if (content != null)
            {
                rContent.Image = content.getImageUrl();
                rContent.Title = content.getSlideshowTranslate(language_code).Title;
                rContent.Description = content.getSlideshowTranslate(language_code).Description;
                rContent.Link = content.Link;
            }
            return rContent;
        }

        public static List<SlideshowAPI> ToAPI(this List<Slideshow> content, string language_code = "th")
        {
            return content.ConvertAll(c => c.ToAPI(language_code));
        }
    }
}