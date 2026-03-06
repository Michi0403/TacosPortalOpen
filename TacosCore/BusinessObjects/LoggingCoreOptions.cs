//-----------------------------------------------------------------------
// <copyright file="LoggingCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class LoggingCoreOptions
    {
        public const string LoggingCore = "LoggingCore";

        public CoreLogLevel CoreLogLevel { get; set; }
        public EmailLoggerCoreOptions? EmailCore { get; set; }

        public FileLoggerCoreOptions? FileCore { get; set; }
    }
}
