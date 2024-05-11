using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories.Events;

public record GiteaRepositoryCreated(GiteaRepository GiteaRepository) 
    : IDomainEvent;
