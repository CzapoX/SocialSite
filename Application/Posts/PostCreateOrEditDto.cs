using System;

namespace Application.Posts
{
    public class PostCreateOrEditDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Post description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Post category
        /// </summary>
        public string Category { get; set; }
    }
}
