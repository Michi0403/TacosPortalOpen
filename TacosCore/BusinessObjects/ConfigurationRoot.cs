//-----------------------------------------------------------------------
// <copyright file="ConfigurationRoot.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class ConfigurationRoot
    {
        public const string Configuration = "Configuration";

        public ApiCoreOptions? ApiCore { get; set; }
        public AuthenticationCoreOptions? AuthenticationCore { get; set; }
        public BotConfigurationCoreOptions? BotConfigurationCore { get; set; }
        public ConnectionStringsCoreOptions? ConnectionStringsCore { get; set; }
        public KestrelOptions? Kestrel { get; set; }

        public LoggingCoreOptions? LoggingCore { get; set; }
        public PythonCoreOptions? PythonCore { get; set; }
        public ServiceConfigurationCoreOptions? ServiceConfigurationCore { get; set; }

        public TimeoutCoreOptions? TimeoutCore { get; set; }
    }
}
