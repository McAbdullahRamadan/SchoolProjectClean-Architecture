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
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.AuthorizeRoute.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.AuthorizeRoute.Edit)]
        public async Task<IActionResult> Create([FromForm] EditRoleCommand Command)
        {
            var response = await Mediator.Send(Command);
            return NewResult(response);
        }
        [HttpDelete(Router.AuthorizeRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
    }
}
