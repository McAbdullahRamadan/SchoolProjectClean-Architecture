using Core.Features.EmailsSend.Command.Models;
using Data.AppRouting;
using Microsoft.AspNetCore.Mvc;
using SchoolLearnAPI.Base;

namespace SchoolLearnAPI.Controllers
{

    [ApiController]
    public class SendEmail : AppControllerBase
    {
        [HttpPost(Router.Email.SendEmail)]
        public async Task<IActionResult> Create([FromQuery] SendEmailCommand comand)
        {
            var response = await Mediator.Send(comand);
            return NewResult(response);
        }
    }
}
