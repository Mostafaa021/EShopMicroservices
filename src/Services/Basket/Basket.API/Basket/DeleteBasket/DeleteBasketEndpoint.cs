


namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string useranme);
    public record DeleteBasketResponce(bool IsSucess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}", async (string username , ISender sender) =>
            {
                var result =  await sender.Send(new DeleteBasketCommand(username));

                var Responce = result.Adapt<DeleteBasketResponce>();

                return Results.Ok(Responce); 

            })
            .WithName("DeleteCart")
            .WithDescription("Cart Deleted")
            .WithSummary("Cart Deleted")
            .Produces<DeleteBasketResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
