
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler ()
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;  
            //var Cart = await session.LoadAsync<ShoppingCart>(command.Cart, cancellationToken);

            // if Not Data then Insert else Update
            // Update Cached Data

            //if(Cart is not  null )
            //{
            //    // Means Update
            //    Cart.UserName = command.Cart.UserName;
            //    Cart.Items = command.Cart.Items;
            //    // Then update and save to database
            //    session.Store(Cart);
            //    await session.SaveChangesAsync();

            //}
            //else
            //{
            //    // Means Create new cart 
            //    var NewCart = new ShoppingCart()
            //    {
            //        Items = command.Cart.Items,
            //        UserName = command.Cart.UserName,
            //    };
            //    session.Store(Cart!);
            //   //Save to Database 
            //    await session.SaveChangesAsync(cancellationToken);
            //}

            return new StoreBasketResult("swn");
        }
    }
}
