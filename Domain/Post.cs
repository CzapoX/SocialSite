using System;
using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PostOwnerId { get; set; }
        public AppUser PostOwner { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<PostLiker> PostLikers { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}