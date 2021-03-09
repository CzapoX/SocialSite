using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class AddForUser
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile Image { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.Id == _userAccessor.GetCurrentUserId());

                if (user == null)
                    return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.Image);

                var photo = new Photo { Id = photoUploadResult.Id, Url = photoUploadResult.Url };


                if (!user.Photos.Any(x => x.IsMain == true))
                    photo.IsMain = true;

                //_context.Photos.Add(photo);
                user.Photos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == true)
                    return Result<Photo>.Success(photo);
                else
                    return Result<Photo>.Failure("Error adding photo");
            }
        }
    }
}
