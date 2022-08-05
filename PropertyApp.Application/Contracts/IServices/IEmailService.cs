using Microsoft.Extensions.Options;
using PropertyApp.Application.Services.EmailService;
using PropertyApp.Domain.Common;

namespace PropertyApp.Application.Contracts.IServices;

public interface IEmailService
{
    public Task SendEmailAsync(EmailDto email, IOptions<EmailSettings> config);
}
