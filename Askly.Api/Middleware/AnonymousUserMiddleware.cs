namespace Askly.Api.Middleware;

public class AnonymousUserMiddleware
{
    private const string CookieName = "anon_user_id";
    private readonly RequestDelegate _next;

    public AnonymousUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Cookies.TryGetValue(CookieName, out var anonUserId))
        {
            anonUserId = Guid.NewGuid().ToString();

            context.Response.Cookies.Append(
                CookieName,
                anonUserId,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = context.Request.IsHttps
                });
        }

        // кладём в HttpContext для дальнейшего использования
        context.Items["AnonUserId"] = Guid.Parse(anonUserId);

        await _next(context);
    }
}
