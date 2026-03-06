//-----------------------------------------------------------------------
// <copyright file="OdataID.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects
{
    public class OdataID
    {
        [JsonPropertyName("@odata.id")]
        public string ID { get; set; } = string.Empty;
    }
}
