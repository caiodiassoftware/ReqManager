using System;
using System.Collections.Generic;

namespace ReqManager.Services.Others.Interfaces
{
    public interface IEmailService
    {
        void addParagraph(String frase);
        void addHeaderH1(String frase);
        void addHeaderH2(String frase);
        void addHeaderH3(String frase);
        void addParagraphCenter(String frase);
        void breakLine();
        void send(String subject, String receiverMail,
            String receiverName, String applicationName = "ReqManager", List<string> attachments = null);
        void send(String subject, Dictionary<String, String> receiversMails,
            String applicationName = "ReqManager", List<string> attachments = null);
    }
}
