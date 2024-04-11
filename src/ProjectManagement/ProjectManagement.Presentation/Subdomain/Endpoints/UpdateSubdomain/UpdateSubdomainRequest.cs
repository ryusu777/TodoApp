using ProjectManagement.Application.Subdomain.Commands.UpdateSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomain;

public record UpdateSubdomainRequest : UpdateSubdomainCommand
{
    public UpdateSubdomainRequest(Guid SubdomainId, string Title, string Description) : base(SubdomainId, Title, Description)
    {
    }
}

