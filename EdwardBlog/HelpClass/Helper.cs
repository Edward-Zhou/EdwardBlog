using EdwardBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EdwardBlog.HelpClass
{    
    public class Helper
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
        private string fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
        private string fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
        private string smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
        private int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"].ToString());
        public  string FromEmailAddress
        {
            get
            {
                return fromEmailAddress;
            }
            set
            {
                fromEmailAddress=value;
            }
        }
        public  string FromEmailDisplayName
        {
            get
            {
                return fromEmailDisplayName;
            }
            set
            {
                fromEmailDisplayName = value;
            }
        }
        public  string FromEmailPassword
        {
            get
            {
                return fromEmailPassword;
            }
            set
            {
                fromEmailPassword = value;
            }
        }
        public  string SMTPHost
        {
            get
            {
                return smtpHost;
            }
            set
            {
                smtpHost = value;
            }
        }
        public  int SMTPPort
        {
            get
            {
                return smtpPort;
            }
            set
            {
                smtpPort = value;
            }
        }
        public  void SendMail(Feedback feedback)
        {
            //send emial   
            //SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);   //for outlook.com mail
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);     //for gmail.com
            try
            {
                SmtpClient smtpClient = new SmtpClient(SMTPHost, SMTPPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(FromEmailAddress, FromEmailPassword);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromEmailAddress, FromEmailDisplayName);
                mail.To.Add(new MailAddress("v-tazho@hotmail.com"));
                mail.Subject = "FeedBack For Edward Blog";
                mail.Body = feedback.Content;
                smtpClient.Send(mail);
                logger.Info("Send Feedback succeed");
            }
            catch(Exception e)
            {
                logger.Error("Send Feedback failed, error message is" + e.ToString());
            }
        }
    }
}