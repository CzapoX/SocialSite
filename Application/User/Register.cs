using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<Result<User>>
        {
            public UserRegisterDto RegisterCredentials { get; set; }
        }

        public class CommandValidator : AbstractValidator<UserRegisterDto>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email can't be empty")
                    .EmailAddress().WithMessage("A valid email address is required.");

                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("Username can't be empty");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password can't be empty")
                    .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$") //Regex from https://stackoverflow.com/questions/48635152/regex-for-default-asp-net-core-identity-password
                    .WithMessage("Minimum 6 characters atleast 1 Upper, 1 Lower case, 1 Number and 1 Special Character and avoid space");
            }
        }

        public class Handler : IRequestHandler<Command, Result<User>>
        {
            public DataContext _context;
            public UserManager<AppUser> _userManager;
            public ITokenService _tokenService;

            public Handler(DataContext context, UserManager<AppUser> userManager, ITokenService tokenService)
            {
                _context = context;
                _userManager = userManager;
                _tokenService = tokenService;
            }

            public async Task<Result<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.AnyAsync(x => x.Email == request.RegisterCredentials.Email))
                    return Result<User>.Failure("Email already exists");

                if (await _context.Users.AnyAsync(x => x.UserName == request.RegisterCredentials.UserName))
                    return Result<User>.Failure("Username already exists");


                var user = new AppUser
                {
                    Email = request.RegisterCredentials.Email,
                    UserName = request.RegisterCredentials.UserName
                };

                var result = await _userManager.CreateAsync(user, request.RegisterCredentials.Password);

                if (result.Succeeded)
                {
                    var appUser =  new User
                    {
                        Token = _tokenService.CreateToken(user),
                        Username = user.UserName
                    };

                    return Result<User>.Success(appUser);
                }

                return Result<User>.Failure("User creating failed");
            }
        }
    }
}
