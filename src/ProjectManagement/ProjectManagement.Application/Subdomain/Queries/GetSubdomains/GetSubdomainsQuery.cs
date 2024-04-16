using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomains;

public record GetSubdomainsQuery(string ProjectId) : IQuery<GetSubdomainsResult>;
