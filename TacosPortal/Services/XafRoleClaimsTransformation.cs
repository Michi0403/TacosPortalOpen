//-----------------------------------------------------------------------
// <copyright file="XafRoleClaimsTransformation.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosPortal.Services
{
    public sealed class XafRoleClaimsTransformation : IClaimsTransformation
    {
        private readonly ILogger<XafRoleClaimsTransformation> _logger;
        private readonly INonSecuredObjectSpaceFactory _osFactory;

        public XafRoleClaimsTransformation(INonSecuredObjectSpaceFactory osFactory, ILogger<XafRoleClaimsTransformation> logger)
        {
            _osFactory = osFactory;
            _logger = logger;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            if (identity?.IsAuthenticated != true) return Task.FromResult(principal);

            if (identity.FindFirst(ClaimTypes.Role) is not null) return Task.FromResult(principal);

            var idValue = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idValue, out var userId)) return Task.FromResult(principal);

            try
            {
                using var os = _osFactory.CreateNonSecuredObjectSpace<ApplicationUser>();
                var user = os.FirstOrDefault<ApplicationUser>(u => u.ID == userId);
                if (user != null)
                {
                    foreach (var role in user.Roles)
                        if (!string.IsNullOrWhiteSpace(role?.Name))
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add role claims for {UserId}", userId);
            }

            return Task.FromResult(principal);
        }
    }
}
