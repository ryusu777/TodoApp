using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Commands.UpdateSubdomain;

public record UpdateSubdomainCommand(
    Guid SubdomainId, 
    string Title,
    string Description) : ICommand;
