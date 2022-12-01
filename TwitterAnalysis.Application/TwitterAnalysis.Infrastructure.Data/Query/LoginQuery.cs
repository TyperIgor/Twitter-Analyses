namespace TwitterAnalysis.Infrastructure.Data.Query
{
    public static class LoginQuery
    {
        public static string UserQuery => "select count(1) from \"ClientAuthorize\" where \"Name\"=@Name and \"Secret\"=@Secret";
    }
}
