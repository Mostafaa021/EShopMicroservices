

using Catalog.API.Products.GetAllProducts;

namespace Catalog.API.Products.CreateProduct
{

    public record GetAllProductsRequest();
    public record GetAllProductsResponce(IEnumerable<Product> Products);

    public class GetAllProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products",
                async (ISender sender) =>
            {

                // MediatR (ISender) Need Query Object and get this result to be ready for RequestHandler 
                var result = await sender.Send(new GetAllProductsQuery());

                // Map Result to Responce of Type <T> ==> GetAllProductsResponce
                var Responce = result.Adapt<GetAllProductsResponce>();

                // Return Responce
                return Results.Ok(Responce);
            })
            .WithName("GetAllProducts")
            .WithDescription("Get All Products")
            .WithSummary("Get All Products")
            .Produces<GetAllProductsResponce>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
                
        }
    }
}
