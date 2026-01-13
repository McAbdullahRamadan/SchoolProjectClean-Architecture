using AutoMapper;
using Core.Besec;
using Core.Features.Students.Commands.Models;
using Core.Resource;
using Data.Entites;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandlar,
                                        IRequestHandler<AddStudentComand, Response<string>>,
                                        IRequestHandler<EditStudentCommand, Response<string>>,
                                        IRequestHandler<DeleteStudentComand, Response<string>>



    {

        #region Fildes 
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;

        #endregion

        #region Constructor
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SheardResource> Localizer) : base(Localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _Localizer = Localizer;


        }


        #endregion
        #region Handel Function
        public async Task<Response<string>> Handle(AddStudentComand request, CancellationToken cancellationToken)
        {
            //Mapping
            var Stude = _mapper.Map<Student>(request);
            //Add
            var result = await _studentService.AddAsync(Stude);


            //Return Response
            if (result == "Success") return Created(_Localizer[KeySharedResource.Create] + "");
            else return BadRequst<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //chack if the id is Exist or Not
            var student = await _studentService.GetStudentByid(request.Id);
            //return NotFound
            if (student == null)
                return NotFound<string>();
            //mapping between request and student
            var StudeMapp = _mapper.Map(request, student);
            //call service that make Edit
            var result = await _studentService.EditAsync(StudeMapp);
            //return Response
            if (result == "Success") return Success((string)_Localizer[KeySharedResource.Success] + StudeMapp.StudID);
            else return BadRequst<string>();

        }

        public async Task<Response<string>> Handle(DeleteStudentComand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByid(request.Id);
            //return NotFound
            if (student == null)
                return NotFound<string>(_Localizer[KeySharedResource.NoFound]);
            //call service that make Edit
            var result = await _studentService.DeleteAsync(student);
            //return Response
            if (result == "Success") return Deleted<string>(_Localizer[KeySharedResource.Deleted] + $" {request.Id}");
            else return BadRequst<string>();
        }
        #endregion

    }
}
