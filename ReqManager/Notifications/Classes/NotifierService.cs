using ReqManager.Notifications.Interfaces;
using System.Collections.Generic;

namespace ReqManager.Notifications.Classes
{
    public class NotifierService : INotifierService
    {
        public IList<Message> Messages { get; private set; }

        public NotifierService()
        {
            Messages = new List<Message>();
        }

        public void AddMessage(MessageSeverity severity, string text, params object[] format)
        {
            Messages.Add(new Message { Severity = severity, Text = string.Format(text, format) });
        }
    }
}