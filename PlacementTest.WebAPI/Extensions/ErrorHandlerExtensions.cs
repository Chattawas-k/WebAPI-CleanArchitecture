using Microsoft.AspNetCore.Diagnostics;
using PlacementTest.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace PlacementTest.WebAPI.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        BadRequestException => (int)HttpStatusCode.BadRequest,
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                        NoDataFoundException => (int)HttpStatusCode.NotFound,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var exception = contextFeature.Error;
                    var badRequestErrors = (exception as BadRequestException)?.Errors;

                    object errorResponse;

                    if (badRequestErrors != null && badRequestErrors.Length > 0)
                    {
                        errorResponse = new
                        {
                            statusCode = context.Response.StatusCode,
                            message = exception.GetBaseException().Message,
                            errors = badRequestErrors
                        };
                    }
                    else
                    {
                        errorResponse = new
                        {
                            statusCode = context.Response.StatusCode,
                            message = exception.GetBaseException().Message
                        };
                    }

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}
