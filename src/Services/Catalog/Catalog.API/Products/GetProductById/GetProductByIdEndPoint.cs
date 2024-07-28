

using Catalog.API.Products.GetAllProducts;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.CreateProduct
{

    public record GetProductByIdRequest(Guid id);
    public record GetProductByIdResponce(Product Product);

    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}",
                async (Guid id, ISender sender) =>
            {
                // MediatR (ISender) Need Query Object and get this result to be ready for RequestHandler 
                var result = await sender.Send(new GetProductByIdQuery(id));

                // if Result  is null 
                if (result.Product is null)
                    return Results.NotFound(result.Product);
                // Map Result to Responce of Type <T> ==> GetAllProductsResponce
                var Responce = result.Adapt<GetProductByIdResponce>();
                // Return Responce
                return Results.Ok(Responce);
            })
            .WithName("GetProductById")
            .WithDescription("Get Product By Id")
            .WithSummary("Get Product By Id")
            .Produces<GetProductByIdResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);


        }
    }
}
