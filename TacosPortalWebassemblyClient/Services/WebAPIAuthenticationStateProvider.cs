//-----------------------------------------------------------------------
// <copyright file="WebAPIAuthenticationStateProvider.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.Extensions;

namespace TacosPortalWebassemblyClient.Services;

public class WebAPIAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private ClaimsPrincipal _claimsPrincipal = new(new ClaimsIdentity());
    private readonly HttpClient _httpClient;
    private readonly ILogger<WebAPIAuthenticationStateProvider> _logger;
    private bool disposedValue;

    public WebAPIAuthenticationStateProvider(ILogger<WebAPIAuthenticationStateProvider> logger, IHttpClientFactory ClientFactory)
    {
        ArgumentNullException.ThrowIfNull(ClientFactory);
        _logger = logger;
        _httpClient = ClientFactory.CreateClient("WasmClient");
    }








    void IDisposable.Dispose()
    {

        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        try
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();


                }



                disposedValue = true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Dispose");
            throw;
        }
    }


    public void ClearAuthInfo()
    {
        try
        {

            _logger.LogInformation("Clearing authentication info.");

            _claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());



            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(_claimsPrincipal)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in ClearAuthInfo");
            throw;
        }

    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            _logger.LogInformation("GetAuthenticationStateAsync called.");


            if (_claimsPrincipal.Identity?.IsAuthenticated ?? false)
            {
                return new AuthenticationState(_claimsPrincipal);
            }

            try
            {

                var (message, user) = await _httpClient.GetMyUserAsync(_logger).ConfigureAwait(false);
                if (user != null)
                {
                    _logger.LogInformation($"SetAuthInfo for {user.ToString()}.");
                    SetAuthInfo(user);
                }
                else
                {
                    _logger.LogInformation("User from Api was null... Clearing authentication info.");
                    ClearAuthInfo();
                }

            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning("User not authenticated or error retrieving user info: {Message}", ex.Message);
                ClearAuthInfo();
            }

            return new AuthenticationState(_claimsPrincipal);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetAuthenticationStateAsync");
            throw;
        }

    }

    public void SetAuthInfo(ApplicationUser user)
    {
        try
        {
            _logger.LogInformation("Setting authentication info for user: {UserName}", user.UserName);

            ArgumentNullException.ThrowIfNull(user);
            var mail = user.Email ?? string.Empty;
            ArgumentNullException.ThrowIfNull(user.ID);
            var tmpXafUser = user.ID.ToString();
            ArgumentNullException.ThrowIfNull(tmpXafUser);
            var xafUserID = tmpXafUser ?? string.Empty;
            var loginProviderID = !string.IsNullOrWhiteSpace(user.LoginProviderUserId)
                ? user.LoginProviderUserId
                : string.Empty;
            var userName = user.UserName ?? string.Empty;
            var idClaim = user.ToString();

            var identity = new ClaimsIdentity(new[]
            {
        new Claim(nameof(ApplicationUser.Email),          mail),
        new Claim(nameof(ApplicationUser.ID),      xafUserID),
        new Claim(nameof(ApplicationUser.LoginProviderUserId), loginProviderID),
        new Claim(ClaimTypes.Name,                  userName),
        new Claim("ID",                             idClaim!)
    }, "AuthCookie");


            var principal = new ClaimsPrincipal(identity);

            _claimsPrincipal = principal;


            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(_claimsPrincipal)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in SetAuthInfo");
            throw;
        }

    }
}