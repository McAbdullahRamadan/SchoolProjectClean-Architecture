namespace Data.AppRouting
{
    public static class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string Role = root + "/" + version + "/";
        public static class StudentRouter
        {
            public const string Perfix = Role + "Student";
            public const string list = Perfix + "/List";
            public const string GetById = Perfix + SingleRoute;
            public const string Create = Perfix + "/Create";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string Paginated = Perfix + "/Paginated";

        }
        public static class DepartmentRouter
        {
            public const string Perfix = Role + "Department";
            public const string GetbyId = Perfix + "/Id";


        }

    }
}
