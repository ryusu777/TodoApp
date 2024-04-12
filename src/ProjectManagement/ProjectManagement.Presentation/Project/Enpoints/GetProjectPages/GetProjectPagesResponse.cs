using ProjectManagement.Application.Project.Queries.GetProjectPages;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectPages;

public record GetProjectPagesResponse(
    IEnumerable<GetProjectPagesResult>? Data,
    string? ErrorDescription
): IApiResponse<IEnumerable<GetProjectPagesResult>>;

