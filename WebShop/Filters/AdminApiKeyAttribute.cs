using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebShopAPI.Filters
{
    //Använder mig inte av API-key utan skapade istället roller i authentication-token
    public class AdminApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            if (!context.HttpContext.Request.Headers.TryGetValue("key", out var key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();

        }
    }
}
