# Send Email using SMTP Email Service

* You can send email using third party libraries or using SMTP directly. Anothr approach is using POP3.
* Here, we will create SMTP EmailService and send email using this service.
* We need email server. For this go to https://mailtrap.io/ and get server credentials. Remember this is only for development, not for production. For production, you have to change email server detail as per your own email server.
* Here we will use dummy credentials of https://mailtrap.io/ in our appsettings.json file. For production, change it accordingly.
```json
"SMTPConfig": {
    "SenderAddress": "no-reply@gmail.com",
    "SenderDisplayName": "John Smith",
    "UserName": "0a584bf204d120",
    "Password": "8ebea060c2a335",
    "Host": "smtp.mailtrap.io",
    "Port": 25,
    "EnableSSL": true,
    "UseDefaultCredentials": true,
    "IsBodyHTML": true
  }
  ```
* Create SMTPConfigModel.cs and create all properties against all appsettings.json SMTPConfig values. 
* Register configuration in program.cs file to use appsettings.json values.
```c#
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
```
* Now you can access SMTPConfigModel mapped to appsettings.json section as below
```c#
public EmailService(IOptions<SMTPConfigModel> smtpConfig)
{
    //The IOptions service is used to bind strongly types options class to configuration section
    //and registers it to the Asp.Net Core Dependency Injection Service Container as singleton lifetime.
    //It exposes a Value property which contains your configured TOptions class.
    this.smtpConfig = smtpConfig.Value;
}
```
* Create Service.cs file under Service folder and write method SendEmail
* Create another folder EmailTemplate and create TestEmail.html file
* Create model UserEmailOptions.cs 
* Create interface of EmailService.cs and register in program.cs file
* Inject EmailService in HomeController constructor