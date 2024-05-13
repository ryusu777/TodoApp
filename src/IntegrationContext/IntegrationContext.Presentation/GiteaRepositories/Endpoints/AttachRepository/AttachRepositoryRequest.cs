namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.AttachRepositoryEndpoint;

public record AttachRepositoryRequest(
    string ProjectId, string RepoOwner, string RepoName);
