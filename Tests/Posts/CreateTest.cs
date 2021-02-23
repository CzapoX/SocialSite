using Application.Posts;
using Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class CreateTest : BaseTest
    {
        [Fact]
        public async Task ShouldCreateActivity()
        {
            var context = GetDataContext();
            var sut = new Create.Handler(context);
            var postToCreate = new Post
            {
                Category = "Animal",
                CreateDate = DateTime.Today,
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
            Assert.Equal(postInDb.CreateDate, postToCreate.CreateDate);
            Assert.Equal(postToCreate.Description, postInDb.Description);
            Assert.NotEqual(postInDb.Id, Guid.Empty);
        }
    }
}