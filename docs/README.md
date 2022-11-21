# Documentation  :page_with_curl:

Let's segment this project and learn over how it was built

## Class

Class **EmailService**

## Models

#### EmailMessage
-----
Model per Email
```cs
public class EmailMessage
{
	public string ToAddress { get; set; }
	public string FromAddress { get; set; }
	public string Subject { get; set; }
	public string Content { get; set; }
}
```

#### MailSettings
Model per SMTP Client
-----
```cs
public class MailSettings
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
}
```
 
## Functions

Function in charge of creating the SMTP connection.

```cs
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
                emailClient.Connect(_appSettings.Host, _appSettings.Port, _appSettings.UseSSL); // The last is security
                emailClient.Authenticate(_appSettings.UserName, _appSettings.Password);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            var error = $"ERROR: {ex}";
        }
    }
```