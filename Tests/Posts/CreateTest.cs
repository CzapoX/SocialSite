using Application.Interfaces;
using Application.Posts;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class CreateTest : BaseTest
    {
        private readonly Mock<IUserAccessor> _userAcessor;

        public CreateTest()
        {
            _userAcessor = new Mock<IUserAccessor>();
        }

        [Fact]
        public async Task ShouldCreatePost()
        {
            var context = GetDataContext();
            var sut = new Create.Handler(context, _mapper, _userAcessor.Object);
            var postToCreate = new PostCreateOrEditDto
            {
                Category = "Animal",
                Description = "Desc",
                Title = "Title"
            };

            var postCommand = new Create.Command
            {
                Post = postToCreate
            };

            await sut.Handle(postCommand, CancellationToken.None);

            var postInDb = context.Posts.FirstOrDefault(x => x.Category == "Animal");

            Assert.NotNull(postInDb);
            Assert.NotEqual(postInDb.CreateDate, DateTime.MinValue);
            Assert.Equal(postToCreate.Description, postInDb.Description);
            Assert.NotEqual(postInDb.Id, Guid.Empty);
        }
    }
}