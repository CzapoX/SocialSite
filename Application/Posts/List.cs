using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<Result<List<Post>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Post>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Post>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts.ToListAsync();

                return Result<List<Post>>.Success(posts);
            }
        }
    }
}
