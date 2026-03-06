//-----------------------------------------------------------------------
// <copyright file="FileLoggerCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects
{
    public class FileLoggerCoreOptions
    {
        public const string FileLoggerCore = "FileLoggerCore";

        public FileLoggerCoreOptions CloneOptions(FileLoggerCoreOptions options)
        {
            return new FileLoggerCoreOptions
            {
                CoreLogLevel = options.CoreLogLevel,
                FilePath = options.FilePath
            };
        }

        [JsonInclude]
        public CoreLogLevel CoreLogLevel { get; set; }
        [JsonInclude]
        public string? FilePath { get; set; }
    }
}
