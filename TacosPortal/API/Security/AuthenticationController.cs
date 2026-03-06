//-----------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosPortal.API.Security;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IStandardAuthenticationService securityAuthenticationService, ISecurityProvider securityProvider, IAuthenticationTokenProvider tokenProvider, ILogger<AuthenticationController> logger) : ControllerBase
{
    [HttpPost(nameof(Authenticate))]
    [SwaggerOperation("Checks if the user with the specified logon parameters exists in the database. If it does, authenticates this user.", "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>AuthenticationCore</a>.")]
    [Produces("application/json")]
    public IActionResult Authenticate(
        [FromBody]
        [SwaggerRequestBody(@"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }")]
        AuthenticationStandardLogonParameters logonParameters
    )
    {
        try
        {
            return Ok(tokenProvider.Authenticate(logonParameters));
        }
        catch (AuthenticationException ex)
        {
            logger.LogError(ex, "Authenticate failed for user {UserName}", logonParameters.UserName);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Authenticate failed with exception: {ex.ToString()}");
            throw;
        }
    }

    [HttpPost(nameof(LoginAsync))]
    [Produces("application/json")]
    [SwaggerOperation("Checks if the user with the specified logon parameters exists in the database. If it does, authenticates this user.", "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>AuthenticationCoreOptions</a>.")]
    public async Task<IActionResult> LoginAsync([FromBody] [SwaggerRequestBody(@"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }")]
        AuthenticationStandardLogonParameters logonParameters)
    {
        try
        {
            var principal = securityAuthenticationService.Authenticate(logonParameters);
            if (principal == null) return Unauthorized("User name or password is incorrect.");

            var identity = principal.Identity as ClaimsIdentity
                           ?? new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!(principal.Identity is ClaimsIdentity))
                principal = new ClaimsPrincipal(identity);

            var idValue = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idValue, out var userId))
                return Unauthorized("No NameIdentifier claim in principal.");

            var osFactory = HttpContext.RequestServices.GetRequiredService<INonSecuredObjectSpaceFactory>();
            using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationUser>();
            var dbUser = os.FirstOrDefault<ApplicationUser>(u => u.ID == userId);
            if (dbUser is null) return Unauthorized("User not found.");

            var existing = new HashSet<string>(
                identity.FindAll(ClaimTypes.Role).Select(c => c.Value),
                StringComparer.Ordinal);

            foreach (var r in dbUser.Roles)
                if (!string.IsNullOrWhiteSpace(r?.Name) && existing.Add(r.Name))
                    identity.AddClaim(new Claim(ClaimTypes.Role, r.Name));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { AllowRefresh = true, IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) });

            return Ok();
        }
        catch (AuthenticationException ex)
        {
            logger.LogError(ex, $"LoginAsync failed for user {ex.UserName} {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"LoginAsync failed with exception: {ex.ToString()}");
            throw;
        }
    }

    [HttpPost(nameof(LogoutAsync))]
    [Produces("application/json")]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);
            return Ok("Logout successful");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Logout failed with exception:  {ex.ToString()}");
            throw;
        }

    }

    [Authorize]
    [HttpGet(nameof(UserInfo))]
    [Produces("application/json")]
    public IActionResult UserInfo()
    {
        try
        {
            var user = HttpContext.User;

            if ((user.Identity == null) || !user.Identity.IsAuthenticated)
            {
                return Unauthorized("TelegramUser is not authenticated.");
            }

            var userName = user.Identity.Name;
            var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var xafUser = (ApplicationUser)securityProvider.GetSecurity().User;

            var customClaims = user.Claims
                .Where(c => c.Type != ClaimTypes.Name &&
                            c.Type != ClaimTypes.NameIdentifier &&
                            c.Type != ClaimTypes.Email &&
                            c.Type != ClaimTypes.Role)
                .ToDictionary(c => c.Type, c => c.Value);

            foreach (var claim in customClaims)
            {
                _ = xafUser.CustomClaims.TryAdd(claim.Key, claim.Value);
            }
            return Ok(xafUser);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UserInfo failed with exception: {Exception}", ex.Message);
            throw;
        }

    }
}
