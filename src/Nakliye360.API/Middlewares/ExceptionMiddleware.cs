namespace Nakliye360.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // normal akış
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new
                {
                    status = 500,
                    title = "Sunucuda beklenmeyen bir hata oluştu.", // 
                    detail = _env.IsDevelopment() ? ex.ToString() : null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
