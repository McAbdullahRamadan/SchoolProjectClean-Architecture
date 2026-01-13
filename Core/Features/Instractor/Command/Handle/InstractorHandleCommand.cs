using AutoMapper;
using Core.Besec;
using Core.Features.Instractor.Command.Model;
using Core.Resource;
using Data.Entites;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Instractor.Command.Handle
{
    public class InstractorHandleCommand : ResponseHandlar,
        IRequestHandler<AddInstractorCommand, Response<string>>
    {
        #region Feilds
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IInstractorService _instractorService;
        private readonly IMapper _mapper;


        #endregion
        #region Constructors
        public InstractorHandleCommand(IStringLocalizer<SheardResource> localizer, IMapper mapper, IInstractorService instractorService) : base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _instractorService = instractorService;

        }

        public async Task<Response<string>> Handle(AddInstractorCommand request, CancellationToken cancellationToken)
        {
            var MappInstructor = _mapper.Map<Instructor>(request);
            var result = await _instractorService.AddInstructorAsync(MappInstructor, request.Image);
            switch (result)
            {
                case "NoImage":
                    return
                    BadRequst<string>(_localizer[KeySharedResource.NoImage]);
                case "FailedToUploadImage": return BadRequst<string>(_localizer[KeySharedResource.FailedToUpload]);
                case "FailedInAdd": return BadRequst<string>(_localizer[KeySharedResource.AddFailed]);
            }
            return Success("");
        }
        #endregion
        #region Handle Function

        #endregion

    }
}
