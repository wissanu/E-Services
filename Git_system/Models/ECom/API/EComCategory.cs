using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class EComCategoryAPI
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Detail { get; set; }

        //public bool ActiveStatus { get; set; }

        //public bool VisibleStatus { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static List<Git_system.Models.ECom.API.EComCategoryAPI> ToAPI(this List<Git_system.Models.EComCategory> contents, int languageTypeID)
        {
            List<Git_system.Models.ECom.API.EComCategoryAPI> rContents = new List<Git_system.Models.ECom.API.EComCategoryAPI>();
            foreach (Git_system.Models.EComCategory content in contents)
            {
                rContents.Add(content.ToAPI(languageTypeID: languageTypeID));
            }
            return rContents;
        }

        public static Git_system.Models.ECom.API.EComCategoryAPI ToAPI(this Git_system.Models.EComCategory content, int languageTypeID)
        {
            Git_system.Models.ECom.API.EComCategoryAPI rContent = new Git_system.Models.ECom.API.EComCategoryAPI();
            rContent.Id = content.Id;
            rContent.Name = languageTypeID == 1 ? content.NameTH : content.NameEN;
            rContent.Detail = languageTypeID == 1 ? content.DetailTH : content.DetailEN;
            //rContent.ActiveStatus = content.ActiveStatus;
            //rContent.VisibleStatus = content.VisibleStatus;
            return rContent;
        }
    }
}