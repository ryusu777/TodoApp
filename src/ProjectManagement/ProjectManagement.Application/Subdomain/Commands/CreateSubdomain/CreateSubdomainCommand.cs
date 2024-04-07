using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Commands.CreateSubdomain;

public record CreateSubdomainCommand(
    string Title, 
    string Description,
    string ProjectId) : ICommand;
