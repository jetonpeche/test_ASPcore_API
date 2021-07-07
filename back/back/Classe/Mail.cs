using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace back.Classe
{
    public class Mail
    {
        private string expediteur;
        private string mdp;

        private string destinataire;
        private string sujet;
        private string message;

        public Mail(string _expediteur, string _mdp, string _destinataire, string _sujet, string _message)
        {
            mdp = _mdp;
            expediteur = Protection.XSS(_expediteur);
            destinataire = Protection.XSS(_destinataire);
            sujet = Protection.XSS(_sujet);
            message = Protection.XSS(_message);
        }

        public void EnvoieMail()
        {
            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(expediteur, mdp)
            };

            MailMessage msg = new MailMessage(expediteur, destinataire, sujet, message)
            {
                Priority = MailPriority.Normal,
                BodyEncoding = System.Text.Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };

            client.Send(msg);
        }
    }
}
