using MediatR;

namespace CityCab.Common.Interfaces.CQRS.Commands
{
    public interface ICommand : IRequest<Unit> { }
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}
