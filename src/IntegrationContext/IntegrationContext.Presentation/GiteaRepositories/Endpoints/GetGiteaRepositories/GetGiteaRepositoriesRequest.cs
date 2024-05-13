namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.GetGiteaRepositories;

public record GetGiteaRepositoriesRequest(
    string? SearchText,
    int? Page,
    int? ItemPerPage
);

