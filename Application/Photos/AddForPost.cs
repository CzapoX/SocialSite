using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class AddForPost
    {
        public class Command : IRequest<Result<Photo>>
        {
            public Guid PostId { get; set; }
            public IFormFile Image { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler(DataContext context, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
            }

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == request.PostId);

                if (post == null)
                    return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.Image);

                var photo = new Photo { Id = photoUploadResult.Id, Url = photoUploadResult.Url };

                if (!post.Photos.Any(x => x.IsMain == true))
                    photo.IsMain = true;

                await _context.Photos.AddAsync(photo);
                post.Photos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;

                if (result == true)
                    return Result<Photo>.Success(photo);
                else
                    return Result<Photo>.Failure("Error adding photo");
            }
        }
    }
}
