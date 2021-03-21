using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUppgift.Auth
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method  )]
    public class AccessingAuthAttribute : Attribute, IAsyncActionFilter
    {
       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Query.TryGetValue("AccessKey", out var provideAccessKey ))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var _confiq = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var _apiAccessKey = _confiq.GetValue<string>(key: "SecretKey");
            if (!_apiAccessKey.Equals(provideAccessKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
