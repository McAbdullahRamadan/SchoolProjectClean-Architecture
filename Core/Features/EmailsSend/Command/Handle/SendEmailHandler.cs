using Core.Besec;
using Core.Features.EmailsSend.Command.Models;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.EmailsSend.Command.Handle
{
    public class SendEmailHandler : ResponseHadlar,
                                        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IEmailsService _emailsService;
        #endregion
        #region Constructors
        public SendEmailHandler(IStringLocalizer<SheardResource> localizer, IEmailsService emailsService) : base(localizer)
        {
            _emailsService = emailsService;

            _localizer = localizer;
        }



        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmails(request.Email, request.Message, null);
            if (response == "Success")
                return Success<string>("");
            return BadRequst<string>(_localizer[KeySharedResource.SendEmailFailed]);
        }
        #endregion
    }
}
