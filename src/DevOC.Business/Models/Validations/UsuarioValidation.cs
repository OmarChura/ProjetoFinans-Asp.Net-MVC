using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOC.Business.Models.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario> 
    {
        public UsuarioValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Login)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(4, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(6, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .EmailAddress();

            RuleFor(f => f.Celular)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                ;

            RuleFor(f => f.Perfil)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

           
        }
    }
}
