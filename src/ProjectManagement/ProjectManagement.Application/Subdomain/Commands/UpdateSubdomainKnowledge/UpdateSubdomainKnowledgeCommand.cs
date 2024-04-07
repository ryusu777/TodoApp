using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Commands.UpdateSubdomainKnowledge;

public record UpdateSubdomainKnowledgeCommand(
    Guid SubdomainKnowledgeId,
    string Title,
    string Content,
    Guid SubdomainId
) : ICommand;
