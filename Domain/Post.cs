using System;

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
    }
}