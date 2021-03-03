using Application.Core;
using Application.Interfaces;
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
        public class Command : IRequest<Result<Unit>>
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

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                Post postToCreate = _mapper.Map<Post>(request.Post);
                postToCreate.CreateDate = DateTime.Now;
                postToCreate.PostOwnerId = _userAccessor.GetCurrentUserId();

                await _context.Posts.AddAsync(postToCreate);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to create post");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
