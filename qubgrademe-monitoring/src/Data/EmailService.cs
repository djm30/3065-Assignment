using System.Net.Mail;

namespace src.Data;

public class EmailService
{
 
    // Create a Send method that sends an email to a list of emails
    public void Send(string subject, string body, string[] emails)
    {
        // Create a new MailMessage object
        MailMessage mail = new MailMessage();

        // Set the sender address of the mail message
        mail.From = new MailAddress(Secrets.FromEmail);

        // Connect to google smtp server
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new System.Net.NetworkCredential(Secrets.FromEmail, Secrets.GmailAppPassword);

        // Set the subject of the mail message
        mail.Subject = subject;
        mail.Body = body;

        // Send the email to each email in the list
        foreach (string email in emails)
        {
            mail.To.Add(email);
            smtp.Send(mail);
            mail.To.Clear();
        }
    }
}