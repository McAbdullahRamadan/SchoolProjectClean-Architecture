using AutoMapper;
using Core.Besec;
using Core.Features.UserRegistration.Query.Models;
using Core.Features.UserRegistration.Query.Results;
using Core.Resource;
using Core.Wrappers;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Query.Handle
{
    public class UserQueryHandler : ResponseHandlar,
        IRequestHandler<GetUserListPaginationQuery, PaginatedResult<GetUserListResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>



    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly UserManager<UserIdentity> _userManager;
        #endregion
        #region Constructors
        public UserQueryHandler(IStringLocalizer<SheardResource> stringLocalizer, IMapper mapper
            , UserManager<UserIdentity> userManage) : base(stringLocalizer)
        {
            _Localizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManage;

        }




        #endregion
        #region Handle Function

        public async Task<PaginatedResult<GetUserListResponse>> Handle(GetUserListPaginationQuery request, CancellationToken cancellationToken)
        {
            var Users = _userManager.Users.AsQueryable();
            var PaginatedList = await _mapper.ProjectTo<GetUserListResponse>(Users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            //var User1 = _userManager.FindByIdAsync(request.Id.ToString());
            if (User == null)
                return NotFound<GetUserByIdResponse>(_Localizer[KeySharedResource.NoFound]);
            var Result = _mapper.Map<GetUserByIdResponse>(User);
            return Success(Result);
        }

        #endregion
    }
}
