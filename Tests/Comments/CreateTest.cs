using Application.Interfaces;
using Application.Comments;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.Core;
using Domain;

namespace Tests.Comments
{
    public class CreateTest : BaseTest
    {
        private readonly Mock<IUserAccessor> _userAccessor;
        private readonly Guid postId = Guid.NewGuid();

        public CreateTest()
        {
            _userAccessor = new Mock<IUserAccessor>();
            _userAccessor.Setup(u => u.GetCurrentUserId()).Returns("123");
        }

        [Fact]
        public async Task ShouldCreateComment()
        {
            var context = GetDataContext();
            var sut = new Create.Handler(context, _userAccessor.Object, _mapper);

            context.Posts.Add(new Post { Id = postId, Title = "Test 1" });
            context.Users.Add(new AppUser
            {
                Id = "123",
                Email = "test@test.com",
                UserName = "test"
            });
            context.SaveChanges();

            var commentCommand = new Create.Command
            {
                PostId = postId,
                Content = "Example"
            };

            var result = await sut.Handle(commentCommand, CancellationToken.None);

            Assert.NotNull(result.Value);
            Assert.Equal(ResultStatus.IsSuccess, result.ResultStatus);
            Assert.Equal("Example", result.Value.Content);
        }
    }
}
