using IntegrationContext.Domain.GiteaRepositories.Entities;
using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.Events;

public record RepositoryHookCreated(RepositoryHook Hook) 
    : IDomainEvent;
