namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator() 
        {
            RuleFor(x => x.username).NotEmpty().WithMessage("User Name is Required");
        }
        
    }
}
