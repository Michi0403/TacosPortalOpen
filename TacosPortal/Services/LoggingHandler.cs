//-----------------------------------------------------------------------
// <copyright file="LoggingHandler.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosPortal.Services
{
    public class LoggingHandler(ILogger<LoggingHandler> logger) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request, nameof(request));
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false); ;
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SendAsync LoggingHandler {ex.ToString()}");
                throw;
            }

        }
    }
}
