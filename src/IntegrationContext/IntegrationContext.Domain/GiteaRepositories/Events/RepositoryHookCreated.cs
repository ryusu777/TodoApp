using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.Events;

public record RepositoryHookCreated(GiteaRepository GiteaRepository) 
    : IDomainEvent;
