using Application.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Logs in existing user
        /// </summary>
        /// <param name="loginCredentials">User login credentials</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto loginCredentials)
        {
            return HandleResult(await Mediator.Send(new Login.Query { UserLogin = loginCredentials }));
        }
    }
}
