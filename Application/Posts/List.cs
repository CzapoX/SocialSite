using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PostDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<PostDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<PostDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts
                    .Include(x => x.PostOwner)
                    .Include(x => x.PostLikers).ThenInclude(x => x.AppUser).ThenInclude(x => x.Photos).AsSplitQuery()
                    .ToListAsync();

                List<PostDto> postsDtos = new List<PostDto>();

                foreach (var post in posts)
                {
                    postsDtos.Add(_mapper.Map<PostDto>(post));
                }

                var pagedList = PagedList<PostDto>
                    .Create(postsDtos.AsQueryable(), request.Params.PageNumber, request.Params.PageSize);

                return Result<PagedList<PostDto>>.Success(pagedList);
            }
        }
    }
}
