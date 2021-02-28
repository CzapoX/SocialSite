using Application.Core;
using Application.Services;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    class Login
    {
        public class Query : IRequest<Result<User>>
        {
            public UserLoginDto UserLogin { get; set; }
        }

        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserLogin.Email)
                    .NotEmpty().WithMessage("Email can't be empty")
                    .EmailAddress().WithMessage("A valid email address is required.");

                RuleFor(x => x.UserLogin.Password)
                    .NotEmpty().WithMessage("Password can't be empty");
            }
        }

        public class Handler : IRequestHandler<Query, Result<User>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly TokenService _tokenService;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenService = tokenService;
            }

            public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.UserLogin.Email);

                if (user == null)
                    return Result<User>.Unauthorized($"Invalid email: {request.UserLogin.Email}");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.UserLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = new User
                    {
                        Token = _tokenService.CreateToken(user),
                        Username = user.UserName,
                    };

                    return Result<User>.Success(appUser);
                }
                return Result<User>.Unauthorized("Invalid password");
            }
        }
    }
}
