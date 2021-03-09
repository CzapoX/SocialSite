using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Bio { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Post> PostsCreated { get; set; }
        public ICollection<PostLiker> PostsLiked { get; set; }
    }
}
