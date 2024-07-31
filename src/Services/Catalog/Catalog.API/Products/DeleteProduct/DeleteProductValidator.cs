
using Catalog.API.Products.DeleteProduct;

namespace Catalog.API.Products.CreateProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id is Required");
        }
    }
}
