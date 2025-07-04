using FluentValidation;
using Storia.Application.DTOs;

namespace Storia.Application.Validation
{
    /// <summary>
    /// Define as regras de valida��o para um ProductDto usando FluentValidation.
    /// </summary>
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do produto � obrigat�rio.")
                .MaximumLength(200).WithMessage("O nome n�o pode exceder 200 caracteres.");

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("O SKU � obrigat�rio.")
                .MaximumLength(50).WithMessage("O SKU n�o pode exceder 50 caracteres.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O pre�o deve ser maior que zero.");

            RuleFor(p => p.UnitOfMeasure)
                .NotEmpty().WithMessage("A unidade de medida � obrigat�ria.");
        }
    }
}