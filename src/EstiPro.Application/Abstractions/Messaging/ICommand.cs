using MediatR;

namespace EstiPro.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;
