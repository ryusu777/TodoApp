using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetProjectPages;

public record GetProjectPagesQuery(): IQuery<IEnumerable<GetProjectPagesResult>>;
