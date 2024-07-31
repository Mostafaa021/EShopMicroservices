
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId): IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            // Get Products from Database
            //var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Id == query.ProductId, cancellationToken);
             // we use Load to retrive specific product by id 
            var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
             // Return product as Product Result 
             if(product is null)
             {
                throw new ProductNotFoundException(query.ProductId);
             }
              return new GetProductByIdResult(product);
        }
    }
}
