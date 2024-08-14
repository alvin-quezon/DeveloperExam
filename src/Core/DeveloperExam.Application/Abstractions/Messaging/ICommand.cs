using MediatR;

namespace DeveloperExam.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>;