using MediatR;

namespace DeveloperExam.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
