namespace Data.Helpers.Results
{
    public class ManageUserClaimsResult
    {
        public int UserId { get; set; }
        public List<UserClaims> Userclaims { get; set; } = new List<UserClaims>();
        public class UserClaims
        {
            public string Type { get; set; }
            public bool Value { get; set; }

        }
    }
}
