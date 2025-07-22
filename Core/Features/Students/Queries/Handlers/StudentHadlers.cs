using AutoMapper;
using Core.Besec;
using Core.Features.Students.Queries.Models;
using Core.Features.Students.Queries.Results;
using Core.Resource;
using Core.Wrappers;
using Data.Entites;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;
using System.Linq.Expressions;

namespace Core.Features.Students.Queries.Handlers
{
    public class StudentHadlers : ResponseHadlar,
        IRequestHandler<GetListStudentQueries, Response<List<GetSudentListResult>>>,
        IRequestHandler<GetStudentByIdQueries, Response<GetSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentpaginatedListResponse>>



    {

        #region Fildes
        public readonly IStudentService _studentService;
        public readonly IMapper _mapper;
        public readonly IStringLocalizer<SheardResource> _Localizer;



        #endregion
        #region Constructor
        public StudentHadlers(IStudentService studentService, IMapper mapper, IStringLocalizer<SheardResource> Localizer) : base(Localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _Localizer = Localizer;

        }
        #endregion
        #region Handels Function
        public async Task<Response<List<GetSudentListResult>>> Handle(GetListStudentQueries request, CancellationToken cancellationToken)
        {
            var studentresult = await _studentService.GetStudentsAsync();
            var studentresultMapper = _mapper.Map<List<GetSudentListResult>>(studentresult);
            var result = Success(studentresultMapper);
            result.Meta = new { Count = studentresultMapper.Count() };
            return result;


        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQueries request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsByidAsyncEncloud(request.Id);
            if (student == null)
                return NotFound<GetSingleStudentResponse>(_Localizer[KeySharedResource.NoFound]);
            var Result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(Result);


        }

        public async Task<PaginatedResult<GetStudentpaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentpaginatedListResponse>> expression = e => new GetStudentpaginatedListResponse(e.StudID, e.Localized(e.NameAr, e.NameEn), e.Adreess, e.Localized(e.Departments.DNameAr, e.Departments.DNameEn));
            //var querable = _studentService.GetStudentQuerable();
            var fliterQuery = _studentService.FilterStudentPagintedQuerable(request.OrderBy, request.Search);
            var paginatedList = await fliterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;

        }
        #endregion

    }
}
