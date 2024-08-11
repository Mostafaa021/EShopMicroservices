

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName) ;
    public record GetBasketResponce(ShoppingCart ShoppingCart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Basket/{username}",
               async (string username,  ISender sender) =>
                {
                    // Map from Query Object to Result 
                    var result = await sender.Send(new GetBasketQuery(username) );
                    // Then Map from Result to Responce
                    var Responce = result.Adapt<GetBasketResponce>();
                    // Return Result
                    return Results.Ok(Responce);
                })
                .WithName("GetBasket")
                .WithDescription("GetBasket")
                .WithSummary("GetBasket")
                .Produces<GetBasketResponce>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
