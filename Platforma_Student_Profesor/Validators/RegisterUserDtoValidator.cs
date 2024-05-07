using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

using System.Threading.Tasks;
using DAL;

namespace MODEL.DTO.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {

        public RegisterUserDtoValidator(DataContext dataContext) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x=>x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dataContext.Users.Any(e => e.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
            });



        }



    }
}
