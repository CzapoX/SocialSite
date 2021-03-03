using Application.Posts;
using Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class DetailsTest : BaseTest
    {
        private readonly Guid firstId;
        private readonly Guid secondId;

        public DetailsTest()
        {
            firstId = new Guid("1fbf9f1d-9f35-48dd-bcfc-7bcbd61c134c");
            secondId = new Guid("3fbf9f3d-9f55-48dd-bcfc-7bcbd61c1341");
        }

        [Fact]
        public async Task ShouldGetPostDetails()
        {

            var context = GetDataContext();

            var postToFind = new Post
            {
                Id = firstId,
                Title = "First Post",
                Description = "Desc",
                Category = "cat",
                CreateDate = DateTime.Today,
                PostOwner = new AppUser { UserName = "Norbert", Bio = "Bio"}
            };

            var expectedPost = _mapper.Map<PostDto>(postToFind);

            await context.Posts.AddAsync(postToFind);
            await context.Posts.AddAsync(new Post { Id = secondId, Title = "Second Post" });
            await context.SaveChangesAsync();

            var sut = new Details.Handler(context, _mapper);

            var result = sut.Handle(new Details.Query { Id = firstId }, CancellationToken.None).Result.Value;

            Assert.NotNull(result);
            Assert.NotNull(result.PostOwner);
            Assert.Equal(result.Category, expectedPost.Category);
            Assert.Equal(result.Title, expectedPost.Title);
            Assert.Equal(result.CreateDate, expectedPost.CreateDate);
            Assert.Equal(result.Description, expectedPost.Description);
            Assert.Equal(result.PostOwner.Bio, expectedPost.PostOwner.Bio);
            Assert.Equal(result.PostOwner.Username, expectedPost.PostOwner.Username);
        }
    }
}