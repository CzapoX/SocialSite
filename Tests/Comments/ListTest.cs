using Application.Comments;
using Domain;
using System.Threading;
using Xunit;
using System;
using System.Collections.Generic;

namespace Tests.Comments
{
    public class ListTest : BaseTest
    {
        private readonly Guid postId = Guid.NewGuid();

        [Fact]
        public void ShouldReturnListOfComments()
        {
            var context = GetDataContext();

            context.Users.Add(new AppUser
            {
                Id = "1",
                Email = "test@test.com",
                UserName = "test",
                Bio ="testBio",
                Photos = new List<Photo>()
            });

            context.Posts.Add(new Post { Id = postId, Title = "Post with Comments", PostOwnerId = "1" });
            context.Comments.Add(new Comment { Content = "test1", AuthorId = "1", PostId = postId, CreateDate = DateTime.UtcNow });
            context.Comments.Add(new Comment { Content = "test2", AuthorId = "1", PostId = postId, CreateDate = DateTime.UtcNow });

            context.SaveChanges();

            var sut = new List.Handler(context, _mapper);
            var result = sut.Handle(new List.Query { PostId = postId}, CancellationToken.None).Result.Value;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("test1", result[0].Content);
            Assert.Equal("test2", result[1].Content);
            Assert.Equal("test", result[0].Author.Username);
        }
    }
}
