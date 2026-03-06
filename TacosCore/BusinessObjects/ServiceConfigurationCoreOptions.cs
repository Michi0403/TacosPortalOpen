//-----------------------------------------------------------------------
// <copyright file="ServiceConfigurationCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class ServiceConfigurationCoreOptions
    {
        public static readonly string ServiceConfigurationCore = "ServiceConfigurationCore";

        public string ApiPassword { get; init; } = default!;

        public string ApiUser { get; init; } = default!;
    }
}
