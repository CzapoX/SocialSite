using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class Details
    {
        public class Query : IRequest<Result<PostDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PostDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PostDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts
                    .Include(x => x.PostOwner)
                    .Include(x => x.PostLikers).ThenInclude(x => x.AppUser).ThenInclude(x => x.Photos).AsSplitQuery()
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<PostDto>.Success(_mapper.Map<PostDto>(post));
            }
        }
    }
}
