using Computers.Attributes;
using Computers.Models.Dto;
using Computers.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;

namespace Computers.Middlewares
{
    public class AuthorizationHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthorizationRepository _authUsers;

        public AuthorizationHandlingMiddleware(RequestDelegate next, IAuthorizationRepository authorizationUsers)
        {
            _next = next;
            _authUsers = authorizationUsers;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint.Metadata.Count == 0)
            {
                await AbortRequestAsync(context, HttpStatusCode.NotFound);
                return;
            }
            var attribute = endpoint.Metadata.GetMetadata<CustomAuthorizeAttribute>();
            if (attribute != null)
            {
                if (context.Request.Headers.TryGetValue("accessToken", out var token))
                {
                    if (Guid.TryParse(token, out var guidToken))
                    {
                        if (_authUsers.GetAuthorizationUser(guidToken, out var user))
                        {
                            if (_authUsers.IsValidRole(attribute.Role, user.Role))
                            {
                                await _next(context);
                                return;
                            }
                            else
                            {
                                await AbortRequestAsync(context, HttpStatusCode.Forbidden);
                                return;
                            }
                        }
                    }
                }
                await AbortRequestAsync(context, HttpStatusCode.Unauthorized);
            }
            else
            {
                await _next(context);
            }            
        }

        private async Task AbortRequestAsync(HttpContext context, HttpStatusCode statusCode)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            await response.WriteAsJsonAsync(statusCode.ToString());
        }
    }
}
