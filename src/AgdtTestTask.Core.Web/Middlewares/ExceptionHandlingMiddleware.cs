using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AgdtTestTask.Core.Web.Middlewares
{
    internal class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(
            HttpContext context,
            RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(
                    context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            string message,
            HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = statusCode.ToString(),
                Detail = message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem));
        }
    }
}
