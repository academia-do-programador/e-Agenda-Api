using Microsoft.AspNetCore.Mvc.Filters;

namespace eAgenda.WebApi.Filters
{
    public class SerilogActionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
        
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

    }
}
