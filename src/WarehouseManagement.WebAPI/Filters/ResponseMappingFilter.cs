using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WarehouseManagement.Application.Common;

namespace WarehouseManagement.WebAPI.Filters;

public class ResponseMappingFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.Result is ObjectResult objectResult && objectResult.Value != null)
        {
            var response = new ApiResponse<object>
            {
                Success = true,
                Message = "Request Successfull",
                Data = objectResult.Value,
            };

            resultContext.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }
}