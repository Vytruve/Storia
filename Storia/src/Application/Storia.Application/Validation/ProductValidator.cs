using FluentValidation;
using Storia.Application.DTOs;

namespace Storia.Application.Validation
{
    /// <summary>
    /// Define as regras de validação para um ProductDto usando FluentValidation.
    /// </summary>
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("O SKU é obrigatório.")
                .MaximumLength(50).WithMessage("O SKU não pode exceder 50 caracteres.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(p => p.UnitOfMeasure)
                .NotEmpty().WithMessage("A unidade de medida é obrigatória.");
        }
    }
}