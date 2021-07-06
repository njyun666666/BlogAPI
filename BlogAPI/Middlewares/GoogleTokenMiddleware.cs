using BlogAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Middlewares
{
	public class GoogleTokenMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly IGoogleLoginService _googleLoginService;

        public GoogleTokenMiddleware(RequestDelegate next, IGoogleLoginService googleLoginService)
        {
            _next = next;
            _googleLoginService = googleLoginService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string gtoken = httpContext.Request.Headers["gtoken"].FirstOrDefault();

            if (!string.IsNullOrEmpty(gtoken))
            {
                try
                {
                    await _googleLoginService.ValidateGoogleToken(gtoken);
                }
                catch (Exception ex)
                {

                }

            }

            await _next(httpContext);
        }
    }
}
