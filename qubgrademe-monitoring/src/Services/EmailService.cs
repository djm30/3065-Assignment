using System.Net;
using System.Net.Mail;
using src.Data;

namespace src.Services;

public class EmailService
{
    
    private DateTime _lastEmailSent = DateTime.MinValue;
    
    private readonly Config _config;
    
    // Create a Send method that sends an email to a list of emails
    public EmailService(Config config, MonitoringService monitoringService)
    {
        _config = config;
        monitoringService.ServiceStatusChanged += OnServiceStatusChanged;
    }
    
    // Write an event handler for the ServiceStatusChanged event
    private void OnServiceStatusChanged(object sender, List<ServiceMonitorSchema> ServiceStatus )
    {
        var emailText = EmailBody(ServiceStatus);
        if(emailText != "")
            Send("Service Status", emailText);
    }

    public void Send(string subject, string body)
    {
        
        // Check if last email was sent was over 15 minutes ago
        if (DateTime.Now.Subtract(_lastEmailSent).TotalMinutes < 15)
        {
            return;
        }
        
        // Create a new MailMessage object
        MailMessage mail = new MailMessage();

        // Set the sender address of the mail message
        mail.From = new MailAddress(Secrets.FromEmail);

        // Connect to google smtp server
        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(Secrets.FromEmail, Secrets.GmailAppPassword),
            EnableSsl = true
        };

        // Set the subject of the mail message
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        // Send the email to each email in the list
        foreach (string email in _config.Emails)
        {
            mail.To.Add(email);
            smtp.Send(mail);
            mail.To.Clear();
        }
        
        // Set last datetime sent to now
        _lastEmailSent = DateTime.Now;
    }

    public string EmailBody(List<ServiceMonitorSchema> serviceResults)
    {
        // Check if there was any response codes in any service in each list of each serviceResult
        // Check if a service return a response code not in the range 200 - 300 and add it to an a styled html table with the name and service url
        string html = "<table style='border: 1px solid black; border-collapse: collapse; padding: 6px;'><tr><th style='border: 1px solid black; padding: 6px;'>Name</th><th style='border: 1px solid black; padding: 6px;'>Url</th><th style='border: 1px solid black; padding: 6px;'>Status Code</th></tr>";
        foreach (ServiceMonitorSchema servicesResult in serviceResults)
        {
            foreach (var service in servicesResult.services)
            {
                if ((int)service.statusCode < 200 || (int)service.statusCode > 300)
                {
                    html += $"<tr><td style='border: 1px solid black; padding: 4px;'>{servicesResult.name}</td><td style='border: 1px solid black; padding: 4px;'>{service.url}</td><td style='border: 1px solid black; padding: 4px;'>{(int)service.statusCode}</td></tr>";
                }
            }

        }

        if (html ==
            "<table style='border: 1px solid black; border-collapse: collapse;'><tr><th style='border: 1px solid black;'>Name</th><th style='border: 1px solid black;'>Url</th></tr>")
            return "";
        html += "</table>";
        return "<h2>Service Status</h2>" + html;
    }
}