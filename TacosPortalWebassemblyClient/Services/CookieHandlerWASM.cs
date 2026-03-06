//-----------------------------------------------------------------------
// <copyright file="CookieHandlerWASM.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace TacosPortalWebassemblyClient.Services
{
    public class CookieHandlerWASM(ILogger<CookieHandlerWASM> logger) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nameof(request));
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