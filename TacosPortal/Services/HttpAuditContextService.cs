//-----------------------------------------------------------------------
// <copyright file="HttpAuditContextService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.Security;
using System.Security.Claims;

namespace TacosPortal.Services
{
    public interface IHttpAuditContextService
    {
        string? RequestId { get; }
        Guid? UserId { get; }
        string? UserName { get; }
    }

    internal class HttpAuditContextService(IHttpContextAccessor http, ISecurityProvider security) : IHttpAuditContextService
    {

        public string? RequestId => http.HttpContext?.TraceIdentifier;

        public Guid? UserId
        {
            get
            {
                var id = http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(id, out var g) ? g : null;
            }
        }
        public string? UserName => http.HttpContext?.User?.Identity?.Name;
    }
}
