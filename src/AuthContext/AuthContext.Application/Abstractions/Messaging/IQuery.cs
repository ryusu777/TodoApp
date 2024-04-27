using Library.Models;
using MediatR;

namespace AuthContext.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}


