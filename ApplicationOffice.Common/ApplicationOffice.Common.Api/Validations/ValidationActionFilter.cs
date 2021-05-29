using System.Linq;
using ApplicationOffice.Common.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApplicationOffice.Common.Api.Validations
{
    /// <summary>
    /// Action filter which validates model state and
    /// throws exception containing all validation errors if the state is invalid.
    /// </summary>
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var additionalInfo = string.Join(
                    ", ",
                    context
                        .ModelState
                        .Values
                        .SelectMany(node => node.Errors)
                        .Select(error => error.ErrorMessage));

                throw new BadRequestException(additionalInfo);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}