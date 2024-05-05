using Library.Models;

namespace AuthContext.Domain.User.Events;

public record UserCreated(Domain.User.User User) : IDomainEvent;
