
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GeTProductByCategoryRequest(string Category);
    public record GeTProductByCategoryResponce(IEnumerable<Product> Products);

    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{Category}", 
                async (string Category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(Category));

                var responce = result.Adapt<GeTProductByCategoryResponce>();

                return Results.Ok(responce);

            })
            .WithName("GetPRoductByCategory")
            .WithDescription("Get Product By Category")
            .WithSummary("Get Product By Category")
            .Produces<GetProductByIdResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }

    }
}
