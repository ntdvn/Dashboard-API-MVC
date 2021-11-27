using System.Globalization;
using System.Threading.Tasks;
using DashboardMVC.DTOs;
using DashboardMVC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace DashboardMVC.Common.Middlewares
{
    public class ResponseMessageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ResponseMessageMiddleware(RequestDelegate next, IStringLocalizer<SharedResource> localizer)
        {
            this._localizer = localizer;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IOptions<JsonOptions> jsonOptions)
        {
            // invoke _next to make the next code run before the response sended
            await _next(httpContext);
            // Make sure the response has not been sent by the controller, 
            // this ensures that the message we are about to send does not 
            // suppress messages from the controller with the same status code
            if (!httpContext.Response.HasStarted)
            {

                switch (httpContext.Response.StatusCode)
                {
                    case 401:
                        await httpContext.Response.WriteAsJsonAsync(new BaseDto
                        {
                            Status = false,
                            Messages = new string[] {
                                _localizer["server_error_401"]
                            }
                        }, jsonOptions.Value.JsonSerializerOptions);
                        break;
                    case 404:
                        await httpContext.Response.WriteAsJsonAsync(new BaseDto
                        {
                            Status = false,
                            Messages = new string[] {
                                _localizer["server_error_404"]
                            }
                        }, jsonOptions.Value.JsonSerializerOptions);
                        break;
                    case 405:
                        await httpContext.Response.WriteAsJsonAsync(new BaseDto
                        {
                            Status = false,
                            Messages = new string[] {
                                _localizer["server_error_405"]
                            }
                        }, jsonOptions.Value.JsonSerializerOptions);
                        break;
                    case 500:
                        await httpContext.Response.WriteAsJsonAsync(new BaseDto
                        {
                            Status = false,
                            Messages = new string[] {
                                _localizer["server_error_405"]
                            }
                        }, jsonOptions.Value.JsonSerializerOptions);
                        break;
                }
                return;
            }
        }
    }
}