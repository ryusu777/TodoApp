namespace IntegrationContext.Application.GiteaRepositories.Dtos;

public record GiteaRepositoryDto(
    string ProjectId,
    string RepoOwner,
    string RepoName
);
