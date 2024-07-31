using MediatR;


namespace BuildingBlocks.Abstractions.CQRS
{
    public interface ICommandHandler<in TCommand , TResponce> 
        :  IRequestHandler<TCommand , TResponce>
        where TCommand : ICommand<TResponce>
        where TResponce : notnull
    {
    }

    public interface ICommandHandler<in TCommand> 
        : ICommandHandler<TCommand, Unit>   //  unit if there is No responce from Command Object
        where TCommand : ICommand<Unit>    
    {
    }

}
