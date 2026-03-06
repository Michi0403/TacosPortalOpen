//-----------------------------------------------------------------------
// <copyright file="UrlGenerator.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web;

namespace TacosPortal.Services
{
    public static class UrlGenerator
    {
        public const string ToggleSidebarName = "toggledSidebar";

        public static string GetUrl(string baseUrl, bool toggledSidebar)
        {
            return $"{baseUrl}?{ToggleSidebarName}={toggledSidebar}";
        }
        public static string GetUrl(bool toggledSidebar, string returnUrl)
        {
            var baseUriBuilder = new UriBuilder(returnUrl);
            var query = HttpUtility.ParseQueryString(baseUriBuilder.Query);
            var baseUrl = baseUriBuilder.Fragment + baseUriBuilder.Host + baseUriBuilder.Path;

            return $"{baseUrl}?{ToggleSidebarName}={toggledSidebar}&{query}";
        }
    }
}