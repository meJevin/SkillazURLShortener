using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.API.Middleware
{
    public class UserWithoutRegistrationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserWithoutRegistrationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Session.GetString("SessionUserName")))
            {
                context.Session.SetString(Constants.SessionUserNameKey, Guid.NewGuid().ToString());
            }

            Debug.WriteLine("CURRENt SESSION: " + context.Session.GetString(Constants.SessionUserNameKey));

            await _next(context);
        }
    }
}
