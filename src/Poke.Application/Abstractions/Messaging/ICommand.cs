using MediatR;

namespace Poke.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
