using MediatR;


namespace BuildingBlocks.Abstractions.CQRS
{
    public interface ICommand<out TResponce> : IRequest<TResponce>  // Generic Type of ICommand
    {
    }
    public interface ICommand : ICommand<Unit>  // Empty or no generic type of ICommand
    {
    }
}
