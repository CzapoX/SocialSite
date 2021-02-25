using Application.Posts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class ListTest : BaseTest
    {
        [Fact]
        public void ShouldReturnListOfActivity()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestListDatabase").Options;
            var context = new DataContext(options);

            context.Posts.Add(new Post { Title = "Post 1", });
            context.Posts.Add(new Post { Title = "Post 2" });
            context.SaveChanges();

            var sut = new List.Handler(context);
            var result = sut.Handle(new List.Query(), CancellationToken.None).Result.Value;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Post 1", result[0].Title);
            Assert.Equal("Post 2", result[1].Title);
        }
    }
}
