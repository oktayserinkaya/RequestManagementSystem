using FluentValidation;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.FluentValidation.RequestValidators
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestVM>
    {
        public CreateRequestValidator()
        {
            RuleFor(x => x.Amount).NotEmpty()
              .WithMessage("Bu alan zorunludur!").GreaterThan(0).WithMessage("0'dan büyük bir değer girmelisiniz");
        }
    }
}
