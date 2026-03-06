//-----------------------------------------------------------------------
// <copyright file="ODataResponse.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects
{
    public class ODataResponse<TValue>
    {
        [JsonPropertyName("@odata.context")]
        public string Context { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public IEnumerable<TValue>? Value { get; set; }
    }
}
