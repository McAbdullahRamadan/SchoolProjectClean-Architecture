using Core.Features.Students.Commands.Models;
using Core.Features.Students.Queries.Handlers;
using Core.Features.Students.Queries.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class StudentController : AppControllerBase
    {


        [HttpGet(Router.StudentRouter.list)]
        public async Task<IActionResult> GetAllStudent()
        {
            var response = await Mediator.Send(new GetListStudentQueries());
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet(Router.StudentRouter.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.StudentRouter.GetById)]
        public async Task<IActionResult> GeStudentById([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new GetStudentByIdQueries(id)));
        }
        [HttpPost(Router.StudentRouter.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentComand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpPut(Router.StudentRouter.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
        [HttpDelete(Router.StudentRouter.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            return NewResult(await Mediator.Send(new DeleteStudentComand(id)));
        }
    }
}
