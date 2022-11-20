using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

public class EmailMessage
{
	public string ToAddress { get; set; }
	public string FromAddress { get; set; }
	public string Subject { get; set; }
	public string Content { get; set; }
}

public interface IEmailService
{
    void Send(string from, string to, string subject, string html);
}

public class EmailService : IEmailService
{
    private readonly AppSettings _appSettings;

    public EmailService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public void Send(EmailMessage EmailMessage)
    {
        try
        {
            string emailBody = string.Empty;
            //instantiate a new MimeMessage
            var message = new MimeMessage();
            //Setting the To e-mail address
            message.To.Add(MailboxAddress.Parse(EmailMessage.ToAddress));
            //Setting the From e-mail address
            message.From.Add(MailboxAddress.Parse(EmailMessage.FromAddress));
            //E-mail subject 
            message.Subject = EmailMessage.Subject;
            //E-mail message body
            emailBody = "<h2>Thanks for following! </h2>"
                        + $"<p>{EmailMessage.Content}</p>"
                        + "<h4>Sincerily,<br>Miguel Estrada</h4>";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };
            //Configure the e-mail
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("[HOST]", [PORT], true); // The last is security
                emailClient.Authenticate("[USER]", "[PASS]");
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            var error = $"ERROR: {ex}";
        }
    }
}