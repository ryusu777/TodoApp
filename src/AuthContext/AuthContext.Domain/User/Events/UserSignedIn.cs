using Library.Models;

namespace AuthContext.Domain.User.Events;

public record UserSignedIn(Domain.User.User User) : IDomainEvent;
