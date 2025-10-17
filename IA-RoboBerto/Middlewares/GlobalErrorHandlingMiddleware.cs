using IA_RoboBerto.Exceções;
using System.Data;
using System.Net;
using System.Text.Json;

namespace IA_RoboBerto.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
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
                await HandlerExceptionAsync(context, ex);
            }
        }

        private static Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            DateTime instant = DateTime.UtcNow;
            string mensagem = exception.Message;
            string path = context.Request.Path;

            var exceptionTipo = exception.GetType();

            if (exceptionTipo == typeof(DBConcurrencyException))
            {
                status = HttpStatusCode.BadRequest;    
            }
            else if (exceptionTipo == typeof(ResourceNotFoundException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else 
            {
                status = HttpStatusCode.InternalServerError;

            }
            var result = JsonSerializer.Serialize(new
            {

                status = (int)status,
                timestamp = instant,
                error = mensagem,
                path = path

            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}
