using Application.Core;
using Application.Posts;
using AutoMapper;
using Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Posts
{
    public class EditTest : BaseTest
    {
        private readonly IMapper _mapper;
        private readonly Guid id;

        public EditTest()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
            _mapper = mockMapper.CreateMapper();

            id = new Guid("1fbf9f1d-9f35-483d-bcfc-7acbd63c134c");
        }

        [Fact]
        public async Task ShouldEditPost()
        {
            var context = GetDataContext();

            await context.Posts.AddAsync(new Post { Id = id, Title = "Unedited Post" });
            await context.SaveChangesAsync();

            var editedPost = new PostCreateOrEditDto { Id = id, Title = "Edited Post" };

            var sut = new Edit.Handler(context, _mapper);

            await sut.Handle(new Edit.Command { Post = editedPost }, CancellationToken.None);

            var post = await context.Posts.FindAsync(id);

            Assert.NotNull(post);
            Assert.Equal("Edited Post", post.Title);
            Assert.NotEqual(post.EditDate, DateTime.MinValue);
        }
    }
}