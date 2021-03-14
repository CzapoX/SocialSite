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

namespace Application.Comments
{
    public class Create
    {
        public class Command:IRequest<Result<CommentDto>>
        {
            public string Content { get; set; }
            public Guid PostId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Content).NotEmpty().WithMessage("Comment must have content");
            }
        }

        public class Handler : IRequestHandler<Command, Result<CommentDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.PostId);

                if (post == null)
                    return null;

                var user = await _context.Users.FindAsync(_userAccessor.GetCurrentUserId());

                if (user == null)
                    return null;

                var comment = new Comment
                {
                    Author = user,
                    Content = request.Content,
                    Post = post,
                    CreateDate = DateTime.UtcNow
                };

                await _context.AddAsync(comment);

                var result = await _context.SaveChangesAsync() > 0;

                if (result)
                    Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

                return Result<CommentDto>.Failure("Error adding comment");
            }
        }
    }
}
