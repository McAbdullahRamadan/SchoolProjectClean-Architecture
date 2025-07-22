namespace Core.Features.Students.Queries.Results
{
    public class GetSingleStudentResponse
    {
        public int StudID { get; set; }
        public string? Name { get; set; }
        public string? Adreess { get; set; }
        public string? DepartmentName { get; set; }
    }
}
