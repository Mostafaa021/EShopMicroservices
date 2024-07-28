

using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetAllProducts;
using Catalog.API.Products.GetProductById;
using OpenTelemetry.Trace;

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductRequest(Guid Id, string ProductName, List<string> Category, string Descreption, string ImageFile, decimal Price);
    public record UpdateProductResponce(bool IsSucess);

    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Products",
                async (UpdateProductRequest request, ISender sender) =>
            {
                // Adapt UpdateProductRequest to UpdateProductCommand Object 
                var command = request.Adapt<UpdateProductCommand>();
                // MediatR (ISender) Need Command Object and get this result to be ready for RequestHandler 
                var result = await sender.Send(command);
                // if Result  is null 
                if (result.IsSucess == false)
                    return Results.NotFound();
                // Map Result to Responce of Type <T> ==> GetAllProductsResponce
                var Responce = result.Adapt<UpdateProductResponce>();
                // Return Responce
                return Results.Ok(Responce);
            })
            .WithName("UpdateProduct")
            .WithDescription("Product Updated")
            .WithSummary("Product Updated")
            .Produces<UpdateProductResponce>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);

                
        }
    }
}
