using MaxiShop.Application.Exceptions;
using MaxiShop.Web.Models;
using System.Net;

namespace MaxiShop.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch(ex)
            {
                case BadRequestExeption badRequestExeption:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails()
                    {
                        Title = badRequestExeption.Message,
                        Status = (int)statusCode,
                        Type = nameof(badRequestExeption),
                        Detail = badRequestExeption.InnerException?.Message,
                        Errors = badRequestExeption.ValidationsErrors
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
