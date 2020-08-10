using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Git_system.Models.Form
{
    public class Frontend_ShowMessage
    {
        private const string ACTION_VIEW = "ShowMessage";

        public Frontend_ShowMessage(string Header, string Message)
            : this(Title: null, Header: Header, Message: Message, RedirectAction: "Home")
        {
        }

        public Frontend_ShowMessage(string Title, string Header, string Message)
            : this(Title: Title, Header: Header, Message: Message, RedirectAction: "Home")
        {
        }

        public Frontend_ShowMessage(string Title, string Header, string Message, string RedirectAction = "Home")
        {
            this.Title = Title;
            this.Header = Header;
            this.Message = Message;
            this.RedirectAction = RedirectAction;
        }

        public string Title { get; set; }

        public string Header { get; set; }

        public string Message { get; set; }

        public string RedirectAction { get; set; }

        public ActionResult getAction()
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new
            {
                action = ACTION_VIEW,
                controller = "Home",
                Title = this.Title,
                Header = this.Header,
                Message = this.Message,
                RedirectAction = this.RedirectAction
            }));
        }
    }
}