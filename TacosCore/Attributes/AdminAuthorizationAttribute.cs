//-----------------------------------------------------------------------
// <copyright file="AdminAuthorizationAttribute.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Authorization;

namespace TacosCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        public AdminAuthorizationAttribute() { Roles = "Administrators"; }
    }
}