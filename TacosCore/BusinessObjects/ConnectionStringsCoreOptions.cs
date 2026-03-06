//-----------------------------------------------------------------------
// <copyright file="ConnectionStringsCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class ConnectionStringsCoreOptions()
    {
        public const string ConnectionStringsCore = "ConnectionStringsCore";

        public string? ConnectionString { get; set; }

        public string? DefaultConnection { get; set; }

        public string? EasyTestConnectionString { get; set; }
    }
}