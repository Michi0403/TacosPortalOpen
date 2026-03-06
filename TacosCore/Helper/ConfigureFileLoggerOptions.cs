//-----------------------------------------------------------------------
// <copyright file="ConfigureFileLoggerOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.Extensions.Options;
using TacosCore.BusinessObjects;

namespace TacosCore.Helper
{
    public class ConfigureFileLoggerOptions(IOptionsMonitor<FileLoggerCoreOptions> loggingOptions) : IConfigureOptions<FileLoggerCoreOptions>
    {

        public void Configure(FileLoggerCoreOptions options)
        {
            loggingOptions.CurrentValue.FilePath = options.FilePath;

            loggingOptions.CurrentValue.CoreLogLevel = options.CoreLogLevel;


        }
    }
}

