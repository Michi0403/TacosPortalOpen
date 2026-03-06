//-----------------------------------------------------------------------
// <copyright file="ApiCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class ApiCoreOptions()
    {
        public const string ApiCore = "ApiCore";

        public int? HttpPort { get; set; }

        public string? JScriptURL { get; set; }
    }
}