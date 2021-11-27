using Microsoft.AspNetCore.Http;

namespace DashboardMVC.Helpers
{
    public class ResourceManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ResourceManager(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public string GetImage(string imagePath)
        {
            string host = "https://" + _httpContextAccessor.HttpContext.Request.Host.ToString() + "/api/resource/images/" + imagePath;
            // var url = Request.Url.Scheme + "://" + Request.Url.Host; this.Url.ActionLink("images", "resource") + "/c3a07619-5d70-4cff-9a06-e238914d7ec2.png";
            return host;
        }

        
    }
}