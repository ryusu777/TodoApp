using Library.Models;
using MediatR;

namespace ProjectManagement.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}


