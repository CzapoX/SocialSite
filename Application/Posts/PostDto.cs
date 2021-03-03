using Application.Profiles;
using System;

namespace Application.Posts
{
    public class PostDto
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public Profile PostOwner { get; set; }
    }
}
