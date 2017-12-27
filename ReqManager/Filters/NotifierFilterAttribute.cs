using ReqManager.Notifications.Classes;
using ReqManager.Notifications.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace ReqManager.Filters
{
    public class NotifierFilterAttribute : ActionFilterAttribute
    {
        public INotifierService Notifier { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var messages = Notifier.Messages;
            if (messages.Any())
            {
                filterContext.Controller.TempData[Constants.TempDataKey] = messages;
            }
        }
    }
}