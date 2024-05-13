using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.GiteaRepositories.Commands.AttachRepository;

public record AttachRepositoryCommand(
    string UserId, string ProjectId, string RepoOwner, string RepoName) : ICommand;
