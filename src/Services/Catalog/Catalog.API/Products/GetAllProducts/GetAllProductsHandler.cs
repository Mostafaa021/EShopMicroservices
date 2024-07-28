


namespace Catalog.API.Products.GetAllProducts
{
    public record GetAllProductsQuery()
        : IQuery<GetAllProductsResult>;
    public record GetAllProductsResult(IEnumerable<Product> Products);
    internal class GetAllProductsQuerytHandler
        (IDocumentSession session, ILogger<GetAllProductsQuerytHandler> logger)
        : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetAllProductsQuerytHandler.Hanlde Called by {@Query}", query);

            // Get Products from Database
            var products = await session.Query<Product>().ToListAsync(cancellationToken);

            // Return products as Product Result 
            return new GetAllProductsResult(products);
        }
    }
}
