using BlogAPI.Common;
using BlogAPI.Models;
using BlogAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Middlewares
{
	public class AuthMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public AuthMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["token"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    TokenModel model = _authService.TokenDecrypt(token);

                    if (model != null && await _authService.TokenKeyCheck(model))
                    {
                        AddHeader(httpContext, "UID", model.UID);
                    }
                }
                catch (Exception ex)
                {

                }

            }

            AddHeader(httpContext, "IP", CommonTool.Userip_Get(httpContext));
            await _next(httpContext);
        }

        private void AddHeader(HttpContext httpContext, string key, string value)
        {
            if (httpContext.Request.Headers.ContainsKey(key))
            {
                httpContext.Request.Headers.Remove(key);
            }
            httpContext.Request.Headers.Add(key, value);
        }

    }
}
