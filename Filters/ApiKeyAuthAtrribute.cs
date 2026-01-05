using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace FootTrack.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check if header exists
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            var receivedKey = potentialApiKey.ToString().Trim();

Console.WriteLine($"Received: '{receivedKey}', Expected: '{apiKey?.Trim()}'");

if (string.IsNullOrEmpty(apiKey) || apiKey.Trim() != receivedKey)
{
    context.Result = new UnauthorizedResult();
    return;
}


        
            await next();
        }
    }
}
