//-----------------------------------------------------------------------
// <copyright file="KestrelOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class KestrelOptions()
    {
        public const string Kestrel = "Kestrel";

        public EndpointOptions Endpoints { get; set; } = new();
    }

    public class EndpointOptions()
    {
        public const string Endpoints = "Endpoints";

        public HttpOptions Http { get; set; } = new();
    }

    public class HttpOptions()
    {
        public const string Http = "Http";

        public string Url { get; set; } = string.Empty;
    }
}