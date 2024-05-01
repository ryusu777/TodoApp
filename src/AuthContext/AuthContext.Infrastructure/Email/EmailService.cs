using AuthContext.Application.Email;
using AuthContext.Application.Email.Model;
using Library.Models;

namespace AuthContext.Infrastructure.Email;

public class EmailService : IEmailService
{
    public Task<Result> SendEmailAsync(EmailContent content)
    {
        throw new NotImplementedException();
    }
}

