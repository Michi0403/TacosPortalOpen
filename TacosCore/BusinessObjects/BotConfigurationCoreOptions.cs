//-----------------------------------------------------------------------
// <copyright file="BotConfigurationCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.BusinessObjects
{
    public class BotConfigurationCoreOptions
    {
        public static readonly string BotConfigurationCore = "BotConfigurationCore";

        public string BotName { get; init; } = default!;

        public string BotToken { get; init; } = default!;

        public string ChatId { get; init; } = default!;

        public string HostAddress { get; init; } = default!;

        public string Route { get; init; } = default!;

        public string SecretToken { get; init; } = default!;
    }
}
