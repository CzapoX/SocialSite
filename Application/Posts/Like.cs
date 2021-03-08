using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class Like
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id;
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
                var userId = _userAccessor.GetCurrentUserId();
                var postId = request.Id;

                var post = await _context.Posts
                    .Include(x => x.PostLikers)
                    .FirstOrDefaultAsync(x => x.Id == postId);

                if (post == null)
                    return null;

                if (post.PostLikers.Any(x => x.AppUserId == userId))
                {
                    var postLiker = await _context.PostLikers
                        .FirstOrDefaultAsync(x => x.AppUserId == userId 
                            && x.PostId == postId);
                    
                    if (postLiker == null)
                        return null;
                    
                    _context.Remove(postLiker);
                    await _context.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value);
                }

                else
                {
                    var postLiker = new PostLiker
                    {
                        PostId = postId,
                        AppUserId = userId
                    };

                    await _context.AddAsync(postLiker);
                    await _context.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value);
                }

            }
        }
    }
}
