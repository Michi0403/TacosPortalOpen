//-----------------------------------------------------------------------
// <copyright file="CookieBlazorServerHandler.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TacosPortal.Services
{
    public class CookieBlazorServerHandler(IHttpContextAccessor httpContextAccessor, ILogger<CookieBlazorServerHandler> logger) : DelegatingHandler
    {

        private void AttachAuthCookie(HttpRequestMessage request)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(httpContextAccessor);
                var cookie = httpContextAccessor.HttpContext?.Request?.Cookies[".AspNetCore.Cookies"];
                if (!string.IsNullOrWhiteSpace(cookie))
                {
                    request.Headers.Add("Cookie", $".AspNetCore.Cookies={cookie}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in AttachAuthCookie {ex.ToString()}");
                throw;
            }

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);
                AttachAuthCookie(request);
                _ = request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in CookieBlazorServerHandler {ex.ToString()}");
                throw;
            }

        }
    }
}
