namespace Askly.Api.Handlers;

public class ForwardCookiesHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ForwardCookiesHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
            return base.SendAsync(request, cancellationToken);

        var cookieHeader = context.Request.Headers["Cookie"].ToString();
        if (!string.IsNullOrEmpty(cookieHeader))
        {
            request.Headers.Add("Cookie", cookieHeader);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
