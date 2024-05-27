using System.Net;
using LearnDotnet.Models; 

namespace LearnDotnet.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while processing your request: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    errorResponse.Title = exception.GetType().Name;
                    break;

                default:
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    errorResponse.Title = "Internal Server Error";
                    break;
            }

            httpContext.Response.StatusCode = Int32.Parse(errorResponse.StatusCode);
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}

// IExceptionHandler
/*
public class GlobalExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            $"An error occurred while processing your request: {exception.Message}");

        var errorResponse = new ErrorResponse
        {
            Message = exception.Message
        };

        switch (exception)
        {
            case BadHttpRequestException:
                errorResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                errorResponse.Title = exception.GetType().Name;
                break;

            default:
                errorResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                errorResponse.Title = "Internal Server Error";
                break;
        }

        httpContext.Response.StatusCode = Int32.Parse(errorResponse.StatusCode);

        await httpContext
            .Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }


}

*/