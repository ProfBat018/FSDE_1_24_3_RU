using System.Net;
using System.Net.Mail;

namespace UserService.API.Services.Classes;

public class EmailSender
{
    private readonly SmtpClient _smtpClient;
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpClient = new()
        {
            Host = _configuration["Email:Host"],
            Port = 587,
            EnableSsl = true
        };
        
        _smtpClient.Credentials = new NetworkCredential()
        {
            UserName = _configuration["Email:Username"],
            Password = _configuration["Email:Password"]
        };
    }

    public async Task SendEmailAsync(string email, string subject, string htmlBody)
    {
        MailMessage message = new()
        {
            From = new(_configuration["Email:From"]),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };
        
        message.To.Add(email);
        
        await _smtpClient.SendMailAsync(message);
    }
    
}