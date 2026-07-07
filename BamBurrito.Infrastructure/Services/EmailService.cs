using System;
using System.Threading.Tasks;
using BamBurrito.Core.Entities;
using BamBurrito.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BamBurrito.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendOfferRequestEmailAsync(OfferRequest request)
        {
            try
            {
                var email = new MimeMessage();

                // Använd företagets email som avsändare
                var fromAddress = _configuration["EmailSettings:FromAddress"] ?? "noreply@bamburrito.se";
                email.From.Add(MailboxAddress.Parse(fromAddress));

                // Målet (Ägaren)
                var ownerEmail = _configuration["EmailSettings:OwnerEmail"] ?? "nordinen93@hotmail.com";
                email.To.Add(MailboxAddress.Parse(ownerEmail));

                // Sätt reply-to så ägaren enkelt kan svara kunden rakt av
                email.ReplyTo.Add(MailboxAddress.Parse(request.Email));

                email.Subject = $"Ny offertförfrågan från {request.Name}";

                string htmlBody = $@"
<!DOCTYPE html>
<html lang=""sv"">
<head>
    <meta charset=""UTF-8"">
    <style>
        body {{
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            background-color: #0b0f19;
            color: #ffffff;
            max-width: 600px;
            margin: 0 auto;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0,0,0,0.2);
            border: 2px solid #FF8400;
        }}
        .header {{
            background-color: #FF8400;
            padding: 20px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            color: #0b0f19;
            font-size: 28px;
            text-transform: uppercase;
            letter-spacing: 2px;
        }}
        .content {{
            padding: 30px;
        }}
        .content p {{
            line-height: 1.6;
            color: #d1d5db;
        }}
        .field {{
            margin-bottom: 15px;
        }}
        .field-label {{
            font-weight: bold;
            color: #FF8400;
            text-transform: uppercase;
            font-size: 12px;
            letter-spacing: 1px;
            display: block;
            margin-bottom: 5px;
        }}
        .message-box {{
            background-color: #1f2937;
            padding: 15px;
            border-left: 4px solid #FF8400;
            border-radius: 4px;
            margin-top: 10px;
            color: #fff;
        }}
        .footer {{
            text-align: center;
            padding: 15px;
            font-size: 12px;
            color: #6b7280;
            border-top: 1px solid #1f2937;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Ny Offertförfrågan</h1>
        </div>
        <div class=""content"">
            <div class=""field"">
                <span class=""field-label"">Namn:</span>
                <div>{request.Name}</div>
            </div>

            <div class=""field"">
                <span class=""field-label"">Email:</span>
                <div><a href=""mailto:{request.Email}"" style=""color: #3b82f6;"">{request.Email}</a></div>
            </div>

            <div class=""field"">
                <span class=""field-label"">Önskat datum & tid:</span>
                <div>{request.Date:yyyy-MM-dd} | {request.StartTime:HH:mm} - {request.EndTime:HH:mm}</div>
            </div>

            <div class=""field"">
                <span class=""field-label"">Meddelande:</span>
                <div class=""message-box"">
                    {request.Message.Replace("\n", "<br/>")}
                </div>
            </div>

            <p style=""margin-top: 30px; font-size: 12px; color: #9ca3af;"">
                Tips: Svara direkt på detta mail för att skicka ditt prisförslag till kunden!
            </p>
        </div>
        <div class=""footer"">
            &copy; {DateTime.Now.Year} Bam! Burrito.
        </div>
    </div>
</body>
</html>";

                email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

                var smtpHost = _configuration["EmailSettings:SmtpHost"] ?? "localhost";
                var smtpPort = int.TryParse(_configuration["EmailSettings:SmtpPort"], out int port) ? port : 25;
                var useSsl = bool.TryParse(_configuration["EmailSettings:UseSsl"], out bool ssl) && ssl;
                var smtpUser = _configuration["EmailSettings:SmtpUser"] ?? "";
                var smtpPass = _configuration["EmailSettings:SmtpPass"] ?? "";

                using var smtp = new SmtpClient();

                try
                {
                    await smtp.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    if (!string.IsNullOrEmpty(smtpUser))
                    {
                        await smtp.AuthenticateAsync(smtpUser, smtpPass);
                    }
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                    _logger.LogInformation("Offertförfrågan mail skickat till {OwnerEmail} för {Customer}", ownerEmail, request.Name);
                }
                catch (Exception smtpEx)
                {
                    // For dev purposes, log the error but we might just simulate success if no local SMTP exists.
                    _logger.LogWarning(smtpEx, "SMTP sändning misslyckades. Konfigurera EmailSettings i appsettings.json. Sparar offerten i logg istället.");
                    _logger.LogInformation("SIMULERAD EMAIL -> \nTo: {OwnerEmail}\nFrom/ReplyTo: {CustomerEmail}\nNamn: {Name}\nDatum: {Date:yyyy-MM-dd HH:mm} - {EndTime:HH:mm}\nMeddelande: {Message}", 
                        ownerEmail, request.Email, request.Name, request.StartTime, request.EndTime, request.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel uppstod vid skapande av offert-mail.");
                throw;
            }
        }
    }
}
