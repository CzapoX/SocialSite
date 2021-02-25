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
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateOrEditDto post)
        {
            return Ok(await Mediator.Send(new Create.Command { Post = post }));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> EditPost(Guid id, PostCreateOrEditDto post)
        {
            post.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Post = post }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}