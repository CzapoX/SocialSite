using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handle : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handle(DataContext context)
            {
                _context = context;
            }

            async Task<Unit> IRequestHandler<Command, Unit>.Handle(Command request, CancellationToken cancellationToken)
            {
                var postToDelete = await _context.Posts.FindAsync(request.Id);
                _context.Posts.Remove(postToDelete);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
