using System.Collections.Generic;

namespace Git_system.Models.ECom.API
{
    public class EComOrderItemAPI
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int EComProductId { get; set; }

        public int EComOrderId { get; set; }
    }

    public static partial class ExtensionMethod
    {
        public static List<Git_system.Models.ECom.API.EComOrderItemAPI> ToAPI(this ICollection<Git_system.Models.EComOrderItem> contents)
        {
            List<Git_system.Models.ECom.API.EComOrderItemAPI> rContents = new List<Git_system.Models.ECom.API.EComOrderItemAPI>();
            foreach (Git_system.Models.EComOrderItem content in contents)
            {
                rContents.Add(content.ToAPI());
            }
            return rContents;
        }

        public static List<Git_system.Models.ECom.API.EComOrderItemAPI> ToAPI(this List<Git_system.Models.EComOrderItem> contents)
        {
            List<Git_system.Models.ECom.API.EComOrderItemAPI> rContents = new List<Git_system.Models.ECom.API.EComOrderItemAPI>();
            foreach (Git_system.Models.EComOrderItem content in contents)
            {
                rContents.Add(content.ToAPI());
            }
            return rContents;
        }

        public static Git_system.Models.ECom.API.EComOrderItemAPI ToAPI(this Git_system.Models.EComOrderItem content)
        {
            Git_system.Models.ECom.API.EComOrderItemAPI rContent = new Git_system.Models.ECom.API.EComOrderItemAPI();
            rContent.Id = content.Id;
            rContent.Quantity = content.Quantity;
            rContent.EComProductId = content.EComProductId;
            rContent.EComOrderId = content.EComOrderId;
            return rContent;
        }
    }
}