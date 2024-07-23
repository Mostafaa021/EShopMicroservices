

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string ProductName, List<string> Categories, string Descroption, string ImageFile, decimal Price);
    public record CreateProductResponce(Guid Id);

    public class CreatePRoductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Products",
                async (CreateProductRequest request, ISender sender) =>
            {
                // Mapping Incoming Request to Command Object to Be used by MediatR (ISender)
                var Command = request.Adapt<CreateProductCommand>();

                // MediatR (ISender) Need Command Object and get this result to be ready for RequestHandler 
                var result = await sender.Send(Command);

                // Map Result to Responce of Type <T> ==> CreateProductResponce 
                var Responce = result.Adapt<CreateProductResponce>();

                // Return Status 201 Created
                return Results.Created($"/Products/{Responce.Id}", Responce);
            })
            .WithName("CreateProduct")
            .WithDescription("Create Product")
            .WithSummary("Create Product")
            .Produces<CreateProductResponce>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
                
        }
    }
}
