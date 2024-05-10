using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;

public record AttachRepositoryCommand(
    string ProjectId, string RepoOwner, string RepoName) : ICommand;
