using Application.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        /// <summary>
        /// Adds photo for logged in user, if user already have photo method returns BadRequest
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddToUser([FromForm] AddForUser.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Adds photo to post
        /// </summary>
        /// <param name="id" example="330EE2CD-F1DD-40B8-807D-08D8D725D360"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize(Policy = "IsPostOwner")]
        public async Task<IActionResult> AddToPost(Guid id, [FromForm] AddForPost.Command command)
        {
            command.PostId = id;
            return HandleResult(await Mediator.Send(command));
        }
    }
}
