using ProjectManagement.Domain.Assignment.Entities;

namespace ProjectManagement.Application.Assignment.Dtos;

public record Assignment(
    Guid Id,
    string Title,
    string? Description,
    string ProjectId,
    string Status,
    Guid? SubdomainId,
    Guid? PhaseId,
    ICollection<string> Assignees,
    string? Reviewer,
    DateTime? Deadline,
    Review? LastReview
);

