using IntegrationContext.Application.Abstractions.Messaging;

namespace IntegrationContext.Application.Auth.Queries.GetClientCredential;

public record GetClientCredentialQuery() : IQuery<GetClientCredentialResult>;
