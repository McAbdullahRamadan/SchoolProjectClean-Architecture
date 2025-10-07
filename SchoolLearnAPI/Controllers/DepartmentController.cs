using Core.Features.Department.Queries.Model;
using Data.AppRouting;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouter.GetbyId)]
        public async Task<IActionResult> GeDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {

            return NewResult(await Mediator.Send(query));
        }
    }
}
