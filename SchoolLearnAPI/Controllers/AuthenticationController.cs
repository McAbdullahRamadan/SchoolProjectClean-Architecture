using Core.Features.Authentication.Command.Models;
using Core.Features.Authentication.Queries.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> Create([FromForm] RefreshTokenCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpGet(Router.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
    }
}
