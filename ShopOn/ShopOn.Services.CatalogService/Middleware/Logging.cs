using System.Text;

namespace ShopOn.Services.CatalogService.Middleware;

public class Logging
{
    private readonly RequestDelegate _next;
    private readonly ILogger<Logging> _logger;

    public Logging(RequestDelegate next, ILogger<Logging> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the incoming request
        context.Request.EnableBuffering();
        var request = await ReadRequestBody(context.Request);
        _logger.LogInformation($"Incoming request: {request}");

        // Copy a pointer to the original response body stream
        var originalResponseBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            // Point the response body to the newly created stream
            context.Response.Body = responseBody;

            await _next(context); // Continue processing

            // Read the response which has now been generated
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Outgoing response: {response}");

            // Copy the contents of the new stream to the original stream
            await responseBody.CopyToAsync(originalResponseBodyStream);
            context.Response.Body = originalResponseBodyStream;
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }
}
