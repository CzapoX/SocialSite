using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class GetCurrentUser
    {
        public class Query : IRequest<Result<User>> { }

        public class Handler : IRequestHandler<Query, Result<User>>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IUserAccessor _userAccessor;
            private readonly ITokenService _tokenService;

            public Handler(UserManager<AppUser> userManager, IUserAccessor userAccessor, ITokenService tokenService)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
                _tokenService = tokenService;
            }

            public async Task<Result<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

                if (user == null)
                {
                    return null;
                }

                return Result<User>.Success(new User
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user),
                    Image = user.Photos?.FirstOrDefault(x => x.IsMain)?.Url
                });
            }
        }

    }
}
