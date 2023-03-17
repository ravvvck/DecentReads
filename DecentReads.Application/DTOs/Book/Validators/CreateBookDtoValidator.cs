using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecentReads.Application.DTOs.Book.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Name).MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.AuthorLastName).NotNull();
            RuleFor(x => x.AuthorFirstName).NotNull();
            RuleFor(x => x.Description).NotNull().MaximumLength(250);
        }
    }
}
