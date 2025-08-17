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

        [HttpGet(Router.AuthorizeRoute.RoleById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.AuthorizeRoute.ManageUserRole)]
        public async Task<IActionResult> ManageUserRole([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserRoleQuery() { UserId = userId });
            return NewResult(response);
        }
        [SwaggerOperation(summary: "تعديل صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpPut(Router.AuthorizeRoute.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateRoleUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.AuthorizeRoute.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }
    }
}
