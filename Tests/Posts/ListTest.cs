using Application.Core;
using Application.Posts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
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
            var param = new PagingParams();

            var postOwnerName = "Norbert";
            context.Posts.Add(new Post { Title = "Post 1", PostOwner = new AppUser {UserName = postOwnerName } });
            context.Posts.Add(new Post { Title = "Post 2" });
            context.SaveChanges();

            var sut = new List.Handler(context, _mapper);
            var result = sut.Handle(new List.Query { Params = param }, CancellationToken.None).Result.Value;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Post 1", result[0].Title);
            Assert.Equal("Post 2", result[1].Title);
            Assert.Equal(result[0].PostOwner.Username, postOwnerName);
        }
    }
}
