using Application.Core;
using AutoMapper;
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
        public class Query : IRequest<Result<List<PostDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<PostDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<PostDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var posts = await _context.Posts.Include(x => x.PostOwner).ToListAsync();

                List<PostDto> postsDtos = new List<PostDto>();

                foreach (var post in posts)
                {
                    postsDtos.Add(_mapper.Map<PostDto>(post));
                }

                return Result<List<PostDto>>.Success(postsDtos);
            }
        }
    }
}
