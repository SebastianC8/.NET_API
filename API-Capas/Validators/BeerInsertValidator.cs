using FluentValidation;
using Repository.Data.DTO;

namespace API_Capas.Validators
{
    public class BeerInsertValidator: AbstractValidator<BeerInsertDTO>
    {

        public BeerInsertValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("El campo nombre es obligatorio.");
            RuleFor(r => r.Name).Length(2, 20).WithMessage("La longitud del nombre debe estar entre 2 y 20 caracteres");
            RuleFor(r => r.BrandID).NotNull().WithMessage(m => "La marca es obligatoria");
            RuleFor(r => r.BrandID).GreaterThan(0).WithMessage(m => "Error con el ID de la marca");
            RuleFor(r => r.Alcohol).GreaterThan(0).WithMessage(m => "El {PropertyName} debe ser mayor a 0");
        }

    }
}
