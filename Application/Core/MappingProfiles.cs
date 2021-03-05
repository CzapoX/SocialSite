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
            CreateMap<PostLiker, Profiles.Profile>()
                .ForMember(x => x.Username, f => f.MapFrom(f => f.AppUser.UserName))
                .ForMember(x => x.Bio, f => f.MapFrom(f => f.AppUser.Bio));
        }
    }
}
