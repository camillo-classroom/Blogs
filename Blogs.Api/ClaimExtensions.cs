using System.Security.Claims;

namespace Blogs.Api
{
    public static class ClaimExtensions
    {
            public static long GetId(this ClaimsPrincipal user)
            {
                try
                {
                    return Convert.ToInt64(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "0");
                }
                catch
                {
                    return 0;
                }
            }
        }
}
