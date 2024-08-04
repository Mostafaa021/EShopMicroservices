
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string username)  :ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSucess);

    public class DeleteBasketCommandHandler ()
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand Command, CancellationToken cancellationToken)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                UserName = Command.username,
            };
            //var cart =  await session.LoadAsync<ShoppingCart>(Command.Cart);

            return new DeleteBasketResult(true);
        }
    }
}
