# Documentation

Let's segment this project and learn over how it was built

## Class



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
