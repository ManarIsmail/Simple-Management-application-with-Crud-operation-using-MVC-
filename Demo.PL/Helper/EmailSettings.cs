using Demo.DAL.Entities;
using System.Net.Mail;
using System.Net;

namespace Demo.PL.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.ethereal.email", 587);

            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("bo.mante94@ethereal.email", "8rVPubqssjCHPUfynQ");

            client.Send("ahmed@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
