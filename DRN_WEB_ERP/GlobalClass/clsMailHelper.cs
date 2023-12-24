using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsMailHelper
    {
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>

        public static void SendMailMessage(string from, string to, string cc, string bcc, string subject, string body)
        {            
            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.dekkogroup.com");
            //System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            //msg.From = new System.Net.Mail.MailAddress("imran.ahsan@dekkogroup.com", "Imran Ahsan");
            ////msg.ReplyTo = new System.Net.Mail.MailAddress("imran.ahsan@link3.net", "Reply of change request process");

            //msg.To.Add(new System.Net.Mail.MailAddress("imran.ahsan@dekkogroup.com"));
            //msg.To.Add(new System.Net.Mail.MailAddress("rizu@dekkogroup.com"));

            //msg.Subject = "This is Mail Subject";
            //msg.Body = "This is Mail Body";
            //smtp.Send(msg);

            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient("mail.dekkogroup.com");
            //SmtpClient mSmtpClient = new SmtpClient("mail.dalbd.com/webmail");

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from,"DAL-WEB-ERP");
            //mMailMessage.From = new MailAddress("weberp@dalbd.com", "DAL-WEB-ERP");

            // Set the recepient address of the mail message            
            mMailMessage.To.Add(new MailAddress(to));


            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                var mBcc = bcc.Split(',');
                foreach (string mbcc in mBcc)
                {
                    mMailMessage.Bcc.Add(new MailAddress(mbcc));
                }
            }      
            
            // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                var mCc = cc.Split(',');
                foreach (string mcc in mCc)
                {
                    mMailMessage.CC.Add(new MailAddress(mcc));
                }
            }
            
            // Set the subject of the mail message
            mMailMessage.Subject = subject;
            
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
           // mMailMessage.IsBodyHtml = true;

            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;            

            // Send the mail message
            mSmtpClient.Send(mMailMessage);
        }


        public static void SendMail(string to, string cc, string bcc, string subject, string body)
        {
            // mail Address from where you send the mail
            var fromAddress = "erp@doreen.com";

            //Password of your mail address
            const string fromPassword = "Erp@_74123";            

            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "mail.doreen.com";
                smtp.Port = 587;
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }

            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

             // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(fromAddress, "DRN-WEB-ERP");

            // Set the recepient address of the mail message            
            mMailMessage.To.Add(new MailAddress(to));

            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                var mBcc = bcc.Split(',');
                foreach (string mbcc in mBcc)
                {
                    mMailMessage.Bcc.Add(new MailAddress(mbcc));
                }
            }      
            
            // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                var mCc = cc.Split(',');
                foreach (string mcc in mCc)
                {
                    mMailMessage.CC.Add(new MailAddress(mcc));
                }
            }
            
            // Set the subject of the mail message
            mMailMessage.Subject = subject;
            
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
           // mMailMessage.IsBodyHtml = true;

            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;            

            // Send the mail message
            smtp.Send(mMailMessage);

            // Passing values to smtp object
            //smtp.Send(fromAddress, toAddress,cc subject, body);
        }
    }
}
