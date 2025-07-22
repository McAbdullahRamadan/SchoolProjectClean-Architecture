using AutoMapper;
using Core.Besec;
using Core.Features.Department.Queries.Model;
using Core.Features.Department.Queries.Result;
using Core.Resource;
using Core.Wrappers;
using Data.Entites;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;
using System.Linq.Expressions;
using static Core.Features.Department.Queries.Result.GetDepartmentByIdResponse;

namespace Core.Features.Department.Queries.Handles
{

    public class HandleDepartmentQuery : ResponseHadlar,
      IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SheardResource> _stringLocalizer;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public HandleDepartmentQuery(IStringLocalizer<SheardResource> stringLocalizer,
            IDepartmentService departmentService, IMapper mapper, IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;


        }


        #endregion
        #region Handel 
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //Service By id include st sub ins
            var response = await _departmentService.GetDepartmentById(request.Id);
            //chack Is nte Exist
            if (response == null) return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[KeySharedResource.NoFound]);
            //Mapping
            var mapper = _mapper.Map<GetDepartmentByIdResponse>(response);

            //Paginted 
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localized(e.NameAr, e.NameEn));
            var StudentQuerable = _studentService.GetStudentByDepartmentbyIDQuerable(request.Id);
            var paginatedList = await StudentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            mapper.studentList = paginatedList;
            //Return Response
            return Success(mapper);
        }
        #endregion

    }


}
