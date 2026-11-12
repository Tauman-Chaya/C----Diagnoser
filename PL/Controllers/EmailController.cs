using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public EmailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        if (request == null) return BadRequest("Invalid request");

        // פרטי השרת והחשבון נקראים מההגדרות (appsettings + user-secrets), ולא כתובים בקוד
        var email = _configuration.GetSection("Email");
        var host = email["SmtpHost"];
        var user = email["UserName"];
        var password = email["Password"];
        var from = email["From"];

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            return StatusCode(500, "Email is not configured");

        try
        {
            var smtpClient = new SmtpClient(host)
            {
                Port = int.TryParse(email["Port"], out var port) ? port : 587,
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(string.IsNullOrEmpty(from) ? user : from),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(request.ToEmail);

            await smtpClient.SendMailAsync(mailMessage);
            return Ok("Email sent successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Email failed: {ex.Message}");
        }
    }
}

public class EmailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
