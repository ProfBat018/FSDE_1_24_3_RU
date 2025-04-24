using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

var configBuilder = new ConfigurationBuilder();

configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = configBuilder.Build();

var smtpHost = config["Smtp:Host"];
var smtpPort = config["Smtp:Port"];
var smtpUser = config["Smtp:Username"];
var smtpPassword = config["Smtp:Password"];

Console.WriteLine("Enter your message:");
var messageText = Console.ReadLine();

Console.WriteLine("Enter recipient email:");
var recipientEmail = Console.ReadLine();

using var mailMessage = new MailMessage()
{
    From = new MailAddress("profbat018@gmail.com"),
    Subject = "Test Email",
    Body = messageText,
    IsBodyHtml = false,
};

mailMessage.To.Add(new MailAddress(recipientEmail));

using var smtpClient = new SmtpClient(smtpHost, int.Parse(smtpPort))
{
    Credentials = new NetworkCredential(smtpUser, smtpPassword),
    EnableSsl = true,
};

try
{
    smtpClient.Send(mailMessage);
    Console.WriteLine("Email sent successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to send email: {ex.Message}");
}
