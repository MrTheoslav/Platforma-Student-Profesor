using DAL;
using FluentValidation;
using MODEL.DTO;

namespace API.Validators
{
    public class LoginUserDtoValidator :AbstractValidator<LoginDto>
    {
        public LoginUserDtoValidator(DataContext dataContext ) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailExist = dataContext.Users.Any(u=>u.Email == value);
                if(!emailExist)
                {
                    context.AddFailure("Emial", "Incorrect email or password");
                }
            });

           //password validation??

        }
    }
}
