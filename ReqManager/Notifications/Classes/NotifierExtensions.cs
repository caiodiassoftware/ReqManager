using ReqManager.Notifications.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace ReqManager.Notifications.Classes
{
    public static class Constants
    {
        public const string TempDataKey = "Messages";
    }

    public static class NotifierExtensions
    {
        public static void Error(this INotifierService notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Danger, text, format);
        }

        public static void Info(this INotifierService notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Info, text, format);
        }

        public static void Success(this INotifierService notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Success, text, format);
        }

        public static void Warning(this INotifierService notifier, string text, params object[] format)
        {
            notifier.AddMessage(MessageSeverity.Warning, text, format);
        }

        public static MvcHtmlString DisplayMessages(this ViewContext context)
        {
            if (!context.Controller.TempData.ContainsKey(Constants.TempDataKey))
            {
                return null;
            }

            var messages = (IEnumerable<Message>)context.Controller.TempData[Constants.TempDataKey];
            var builder = new StringBuilder();
            foreach (var message in messages)
            {
                builder.AppendLine(message.Generate());
            }

            return builder.ToHtmlString();
        }

        private static MvcHtmlString ToHtmlString(this StringBuilder input)
        {
            return MvcHtmlString.Create(input.ToString());
        }
    }
}