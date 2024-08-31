using System.Text;

public class JsonSanitizationMiddleware
{
    private readonly RequestDelegate _next;

    public JsonSanitizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json") && context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();

                // Replace unescaped newlines with properly escaped newlines
                body = body.Replace("\n", "\\n").Replace("\r", "\\r");

                // Reset the request body with the sanitized content
                var sanitizedBody = new MemoryStream(Encoding.UTF8.GetBytes(body));
                context.Request.Body = sanitizedBody;
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
        }

        await _next(context);
    }
}
