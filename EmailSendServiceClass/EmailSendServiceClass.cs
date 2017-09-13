using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using Common;
using System.Threading.Tasks;
using System.Threading;

namespace EmailSendServiceDLL
{
    public class EmailSendServiceClass
    {
        #region vars
        private string strLogin; // email c которого будет рассылаться почта
        private string strPassword; // пароль к email с которого будет рассылаться почта
        private string strSmtp; // smtp - server
        private int iSmtpPort; // порт для smtp-server
        private string strBody; // текст письма для отправки
        private string strSubject = "Привет из С#"; // тема письма для отправки
        #endregion

        public string Body
        {
            get { return strBody; }
            set { strBody = value; }
        }
        public string Subject
        {
            get { return strSubject; }
            set { strSubject = value; }
        }

        public EmailSendServiceClass(string sLogin, string sPassword, string sSmtp, int iPort, string sBody)
        {
            strLogin = sLogin;
            strPassword = sPassword;

            strSmtp = sSmtp;
            iSmtpPort = iPort;
            strBody = sBody;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort); 
                sc.Timeout = 100;
                
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }//private void SendMail(string mail, string name)

        private void SendMail(object objEmail) // Отправка email конкретному адресату
        {
            Emails obj = (Emails)objEmail;
            string mail = obj.Value;
            string name = obj.Name;

            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }//private void SendMail(string mail, string name)

        public void SendMails(IQueryable<Emails> emails)
        {
            foreach (Emails email in emails)
            {
                //SendMail(email.Value, email.Name);
                //Task.Factory.StartNew(() => SendMail(email.Value, email.Name));

                Thread thread = new Thread(new ParameterizedThreadStart(SendMail));
                thread.Start(email);

            }
        }
    }//private void SendMail(string mail, string name)
}
