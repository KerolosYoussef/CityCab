using MediatR;

namespace CityCab.Common.Interfaces.CQRS.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull { }
}
