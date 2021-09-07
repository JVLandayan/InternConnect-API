using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternConnect.Service.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace InternConnect.Service.ThirdParty
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAccountService accountService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = accountService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
