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
        if (!context.Request.Cookies.TryGetValue(CookieName, out var value) ||
            !Guid.TryParse(value, out var anonUserId))
        {
            anonUserId = Guid.NewGuid();
            context.Response.Cookies.Append(
                CookieName,
                anonUserId.ToString(),
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = false
                });
        }

        context.Items["AnonUserId"] = anonUserId;

        await _next(context);
    }
}
