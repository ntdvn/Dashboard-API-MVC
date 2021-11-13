using System;
using System.Security.Claims;

namespace DashboardMVC.Extensions
{
    public static class ClaimsPrincipleExtension
    {
         public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static String GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}