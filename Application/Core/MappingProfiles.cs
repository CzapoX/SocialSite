using Application.Posts;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<PostCreateOrEditDto, Post>();
            CreateMap<Post, PostDto>();
            CreateMap<AppUser, Profiles.Profile>();
        }
    }
}
