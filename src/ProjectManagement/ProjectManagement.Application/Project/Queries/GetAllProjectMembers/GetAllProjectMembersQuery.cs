using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetAllProjectMembers;

public record GetAllProjectMembersQuery(string ProjectId) : IQuery<GetAllProjectMembersResult>;
