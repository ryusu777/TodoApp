using IntegrationContext.Application.Abstractions.Messaging;
using MassTransitContracts.GetAuthProviderUri;

namespace IntegrationContext.Application.Auth.Queries.GetAuthProviderUri;

public record GetAuthProviderUriQuery(string State) : IQuery<GetAuthProviderUriResponse>;
