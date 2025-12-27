using MediatR;

namespace CityCab.Common.Interfaces.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    { }
}
