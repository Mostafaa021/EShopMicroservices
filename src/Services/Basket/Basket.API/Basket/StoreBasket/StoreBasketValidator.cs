
namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().NotEmpty().WithMessage("Cart Can`t be null or Empty");
            RuleFor(x => x.Cart.UserName).NotNull().NotEmpty().WithMessage("User Name is Required");
        }
    }
}
