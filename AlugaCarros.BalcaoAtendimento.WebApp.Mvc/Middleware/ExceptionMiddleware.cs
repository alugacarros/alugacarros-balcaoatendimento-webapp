using AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Configuration.Exceptions;

namespace AlugaCarros.BalcaoAtendimento.WebApp.Mvc.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (Exception)
            {
                HandleRequestExceptionAsync(httpContext, HttpStatusCode.InternalServerError);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.InternalServerError:
                    context.Response.Redirect($"/erro/{(int)statusCode}");
                    return;
            }

            context.Response.StatusCode = (int)statusCode;
        }
    }
}
