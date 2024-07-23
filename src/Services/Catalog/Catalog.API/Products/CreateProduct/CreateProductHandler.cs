

using Microsoft.AspNetCore.Http.HttpResults;
using Catalog.API.Models;
using System.Windows.Input;
using BuildingBlocks.Abstractions.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string ProductName ,List<string> Categories , string Descroption , string ImageFile , decimal Price )
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProducCommandtHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand CommandRequest, CancellationToken cancellationToken)
        {
            // Create Product Enity from Command object
            var Product = new Product()
            {
                ProductName = CommandRequest.ProductName,
                Descroption = CommandRequest.Descroption,
                ImageFile = CommandRequest.ImageFile,
                Price = CommandRequest.Price
            };

            // Save to Database 
            // TODO

            // Return CreateProductResult from Handle 

                return new CreateProductResult(Guid.NewGuid());

        }
    }
}
