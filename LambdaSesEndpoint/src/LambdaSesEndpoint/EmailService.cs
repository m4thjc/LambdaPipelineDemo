using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LambdaSesEndpoint
{
    public class EmailService
    {
        public string _smtpUsername;
        public string _smtpPassword;
        public int _port;
        public string _host;
        public SmtpClient _emailClient;
       
        public EmailService(ILambdaContext context, string smtpUserName, string smtpPassword, int port, string host) {
            _smtpUsername = smtpUserName;
            _smtpPassword = smtpPassword;   
            _port = port;
            _host = host;
            _emailClient = new SmtpClient(_host, _port);
            _emailClient.UseDefaultCredentials = false;
            _emailClient.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            _emailClient.EnableSsl = true;
        }

        public void Send(ILambdaContext context, string to, string subject, string html, string from = null)
        {
            context.Logger.LogInformation("Attempting to send email");
            var message = "This is the message";
            MailAddress fromAddress = new MailAddress("john.mathias3@gmail.com", "John Mathias",Encoding.UTF8);
            MailAddress toAddress = new MailAddress(to, to, Encoding.UTF8);

            MailMessage msg = new MailMessage(fromAddress, toAddress);
            msg.Body = message;
            msg.Subject = subject;
            msg.BodyEncoding = Encoding.UTF8;
            msg.SubjectEncoding = Encoding.UTF8;
            context.Logger.LogInformation("Calling Send Message");
            _emailClient.Send(msg);
            context.Logger.LogInformation("Calling Send Message complete");
        }
    }

}
