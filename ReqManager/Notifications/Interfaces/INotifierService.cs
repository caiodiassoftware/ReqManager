using ReqManager.Notifications.Classes;
using System.Collections.Generic;

namespace ReqManager.Notifications.Interfaces
{
    public interface INotifierService
    {
        IList<Message> Messages { get; }
        void AddMessage(MessageSeverity severity, string text, params object[] format);
    }
}