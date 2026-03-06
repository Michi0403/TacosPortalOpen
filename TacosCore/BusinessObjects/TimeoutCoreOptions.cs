//-----------------------------------------------------------------------
// <copyright file="TimeoutCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class TimeoutCoreOptions
    {
        public const string Timeout = "TimeoutCoreOptions";

        public int Milliseconds { get; set; } = int.MinValue;
    }
}
