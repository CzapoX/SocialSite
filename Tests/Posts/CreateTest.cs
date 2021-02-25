using Application.Core;
using Application.Posts;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateTest()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task ShouldCreateActivity()
        {
            var context = GetDataContext();
            var sut = new Create.Handler(context, _mapper);
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