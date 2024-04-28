using Library.Models;
using MediatR;

namespace IntegrationContext.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}


