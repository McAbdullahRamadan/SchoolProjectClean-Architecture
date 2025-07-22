using Core.Features.Department.Queries.Result;
using Data.Entites;
using static Core.Features.Department.Queries.Result.GetDepartmentByIdResponse;

namespace Core.Features.Mapping.DepartmentMapp.QueryMapping
{
    public partial class DepartmentProfil
    {
        public void GetDepartmentByIDMapping()
        {
            CreateMap<Data.Entites.Department, GetDepartmentByIdResponse>()
                .ForMember(x => x.Name, op => op.MapFrom(src => src.Localized(src.DNameAr, src.DNameEn)))
                .ForMember(x => x.Id, op => op.MapFrom(src => src.DID))
                .ForMember(x => x.ManagerName, op => op.MapFrom(src => src.Instructor.Localized(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                .ForMember(x => x.SubjectList, op => op.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(x => x.studentList, op => op.MapFrom(src => src.Students))
                .ForMember(x => x.InstructorList, op => op.MapFrom(src => src.Instructors));


            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(x => x.Id, op => op.MapFrom(src => src.SubID))
                .ForMember(x => x.Name, op => op.MapFrom(src => src.Subjects.Localized(src.Subjects.SubjectNameAr, src.Subjects.SubjectNameEn)));

            //CreateMap<Student, StudentResponse>()
            //     .ForMember(x => x.Id, op => op.MapFrom(src => src.StudID))
            //    .ForMember(x => x.Name, op => op.MapFrom(src => src.Localized(src.NameAr, src.NameEn)));


            CreateMap<Instructor, InstructorResponse>()
                  .ForMember(x => x.Id, op => op.MapFrom(src => src.InsId))
                .ForMember(x => x.Name, op => op.MapFrom(src => src.Localized(src.ENameAr, src.ENameEn)));




        }
    }
}
