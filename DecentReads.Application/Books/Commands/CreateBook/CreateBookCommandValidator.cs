using DecentReads.Application.Authentication.Commands.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.Books.Commands.CreateBook
{
 

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.AuthorFullName).NotEmpty();

            
        }
    }
}
