using ProjectManagement.Application.Subdomain.Commands.CreateSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomain;

public record CreateSubdomainRequest : CreateSubdomainCommand
{
    public CreateSubdomainRequest(string Title, string Description, string ProjectId) : base(Title, Description, ProjectId)
    {
    }
}

