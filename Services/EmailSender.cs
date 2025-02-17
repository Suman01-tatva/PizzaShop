using System.Net;
using System.Net.Mail;

namespace PizzaShop.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string? email, string? subject, string? message)
    {
        string? smtpServer = _configuration["EmailSettings: MailServer"];
        // int port = Int32.Parse(_configuration["EmailSettings: MailPort"]!);
        int port = 587;
        string? sendeEmail = _configuration["EmailSettings: FormEmail"];
        string? senderPassword = _configuration["EmailSettings: Password"];
        // bool enableSSL = bool.Parse(_configuration["EmailSettings: EnableSSL"]!);
        bool enableSSL = true;

        var client = new SmtpClient(smtpServer)
        {
            Port = port,
            Credentials = new NetworkCredential(sendeEmail, senderPassword),
            EnableSsl = enableSSL,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(sendeEmail!),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email!);

        await client.SendMailAsync(mailMessage);
    }
}
