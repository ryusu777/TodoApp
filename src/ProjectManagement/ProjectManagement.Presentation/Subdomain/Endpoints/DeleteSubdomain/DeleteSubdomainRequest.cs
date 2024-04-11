using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomain;

public record DeleteSubdomainRequest : DeleteSubdomainCommand
{
    public DeleteSubdomainRequest(Guid SubdomainId) : base(SubdomainId)
    {
    }
}

