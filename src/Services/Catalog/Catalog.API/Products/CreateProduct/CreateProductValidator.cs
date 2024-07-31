

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id  is Required");
        }
    }
}
