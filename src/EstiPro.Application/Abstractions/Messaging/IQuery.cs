using MediatR;

namespace EstiPro.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
