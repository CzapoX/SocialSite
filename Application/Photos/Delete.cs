using Application.Core;
using Application.Interfaces;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler(DataContext context, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var photo = await _context.Photos.FindAsync(request.Id);

                if (photo == null)
                    return null;
                if (photo.IsMain)
                    return Result<Unit>.Failure("Main photo can't be deleted");

                var result = await _photoAccessor.DeletePhoto(photo.Id.ToString());

                if (result == false)
                    return Result<Unit>.Failure("Delete image from Azure failed");

                _context.Remove(photo);
                var contextResult = await _context.SaveChangesAsync() > 0;

                if (contextResult)
                    return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Delete image failed");
            }
        }
    }
}
