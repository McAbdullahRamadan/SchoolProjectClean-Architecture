using Core.Features.UserRegistration.Command.Models;
using Core.Features.UserRegistration.Query.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(Router.UserRouter.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpGet(Router.UserRouter.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserListPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.UserRouter.GetById)]
        public async Task<IActionResult> GeStudentById([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPut(Router.UserRouter.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpDelete(Router.UserRouter.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new DeleteUserCommand(id)));
        }
    }
}
