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

        public bool recoveryPasswordEmail(String email, String name, String token)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("18300181@uttt.edu.mx");
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = "Recuperar contraseña";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = this.createRecoveryMessage(name, token);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("your-email-address-bro", "your-password");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception _e)
            {
                Console.WriteLine(_e.Message);
                return false;
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

        private String createRecoveryMessage(String name, String token)
        {
            // String devUrl = "http://localhost:36683/RecuperarContraseña.aspx?token="+token;
            String prodUrl = "https://uttt-oscar-pwd-8igdsg1.azurewebsites.net/RecuperarContraseña.aspx?token="+token;
            String body = "<h2>Hola " + name + "</h2><br>" +
                "<p>Te enviamos este correo para que puedas recuperar tu contraseña," +
                " accede al siguiente link.</p>" +
                "<a href='"+ prodUrl +"'>Recuperar contraseña</a>";
            return body;
        }
    }
}
