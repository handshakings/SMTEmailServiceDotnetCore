using SMTPEmailService.Models;

namespace SMTPEmailService.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions emailOptions);
    }
}