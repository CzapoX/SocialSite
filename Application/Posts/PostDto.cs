using Application.Profiles;
using System;
using System.Collections.Generic;

namespace Application.Posts
{
    public class PostDto
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public PostLikerDto PostOwner { get; set; }
        public List<PostLikerDto> PostLikers { get; set; }
    }
}
