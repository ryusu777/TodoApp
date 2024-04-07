using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;

public record CreateSubdomainKnowledgeCommand(
    string Title,
    string Content,
    Guid SubdomainId
) : ICommand;
