using ReqManager.Services.Others.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ReqManager.Services.Others.Classes
{
    public class EmailService : IEmailService
    {
        private List<String> body;
        private MailAddress from { get; set; }
        private SmtpClient cliente { get; set; }

        public EmailService()
        {
            body = new List<String>();
            setClient();
            setSender();
        }

        public void addParagraph(String frase)
        {
            addTagHtml("p", frase);
        }

        public void addHeaderH1(String frase)
        {
            addTagHtml("h1", frase);
        }

        public void addHeaderH2(String frase)
        {
            addTagHtml("h2", frase);
        }

        public void addHeaderH3(String frase)
        {
            addTagHtml("h3", frase);
        }

        public void addParagraphCenter(String frase)
        {
            body.Add("<p><center>" + frase + "</center></p>");
        }

        public void breakLine()
        {
            body.Add("</br>");
        }

        private void addTagHtml(String tag, String phrase)
        {
            body.Add("<" + tag + ">" + phrase + "</" + tag + ">");
        }

        public void send(String subject, String receiverMail, 
            String receiverName, String applicationName = "ReqManager", List<string> attachments = null)
        {
            try
            {                
                MailAddress to = new MailAddress(receiverMail, receiverName);
                MailMessage mensagem = new MailMessage(from, to);

                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                body.Append("<div style=\"width:580px; margin:auto; padding:3px; border-radius: 5px 5px 5px 5px; border:solid 2px #999;\">");
                body.Append("<h1><center>" + applicationName + "</center></h1>");
                body.Append("</br>");

                foreach (String c in this.body)
                    body.Append(c);

                body.Append("<p style=\"font-size:12px; color:#999; text-align:center;\">This email was sent by the ReqManager application.</p>");
                body.Append("</div></body></html>");

                mensagem.Body = body.ToString();

                mensagem.Subject = subject;
                mensagem.IsBodyHtml = true;
                mensagem.BodyEncoding = System.Text.Encoding.UTF8;

                if (attachments != null)
                {
                    List<Attachment> listaAnexos = new List<Attachment>();

                    foreach (string obj in attachments)
                    {
                        Attachment anexo = new Attachment(obj);
                        anexo.Name = string.Concat(obj.Substring(obj.LastIndexOf("\\") + 1, (obj.Length - (obj.LastIndexOf("\\") + 1))));
                        mensagem.Attachments.Add(anexo);

                        listaAnexos.Add(anexo);
                    }
                }

                cliente.Send(mensagem);

                if (attachments != null)
                    mensagem.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void send(String subject, Dictionary<String, String> receiversMails, 
            String applicationName = "ReqManager", List<string> attachments = null)
        {
            try
            {
                foreach (KeyValuePair<string, string> entry in receiversMails)
                    send(subject, entry.Value, entry.Key, applicationName, attachments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void setSender()
        {
            from = new MailAddress("reqmanageraadsp@gmail.com", "ReqManager Notifications");
        }

        private void setClient()
        {
            try
            {
                cliente = new SmtpClient("smtp.gmail.com", 587);
                cliente.Credentials = new System.Net.NetworkCredential("reqmanageraadsp@gmail.com", "ReqManager2017");
                cliente.EnableSsl = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

