﻿using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            private readonly ITokenService _tokenService;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenService = tokenService;
            }

            public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .Include(x => x.Photos)
                    .SingleOrDefaultAsync(x => x.Email == request.loginCredentials.Email);

                if (user == null)
                    return Result<User>.Unauthorized($"Invalid email: {request.loginCredentials.Email}");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.loginCredentials.Password, false);

                if (result.Succeeded)
                {
                    var appUser = new User
                    {
                        Token = _tokenService.CreateToken(user),
                        Username = user.UserName,
                        Image = user.Photos?.FirstOrDefault(x => x.IsMain)?.Url
                    };

                    return Result<User>.Success(appUser);
                }
                return Result<User>.Unauthorized("Invalid password");
            }
        }
    }
}
