//-----------------------------------------------------------------------
// <copyright file="AuthenticationCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class AuthenticationCoreOptions()
    {
        public const string AuthenticationCore = "AuthenticationCore";

        public JwtCoreOptions? JwtCore { get; set; }

        public string? Password { get; set; }

        public string? URL { get; set; }

        public string? Username { get; set; }
    }
}