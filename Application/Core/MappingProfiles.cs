using Application.Posts;
using Domain;
using System.Linq;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<PostCreateOrEditDto, Post>();
            CreateMap<Post, PostDto>();
            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(x => x.Image, f => f.MapFrom(f => f.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<AppUser, PostLikerDto>()
                .ForMember(x => x.Image, f => f.MapFrom(f => f.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<PostLiker, PostLikerDto>()
                .ForMember(x => x.Username, f => f.MapFrom(f => f.AppUser.UserName))
                .ForMember(x => x.Bio, f => f.MapFrom(f => f.AppUser.Bio))
                .ForMember(x => x.Image, f => f.MapFrom(f => f.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
