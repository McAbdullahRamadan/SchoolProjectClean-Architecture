namespace Data.AppRouting
{
    public static class Router
    {
        public const string SingleRoute = "/{id}";
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public static class StudentRouter
        {
            public const string Perfix = Rule + "Student";
            public const string list = Perfix + "/List";
            public const string GetById = Perfix + SingleRoute;
            public const string Create = Perfix + "/Create";
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string Paginated = Perfix + "/Paginated";

        }
        public static class DepartmentRouter
        {
            public const string Perfix = Rule + "Department";
            public const string GetbyId = Perfix + "/Id";


        }
        public static class UserRouter
        {
            public const string Perfix = Rule + "User";
            public const string Create = Perfix + "/Create";
            public const string Paginated = Perfix + "/Paginated";
            public const string GetById = Perfix + SingleRoute;
            public const string Edit = Perfix + "/Edit";
            public const string Delete = Perfix + "/{id}";
            public const string ChangePassword = Perfix + "/ChangePassword";

        }
        public static class Authentication
        {
            public const string Perfix = Rule + "Authentication";
            public const string SignIn = Perfix + "/SignIn";
            public const string RefreshToken = Perfix + "/RefreshToken";
            public const string ValidateToken = Perfix + "/ValidateToken";




        }
        public static class AuthorizeRoute
        {
            /// <summary>
            //Role Url
            /// </summary>
            public const string Perfix = Rule + "AuthorizeRole";
            public const string Roles = Perfix + "/Role";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{id}";
            public const string ListRole = Roles + "/Role-list";
            public const string RoleById = Roles + "/Role-By/{id}";
            public const string ManageUserRole = Roles + "/Manage-User-Role/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Role";

            //Claims Url
            public const string Claims = Perfix + "/Claims";
            public const string ManageUserClaims = Claims + "/Update-User-Claims/{userId}";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";



















        }

    }
}
