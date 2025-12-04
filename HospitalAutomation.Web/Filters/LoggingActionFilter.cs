using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HospitalAutomation.Web.Filters
{
    public class LoggingActionFilter : IAsyncActionFilter, IAsyncExceptionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackAction = !HttpMethods.IsGet(method);

            if (!trackAction)
            {
                await next();
                return;
            }

            var controller = context.ActionDescriptor.RouteValues["controller"] ?? "UnknownController";
            var action = context.ActionDescriptor.RouteValues["action"] ?? "UnknownAction";
            var userName = context.HttpContext.User?.Identity?.IsAuthenticated == true
                ? context.HttpContext.User.Identity!.Name ?? "(anonymous)"
                : "(anonymous)";

            ActionExecutedContext executedContext;
            try
            {
                executedContext = await next();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Action {Controller}/{Action} ({Method}) failed for user {User}", controller, action, method, userName);
                throw;
            }

            var statusCode = executedContext.HttpContext.Response?.StatusCode;
            var outcome = executedContext.Exception == null ? "Completed" : "Failed";

            _logger.LogInformation("Action {Controller}/{Action} ({Method}) executed by {User} -> {Outcome} (StatusCode: {StatusCode})", controller, action, method, userName, outcome, statusCode);
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context == null)
            {
                return Task.CompletedTask;
            }

            var controller = context.ActionDescriptor?.RouteValues?["controller"] ?? "UnknownController";
            var action = context.ActionDescriptor?.RouteValues?["action"] ?? "UnknownAction";
            var userName = context.HttpContext?.User?.Identity?.IsAuthenticated == true
                ? context.HttpContext.User.Identity!.Name ?? "(anonymous)"
                : "(anonymous)";

            _logger.LogError(context.Exception, "Unhandled exception in action {Controller}/{Action} for user {User}", controller, action, userName);
            return Task.CompletedTask;
        }
    }
}
