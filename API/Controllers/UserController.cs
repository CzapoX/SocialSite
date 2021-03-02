using Application.User;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto loginCredentials)
        {
            return HandleResult(await Mediator.Send(new Login.Query { loginCredentials = loginCredentials }));
        }

        /// <summary>
        /// Adds new user to db and logs him in
        /// </summary>
        /// <param name="registerCredentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDto registerCredentials)
        {
            return HandleResult(await Mediator.Send(new Register.Command { RegisterCredentials = registerCredentials }));
        }

        /// <summary>
        /// Returns loged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            return HandleResult(await Mediator.Send(new GetCurrentUser.Query()));
        }
    }
}
