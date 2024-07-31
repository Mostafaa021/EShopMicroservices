


namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string ProductName ,List<string> Category, string Descreption, string ImageFile , decimal Price )
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);


    internal class CreateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand CommandRequest, CancellationToken cancellationToken)
        {
            // Create Product Enity from Command object
            var Product = new Product()
            {
                ProductName = CommandRequest.ProductName,
                Descreption = CommandRequest.Descreption,
                ImageFile = CommandRequest.ImageFile,
                Price = CommandRequest.Price ,
                Category = CommandRequest.Category
            };
  
            // When Using with Marten Library for Postgres Document DB => We Deal with (DocumentSession) as Unit of Work for Querying and Command => Base Class (IDocumentStore)
            // IQuerySession Only for Querying Data with Notracking =>  Base Class (IDocumentStore)
            //DocumentSession=> 1)IdentityMapDocumentSession, 2)LightWeightDocumentSession , 3)DirtyCheckDocumentSession
            session.Store(Product);
            // Save to Database 
            await session.SaveChangesAsync(cancellationToken);

            // Return CreateProductResult from Handle 
              return new CreateProductResult(Product.Id);

        }
    }
}
