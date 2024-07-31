
namespace Catalog.API.Products.GetAllProducts
{
    public record GetAllProductsQuery(int? PageNumber = 1, int? PageSize = 10)
        : IQuery<GetAllProductsResult>;
    public record GetAllProductsResult(IEnumerable<Product> Products);
    internal class GetAllProductsQuerytHandler
        (IDocumentSession session)
        : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            // Get Products from Database
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10 ,cancellationToken);

            // Return products as Product Result 
            return new GetAllProductsResult(products);
        }
    }
}
