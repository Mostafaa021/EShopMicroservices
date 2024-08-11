

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketHandler (IBasketRepository basketRepo)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public  async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //TODO GET Basket From Database
            var basket = await basketRepo.GetBasket(query.UserName, cancellationToken);
            return  new GetBasketResult(basket); 
        }
    }
}
