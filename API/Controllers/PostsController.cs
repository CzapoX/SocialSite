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
    }
}