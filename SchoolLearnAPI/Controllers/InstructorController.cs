using Core.Features.Instractor.Command.Model;
using Data.AppRouting;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstractorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
