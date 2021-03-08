using Application.Interfaces;
using Application.Posts;
using Domain;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Tests.Posts
{
    public class LikeTest : BaseTest
    {
        private readonly Mock<IUserAccessor> _userAccessor;
        private readonly string _userId = "1";

        public LikeTest()
        {
            _userAccessor = new Mock<IUserAccessor>();
            _userAccessor.Setup(x => x.GetCurrentUserId()).Returns(_userId);
            CreateUser();
        }

        private void CreateUser()
        {
            var context = GetDataContext();

            if (!context.Users.Any())
            {
                var user = new AppUser
                {
                    Id = _userId,
                    UserName = "test",
                    Email = "mail@test.pl"
                };

                context.Add(user);
                context.SaveChanges();
            }
        }

        [Fact]
        public async void ShouldLikePost()
        {
            var context = GetDataContext();
            var sut = new Like.Handler(context, _userAccessor.Object);

            var post = new Post {Title = "test1" };
            context.Add(post);
            context.SaveChanges();


            var likeCommand = new Like.Command
            {
                Id = post.Id
            };

            await sut.Handle(likeCommand, CancellationToken.None);

            var postAfterCommand = context.Posts.Find(post.Id);

            Assert.Equal(1, postAfterCommand.PostLikers.Count);
        }

        [Fact]
        public async void ShouldUnlikePost()
        {
            var context = GetDataContext();
            var sut = new Like.Handler(context, _userAccessor.Object);

            var post = new Post {Title = "test2" };
            context.Add(post);

            var postLiker = new PostLiker
            {
                AppUserId = _userId,
                PostId = post.Id
            };
            context.Add(postLiker);

            context.SaveChanges();


            var likeCommand = new Like.Command
            {
                Id = post.Id
            };

            await sut.Handle(likeCommand, CancellationToken.None);

            var postAfterCommand = context.Posts.Find(post.Id);

            Assert.Equal(0, postAfterCommand.PostLikers.Count);
        }
    }
}