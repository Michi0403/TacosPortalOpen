//-----------------------------------------------------------------------
// <copyright file="CurrentUserAccessorService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using System.Security.Claims;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;

namespace TacosPortal.Services
{


    public sealed class CurrentUserAccessorService(IHttpContextAccessor http, IAmbientUserContext ambient, ILogger<CurrentUserAccessorService> logger) : ICurrentUserAccessor
    {

        private Guid? TryHttpUserId()
        {
            try
            {
                var user = http.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated != true) return null;

                var raw = user.FindFirst("sub")?.Value
                       ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(raw, out var g) ? g : null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in TryHttpUserName: {ex.ToString()}");

            }
            return null;
        }

        private string? TryHttpUserName()
        {
            try
            {
                var user = http.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated != true) return null;
                return user.Identity?.Name
                    ?? user.FindFirst(ClaimTypes.Name)?.Value
                    ?? user.FindFirst("preferred_username")?.Value;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in TryHttpUserName: {ex.ToString()}");

            }
            return null;
        }

        private Guid? TryXafUserId()
        {
            try
            {
                if (!SecuritySystem.IsAuthenticated) return null;
                if (SecuritySystem.CurrentUser is TacoPermissionPolicyUser au)
                    return au.ID;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in TryXafUserId: {ex.ToString()}");

            }
            return null;
        }

        private string? TryXafUserName()
        {
            try
            {
                if (!SecuritySystem.IsAuthenticated) return null;
                if (SecuritySystem.CurrentUser is TacoPermissionPolicyUser au)
                    return au.UserName;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in TryXafUserName: {ex.ToString()}");

            }
            return null;
        }

        public string? RequestId
            => http.HttpContext?.TraceIdentifier;
        public Guid? UserId
        {
            get
            {
                try
                {
                    var uid = TryHttpUserId();
                    if (uid != null) return uid;

                    uid = TryXafUserId();
                    if (uid != null) return uid;

                    return ambient.UserId;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error in get UserId: {ex.ToString()}");

                }
                return null;
            }
        }

        public string? UserName
            => TryHttpUserName() ?? TryXafUserName() ?? ambient.UserName;
    }
}
