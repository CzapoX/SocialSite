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
    public class Login
    {
        public class Query : IRequest<Result<User>>
        {
            public UserLoginDto loginCredentials { get; set; }
        }

        public class QueryValidator : AbstractValidator<UserLoginDto>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email can't be empty")
                    .EmailAddress().WithMessage("A valid email address is required.");

                RuleFor(x => x.Password)
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
                var user = await _userManager.FindByEmailAsync(request.loginCredentials.Email);

                if (user == null)
                    return Result<User>.Unauthorized($"Invalid email: {request.loginCredentials.Email}");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.loginCredentials.Password, false);

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
