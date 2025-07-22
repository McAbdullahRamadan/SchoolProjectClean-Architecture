namespace Core.Features.Students.Queries.Results
{
    public class GetStudentpaginatedListResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Adreess { get; set; }
        public string? DepartmentName { get; set; }
        public GetStudentpaginatedListResponse(int studid, string? name, string? adreess, string? departmentName)
        {
            StudID = studid;
            Name = name;
            Adreess = adreess;
            DepartmentName = departmentName;


        }
    }
}
