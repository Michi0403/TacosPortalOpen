//-----------------------------------------------------------------------
// <copyright file="ConfigureEmailLoggerOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.Extensions.Options;
using TacosCore.BusinessObjects;

namespace TacosCore.Helper
{
    public class ConfigureEmailLoggerOptions(IOptionsMonitor<EmailLoggerCoreOptions> loggingOptions) : IConfigureOptions<EmailLoggerCoreOptions>
    {

        public void Configure(EmailLoggerCoreOptions options)
        {
            loggingOptions.CurrentValue.SmtpServer = options.SmtpServer;
            loggingOptions.CurrentValue.SmtpPort = options.SmtpPort;
            loggingOptions.CurrentValue.SenderEmail = options.SenderEmail;
            loggingOptions.CurrentValue.EmailRecipients = options.EmailRecipients;
            loggingOptions.CurrentValue.CcRecipients = options.CcRecipients;
            loggingOptions.CurrentValue.BccRecipients = options.BccRecipients;
            loggingOptions.CurrentValue.Username = options.Username;
            loggingOptions.CurrentValue.CoreLogLevel = options.CoreLogLevel;
            loggingOptions.CurrentValue.Password = options.Password;
            loggingOptions.CurrentValue.EnableSsl = options.EnableSsl;
        }
    }
}
