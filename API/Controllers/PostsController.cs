using Domain;
using Microsoft.AspNetCore.Mvc;
using Application.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PostsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}