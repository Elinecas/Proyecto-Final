using System;
using System.Net;
using System.Net.Mail;

namespace Correo
{
    public class EnviadorCorreo
    {

        public void Enviar(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                string _servidor= "smtp.gmail.com", de = "gestordepacientesrd@gmail.com", contra = "DavidyEnmanuel";
                int puerto= 587;
                
                MailMessage correo = new MailMessage();
                SmtpClient servidor = new SmtpClient(_servidor);

                correo.From = new MailAddress(de);
                correo.To.Add(destinatario);
                correo.Subject = asunto;
                correo.Body = cuerpo;

                servidor.Port = puerto;
                servidor.Credentials = new NetworkCredential(de, contra);
                servidor.EnableSsl = true;

                servidor.Send(correo);

            }
            catch (Exception e)
            {

            }


        }

    }
}
