using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomain;

public record DeleteSubdomainRequest : DeleteSubdomainCommand
{
    public required Guid subdomain_id { get; set; }
    public DeleteSubdomainRequest(Guid SubdomainId) : base(SubdomainId)
    {
    }
}

