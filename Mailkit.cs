using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

if (ModelState.IsValid)
{
    try
    {
        string emailBody = string.Empty;
        //instantiate a new MimeMessage
        var message = new MimeMessage();
        //Setting the To e-mail address
        message.To.Add(MailboxAddress.Parse(SuscribeModel.Email));
        //Setting the From e-mail address
        message.From.Add(MailboxAddress.Parse(""));
        //E-mail subject 
        message.Subject = "Subscription Confirmation";
        //E-mail message body
        emailBody = "<h2>Thanks for subscribing! </h2>" 
                    + "<h4>Sincerily,<br>Miguel Estrada</h4>";
        message.Body = new TextPart(TextFormat.Html)
        {
            Text = emailBody
        };
        //Configure the e-mail
        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect("", 465, true);
            emailClient.Authenticate("", "");
            emailClient.Send(message);
            emailClient.Disconnect(true);
        }
    }
    catch (Exception ex)
    {
        ModelState.Clear();
    }

}
else
{
    ModelState.Clear();
}