//-----------------------------------------------------------------------
// <copyright file="CustomRevalidationAuthenticationStateProvider.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;
using System.Text;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosPortal.Services
{
    public class CustomRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomRevalidatingAuthenticationStateProvider> _logger;
        private readonly IObjectSpaceFactory _securedObjectSpaceFactory;
        private readonly ISecurityStrategyBase _security;

        public CustomRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            ISecurityStrategyBase security,
            IObjectSpaceFactory securedObjectSpaceFactory,
            IHttpContextAccessor httpContextAccessor)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomRevalidatingAuthenticationStateProvider>();
            _security = security;
            _securedObjectSpaceFactory = securedObjectSpaceFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            try
            {
                var user = authenticationState.User;

                _logger.LogWarning($"Identity: {user.Identity?.Name}, Authenticated: {user.Identity?.IsAuthenticated}");

                foreach (var claim in user.Claims)
                {
                    _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
                }

                if (!user.Identity?.IsAuthenticated ?? true)
                {
                    _logger.LogWarning($"Identity: {user.Identity?.Name},  is not authenticated."
                    );
                    return false;
                }

                _logger.LogInformation($"Identity: {user.Identity?.Name} User is authenticated. Starting validation..."
                    );

                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = user.FindFirst(ClaimTypes.Name)?.Value;
                var xafSecurity = user.FindFirst("XafSecurity")?.Value;
                var xafAuthPassed = user.FindFirst("XafSecurityAuthPassed")?.Value;
                var logonParams = user.FindFirst("XafLogonParams")?.Value;

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _logger.LogWarning($"Identity: {user.Identity?.Name} ClaimTypes.NameIdentifier not found or empty."
                    );
                    return false;
                }

                if (string.IsNullOrWhiteSpace(username))
                {
                    _logger.LogWarning($"Identity: {user.Identity?.Name} ClaimTypes.Name {username} missing."
                    );
                    return false;
                }

                if (xafSecurity != "XafSecurity" || xafAuthPassed != "XafSecurityAuthPassed")
                {
                    _logger.LogWarning($"Identity: {user.Identity?.Name} Required XAF claims are missing or incorrect."
                   );
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(logonParams))
                {
                    try
                    {

                        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(logonParams));
                        _logger.LogInformation($"Identity: {username} Decoded XafLogonParams: {decoded}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Identity: Failed to decode XafLogonParams. {logonParams.ToString()}",
                    user.Identity?.Name);
                        return false;
                    }
                }

                _logger.LogInformation($"Identity: {username} Extracted user ID from claims: {userId}");

                try
                {
                    using var objectSpace = _securedObjectSpaceFactory.CreateObjectSpace<ApplicationUser>();
                    var xafUser = objectSpace.FirstOrDefault<ApplicationUser>(u => u.ID.ToString() == userId);

                    if (xafUser == null)
                    {
                        _logger.LogWarning($"Identity: {user.Identity?.Name}  with ID {userId} not found in database."
                   );
                        return false;
                    }

                    if (!xafUser.IsActive)
                    {
                        _logger.LogWarning($"Identity: {user.Identity?.Name}  {userId} is inactive."
                     );
                        return false;
                    }

                    if (!string.Equals(xafUser.UserName, username, StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogWarning($"Claim username '{xafUser.UserName}' does not match DB username '{username}'.");
                        return false;
                    }

                    _logger.LogInformation($"Identity: {userId}  successfully validated.");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Identity: Failed to validate user with ID {userId}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in ValidateAuthenticationStateAsync: {ex.ToString()}");
                throw;
            }

        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var usertest = _security.User;

                _logger.LogWarning($"Identity: {user?.Identity?.Name}, Authenticated: {user?.Identity?.IsAuthenticated}"
                   );

                foreach (var claim in user?.Claims ?? Enumerable.Empty<Claim>())
                {
                    _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
                }

                if (user?.Identity?.IsAuthenticated ?? false)
                {
                    _logger.LogInformation("Returning authenticated user from HTTP context.");
                    return new AuthenticationState(user);
                }

                _logger.LogWarning("Returning anonymous user — not authenticated.");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetAuthenticationStateAsync: {ex.ToString()}");
                throw;
            }

        }
    }
}