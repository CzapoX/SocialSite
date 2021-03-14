using Application.Profiles;
using System;

namespace Application.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Profile Author { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
