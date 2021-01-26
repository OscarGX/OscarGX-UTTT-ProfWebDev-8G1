using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace UTTT.Ejemplo.Persona.Control.Ctrl
{
    public class CtrlEmail
    {
        public void sendEmail(String message, String file, String scope, String type)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("18300181@uttt.edu.mx");
                mailMessage.To.Add(new MailAddress("reyesmartinezoscar3g15@gmail.com"));
                mailMessage.Subject = "Error / Exception Handling";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = this.createMessage(message, type, file, scope);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("your-email-address-bro", "your-password");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            } catch (Exception _e)
            {
                Console.WriteLine(_e.Message);
            }
        }


        private String createMessage(String message, String type, String file, String scope)
        {
            String now = DateTime.Now.ToString();
            String body = "<h1>Ocurrió una excepción en el sistema</h1><br>" +
                "<p><strong>Mensaje de la excepción: </strong>"+message+"<br><strong>Tipo de la excepción: </strong>"+type+"<br>" +
                "<strong>Archivo: </strong>"+file+"<br><strong>Ámbito: </strong>"+scope+"<br>" +
                "<strong>Fecha y Hora: </strong>"+now+"<br></p>";
            // String htmlBodyMail = String.Format(body, message, type, file, scope, now);
            return body;
        }
    }
}
