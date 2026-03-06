//-----------------------------------------------------------------------
// <copyright file="CookieHandler.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TacosPortal.Services
{
    public class CookieHandler(ILogger<CookieHandler> logger) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request);
                _ = request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SendAsync CookieHandlerWASM: {ex.ToString()}");
                throw;
            }

        }
    }
}