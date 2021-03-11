using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

                if (user == null)
                    return null;

                var photoToSetToMain = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if (photoToSetToMain == null)
                    return null;

                var mainPhoto = user.Photos.FirstOrDefault(x => x.IsMain == true);

                if (mainPhoto != null)
                    mainPhoto.IsMain = false;

                photoToSetToMain.IsMain = true;

                var result = await _context.SaveChangesAsync() > 0;

                if (result)
                    return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Error setting main photo");
            }
        }
    }
}
