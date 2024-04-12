using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetProjectById;

public record GetProjectByIdQuery(string Id) : IQuery<GetProjectByIdResult>;
