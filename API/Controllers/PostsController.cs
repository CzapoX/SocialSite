using Microsoft.AspNetCore.Mvc;
using Application.Posts;
using System.Threading.Tasks;
using System;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        /// <summary>
        /// Fetches list of posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        /// <summary>
        /// Fetches a single post by id
        /// </summary>
        /// <param name="id" example="330EE2CD-F1DD-40B8-807D-08D8D725D360">Post ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        /// <summary>
        /// Adds new post
        /// </summary>
        /// <param name="newPost">Added Post</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateOrEditDto newPost)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Post = newPost }));
        }

        /// <summary>
        /// Updates an existing post
        /// </summary>
        /// <param name="id" example="330EE2CD-F1DD-40B8-807D-08D8D725D360">Post ID</param>
        /// <param name="editPost">Edited post</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> EditPost(Guid id, PostCreateOrEditDto editPost)
        {
            editPost.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Post = editPost }));
        }

        /// <summary>
        /// Deletes a post with selected id
        /// </summary>
        /// <param name="id" example="EC10C5B2-3CE3-4A9D-3248-08D8DE349543">Post ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}