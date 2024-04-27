namespace AuthContext.Application.Email.Model;

public record EmailContent(
    Domain.User.ValueObjects.Email RecipientEmail,
    string Subject,
    string Body
);
