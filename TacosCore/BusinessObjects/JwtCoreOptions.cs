//-----------------------------------------------------------------------
// <copyright file="JwtCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class JwtCoreOptions
    {
        public const string JwtCore = "JwtCore";

        public string? Audience { get; set; }

        public int ExpirationMinutes { get; set; }

        public string? Issuer { get; set; }

        public string? IssuerSigningKey { get; set; }
    }
}
