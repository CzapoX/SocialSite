using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Posts
{
    public class Create
    {
        public class Command : IRequest
        {
            public PostCreateOrEditDto Post { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Post).SetValidator(new PostValidator());
            }
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

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                Post postToCreate = _mapper.Map<Post>(request.Post);
                postToCreate.CreateDate = DateTime.Now;
                await _context.Posts.AddAsync(postToCreate);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
