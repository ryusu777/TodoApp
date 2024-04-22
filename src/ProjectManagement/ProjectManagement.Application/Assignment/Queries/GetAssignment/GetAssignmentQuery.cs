using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Queries.GetAssignment;

public record GetAssignmentQuery(Guid AssignmentId) : IQuery<Dtos.Assignment>;
