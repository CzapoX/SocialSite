using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Post Post { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                var postToEdit =  await _context.Posts.FindAsync(request.Post.Id);
                _mapper.Map(request.Post, postToEdit);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
