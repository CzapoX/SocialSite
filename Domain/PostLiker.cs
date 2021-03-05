using System;

namespace Domain
{
    public class PostLiker
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
