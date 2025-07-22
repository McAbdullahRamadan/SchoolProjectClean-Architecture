using Core.Wrappers;

namespace Core.Features.Department.Queries.Result
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ManagerName { get; set; }
        public PaginatedResult<StudentResponse>? studentList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }



        public class StudentResponse
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public StudentResponse(int id, string name)
            {
                Id = id;
                Name = name;


            }
        }
        public class SubjectResponse
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
        public class InstructorResponse
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }


    }
}
