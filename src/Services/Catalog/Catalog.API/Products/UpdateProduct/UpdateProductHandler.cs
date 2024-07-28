


namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string ProductName, List<string> Category, string Descreption, string ImageFile, decimal Price)
       : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSucess);
    internal class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Hanlde Called by {@Query}", command);

            // Get Product from Database
            var product = await session.LoadAsync<Product>(command.Id,cancellationToken);
            if (product is not null)
            {
                
                product.Id = command.Id;
                product.ImageFile = command.ImageFile;
                product.Price = command.Price;
                product.Category = command.Category;
                product.Descreption = command.Descreption;
                product.ProductName = command.ProductName;
                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);
                return new UpdateProductResult(true);
            }
            else
            {
                return new UpdateProductResult(false);
                //throw new ProductNotFoundException();
            }
            

            
             // Return product as Product Result 
            
        }
    }
}
