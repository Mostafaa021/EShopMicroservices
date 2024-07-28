

using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetAllProducts;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductRequest(Guid id);
    public record DeleteProductResponce(bool IsSucess);

    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/{id}",
                async (Guid id, ISender sender) =>
            {
                // MediatR (ISender) Need Query Object and get this result to be ready for RequestHandler 
                var result = await sender.Send(new DeleteProductCommand(id));
                // if Result  is null 
                if (result.IsSucess == false)
                    return Results.NotFound(result);
                // Map Result to Responce of Type <T> ==> GetAllProductsResponce
                var Responce = result.Adapt<DeleteProductResponce>();

                // Return Responce
                return Results.Ok(Responce);
            })
            .WithName("DeleteProduct")
            .WithDescription("Product Deleted")
            .WithSummary("Product Deleted")
            .Produces<DeleteProductResponce>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
                
        }
    }
}
