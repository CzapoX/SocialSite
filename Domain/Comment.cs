using System;

namespace Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
