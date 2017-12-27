using System.Text;
using System.Web.Mvc;

namespace ReqManager.Notifications.Classes
{
    public enum MessageSeverity
    {
        None,
        Info,
        Success,
        Warning,
        Danger
    }

    public class Message
    {
        public MessageSeverity Severity { get; set; }

        public string Text { get; set; }

        public string Generate()
        {
            var isDismissable = Severity != MessageSeverity.Danger;
            if (Severity == MessageSeverity.None) Severity = MessageSeverity.Info;
            var sb = new StringBuilder();
            var divTag = new TagBuilder("div");
            divTag.AddCssClass("alert fade in");
            divTag.AddCssClass("alert-" + Severity.ToString().ToLower());


            var spanTag = new TagBuilder("span");
            spanTag.MergeAttribute("id", "MessageContent");

            if (isDismissable)
            {
                divTag.AddCssClass("alert-dismissable");
            }

            sb.Append(divTag.ToString(TagRenderMode.StartTag));

            if (isDismissable)
            {
                var buttonTag = new TagBuilder("button");
                buttonTag.MergeAttribute("class", "close");
                buttonTag.MergeAttribute("data-dismiss", "alert");
                buttonTag.MergeAttribute("aria-hidden", "true");
                buttonTag.InnerHtml = "×";
                sb.Append(buttonTag.ToString(TagRenderMode.Normal));
            }

            sb.Append(spanTag.ToString(TagRenderMode.StartTag));
            sb.Append(Text);
            sb.Append(spanTag.ToString(TagRenderMode.EndTag));
            sb.Append(divTag.ToString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }
}