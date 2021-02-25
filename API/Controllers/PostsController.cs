using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Posts;
using System.Collections.Generic;
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
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return await Mediator.Send(new List.Query());
        }

        /// <summary>
        /// Fetches a single post by id
        /// </summary>
        /// <param name="id" example="a238c320-0412-4ba4-b417-256e202ae623">Post ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        /// <summary>
        /// Adds new post
        /// </summary>
        /// <param name="newPost">Added Post</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateOrEditDto newPost)
        {
            return Ok(await Mediator.Send(new Create.Command { Post = newPost }));
        }

        /// <summary>
        /// Updates an existing post
        /// </summary>
        /// <param name="id" example="a238c320-0412-4ba4-b417-256e202ae623">Post ID</param>
        /// <param name="editPost">Edited post</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> EditPost(Guid id, PostCreateOrEditDto editPost)
        {
            editPost.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Post = editPost }));
        }

        /// <summary>
        /// Deletes a post with selected id
        /// </summary>
        /// <param name="id" example="a238c320-0412-4ba4-b417-256e202ae623">Post ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}