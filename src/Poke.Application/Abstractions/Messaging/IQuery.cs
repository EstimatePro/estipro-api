using MediatR;

namespace Poke.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
