//-----------------------------------------------------------------------
// <copyright file="EmailLoggerCoreOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects
{
    public class EmailLoggerCoreOptions
    {
        public const string EmailLoggerCore = "EmailLoggerCore";

        [JsonInclude]
        public IEnumerable<string> BccRecipients { get; set; } = new List<string>();

        [JsonInclude]
        public IEnumerable<string> CcRecipients { get; set; } = new List<string>();
        [JsonInclude]
        public CoreLogLevel CoreLogLevel { get; set; }
        [JsonInclude]
        public IEnumerable<string> EmailRecipients { get; set; } = new List<string>();
        [JsonInclude]
        public bool EnableSsl { get; set; }
        [JsonInclude]
        public string? Password { get; set; }
        [JsonInclude]
        public string? SenderEmail { get; set; }
        [JsonInclude]
        public int SmtpPort { get; set; }
        [JsonInclude]
        public string? SmtpServer { get; set; }
        [JsonInclude]
        public string? Username { get; set; }
    }
}
