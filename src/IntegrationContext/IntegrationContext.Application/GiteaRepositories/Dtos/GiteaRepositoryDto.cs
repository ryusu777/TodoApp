namespace IntegrationContext.Application.GiteaRepositories.Dtos;

public record GiteaRepositoryDto(
    int Id,
    string RepoOwner,
    string RepoName
);
