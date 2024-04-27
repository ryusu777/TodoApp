using AuthContext.Application.Email.Model;
using Library.Models;

namespace AuthContext.Application.Email;

public interface IEmailService
{
    public Task<Result> SendEmailAsync(EmailContent content);
}
