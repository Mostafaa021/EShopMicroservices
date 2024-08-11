
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string username)  :ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSucess);

    public class DeleteBasketCommandHandler (IBasketRepository basketRepository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand Command, CancellationToken cancellationToken)
        {
            var Cart = await basketRepository.GetBasket(Command.username, cancellationToken);
            if (Cart == null)
                throw new BasketNotFoundException(Command.username);
            else
            {
               var IsBasketDeleted =  await basketRepository.DeleteBasket(Command.username, cancellationToken);
                return new DeleteBasketResult (IsBasketDeleted);
            }
           
        }
    }
}
