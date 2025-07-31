using AutoMapper;
using Core.Besec;
using Core.Features.UserRegistration.Query.Models;
using Core.Features.UserRegistration.Query.Results;
using Core.Resource;
using Core.Wrappers;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Query.Handle
{
    public class UserQueryHandler : ResponseHadlar,
        IRequestHandler<GetUserListPaginationQuery, PaginatedResult<GetUserListResponse>>
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

        #endregion
    }
}
