

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is Required");
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Name is Required")
                .Length(4 ,50).WithMessage("Name must be between 4 and 50 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
