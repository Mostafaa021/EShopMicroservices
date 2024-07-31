using MediatR;

namespace BuildingBlocks.Abstractions.CQRS
{
    public interface IQuery<out TResponce> : IRequest<TResponce>
        where TResponce : notnull
    {
    }
}
