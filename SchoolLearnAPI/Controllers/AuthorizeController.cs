using Core.Features.Authorization.Command.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class AuthorizeController : AppControllerBase
    {
        [Authorize]
        [HttpPost(Router.AuthorizeRoute.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
    }
}
