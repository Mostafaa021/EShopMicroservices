
namespace Basket.API.Basket.StoreBasket
{
     public record StoreBasketRequest(ShoppingCart Cart); 
     public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket"
                , async (StoreBasketRequest request, ISender sender) =>
                {
                    var command = request.Adapt<StoreBasketCommand>();

                    StoreBasketResult result =  await sender.Send(command);

                    StoreBasketResponse responce = result.Adapt<StoreBasketResponse>();


                    return Results.Created($"/basket/{responce.UserName}", responce);
                })
                .WithName("CreateCart")
                .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Cart")
                .WithDescription("Create Cart");
        }
    }
}
