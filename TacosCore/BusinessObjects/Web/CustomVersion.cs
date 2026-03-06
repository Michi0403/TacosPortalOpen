//-----------------------------------------------------------------------
// <copyright file="CustomVersion.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using static TacosCore.BusinessObjects.Web.CustomVersion;

namespace TacosCore.BusinessObjects.Web
{
    public class CustomVersion : ICustomVersion
    {

        public CustomVersion(string version)
        {
            Version = version;
        }

        public string Version { get; set; }

        public interface ICustomVersion
        {
            string Version { get; set; }
        }
    }
}
