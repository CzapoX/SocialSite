using Application.Photos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        /// <summary>
        /// Adds photo for logged in user profile
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
        /// <param name="postId" example="330EE2CD-F1DD-40B8-807D-08D8D725D360"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{postId}")]
        [Authorize(Policy = "IsPostOwner")]
        public async Task<IActionResult> AddToPost(Guid postId, [FromForm] AddForPost.Command command)
        {
            command.PostId = postId;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete existing photo if it isn't main photo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        /// <summary>
        /// Sets user's photo as main
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [HttpPost("{photoId}/setMain")]
        public async Task<IActionResult> SetMain(Guid photoId)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { Id = photoId }));
        }

    }
}
