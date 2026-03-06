//-----------------------------------------------------------------------
// <copyright file="UriBuilderHelper.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text;

namespace TacosCore.Helper
{
    public static class UriBuilderHelper
    {
        /// <summary>
        /// Builds a full absolute URI based on the application's Kestrel configuration and a relative path.
        /// </summary>
        /// <param name="relativePath">A path like "chathub" or "api/messages"</param>
        /// <returns>A combined Uri</returns>
        public static Uri BuildAbsoluteUriFromConfig(string relativePath)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            var baseUrl = $"http://localhost:{port}";

            var normalizedBase = baseUrl.Replace("0.0.0.0", "localhost").TrimEnd('/');
            var combined = string.IsNullOrWhiteSpace(relativePath)
                ? normalizedBase
                : $"{normalizedBase}/{relativePath.TrimStart('/')}";

            return new Uri(combined, UriKind.Absolute);
        }
        public static string BuildODataQuery(params (string Key, string? Value)[] parts)
        {
            var sb = new StringBuilder();
            var first = true;
            foreach (var (key, value) in parts)
            {
                if (string.IsNullOrWhiteSpace(value)) continue;
                if (!first) sb.Append('&'); else first = false;

                sb.Append(key);
                sb.Append('=');

                sb.Append(Uri.EscapeDataString(value));
            }
            return sb.ToString();
        }
    }
}
