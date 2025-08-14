using Core.Features.Authorization.Command.Models;
using Core.Features.Authorization.Queries.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;
using Swashbuckle.AspNetCore.Annotations;

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
        [Authorize(Roles = "Admin")]

        [HttpGet(Router.AuthorizeRoute.ListRole)]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(summary: "هذه الصلاحية عن طريق ال Id", OperationId = "RoleById")]
        [HttpGet(Router.AuthorizeRoute.RoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }
    }
}
