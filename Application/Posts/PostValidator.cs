using FluentValidation;

namespace Application.Posts
{
    public class PostValidator : AbstractValidator<PostCreateOrEditDto>
    {
        public PostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
