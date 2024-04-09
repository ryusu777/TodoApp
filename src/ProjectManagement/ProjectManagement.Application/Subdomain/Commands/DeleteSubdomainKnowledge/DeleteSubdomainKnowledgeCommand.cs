namespace ProjectManagement.Application.Subdomain.Commands.DeleteSubdomainKnowledge;

using ProjectManagement.Application.Abstractions.Messaging;

public record DeleteSubdomainKnowledgeCommand(Guid SubdomainId, Guid SubdomainKnowledgeId) : ICommand;
