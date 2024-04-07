using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;

public record DeleteSubdomainCommand(Guid SubdomainId) : ICommand;

