using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomain;

public record GetSubdomainQuery(Guid SubdomainId) : IQuery<GetSubdomainResult>;
