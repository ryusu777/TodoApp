using Library.Models;

namespace AuthContext.Application.User.Events;

public record UserSignedIn(Domain.User.User User) : IDomainEvent;
