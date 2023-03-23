using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        private string receiverName;
        private string receiverEmailId;

        public void SendMail(string token, string emailId, string name)
        {
            this.receiverName = name;
            this.receiverEmailId = emailId;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if(!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] {typeof(string) });
                messageQueue.ReceiveCompleted += Queue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var message = messageQueue.EndReceive(e.AsyncResult);
                string token = message.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("surveshrathore98@gmail.com", "sygyhltmqcawxxtl"),
                };

                mailMessage.From = new MailAddress("surveshrathore98@gmail.com");
                mailMessage.To.Add(new MailAddress(receiverEmailId));

                string mailBody = $"<!DOCTYPE html>" +
                                  $"<html>" +
                                    $"<body style = \"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{receiverName}</b> </h1>\n" +
                                    $"<a href = \'http://localhost:5001/BookStore/ResetPassword/{token}'>Click me </a>" +
                                    $"</body>" +
                                    $"</html>";
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "BookStore Passwork Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
