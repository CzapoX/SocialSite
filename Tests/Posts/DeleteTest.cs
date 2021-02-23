using Application.Posts;
using Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class DeleteTest : BaseTest
    {
        [Fact]
        public async Task ShouldDeleteActivity()
        {
            var context = GetDataContext();
            var sut = new Delete.Handler(context);
            var postToDelete = new Post
            {
                Category = "Animal",
                CreateDate = DateTime.Today,
                Description = "Desc",
                Title = "Title"
            };

            await context.AddAsync(postToDelete);
            await context.SaveChangesAsync();

            var idToDelete = postToDelete.Id;

            var deleteCommand = new Delete.Command
            {
                Id = idToDelete
            };

            await sut.Handle(deleteCommand, CancellationToken.None);

            var postInDb = context.Posts.FirstOrDefault(x=>x.Id == idToDelete);

            Assert.Null(postInDb);
        }
    }
}
