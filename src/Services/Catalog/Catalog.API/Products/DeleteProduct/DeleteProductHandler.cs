


namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucess);
    internal class DeleteProductCommandHandler
        (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Hanlde Called by {@Command}", command);

            // Get Product from Database
            var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Id == command.ProductId && x.IsDeleted == false, cancellationToken);
            if (product != null)
            {
                product.IsDeleted = true;
                 session.Store(product);
                await session.SaveChangesAsync(cancellationToken);
                return new DeleteProductResult(product.IsDeleted);
            }
            else
            {
                return new DeleteProductResult(false);
            }
            ;
        }
    }
}
