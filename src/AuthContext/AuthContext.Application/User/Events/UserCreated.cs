using Library.Models;

namespace AuthContext.Application.User.Events;

public record UserCreated(Domain.User.User User) : IDomainEvent;
