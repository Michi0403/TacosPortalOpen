//-----------------------------------------------------------------------
// <copyright file="EmailLogger.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using TacosCore.BusinessObjects;
using TacosCore.Helper;

namespace TacosCore.Logging
{
    public class EmailLogger : ILogger, IDisposable
    {
        private readonly Task _backgroundTask;
        private readonly EmailLoggerCoreOptions _config;
        private readonly BlockingCollection<(string message, Exception? exception)> _logQueue = new();

        public EmailLogger(string categoryName, IOptionsMonitor<EmailLoggerCoreOptions> optionsSnapshot)
        {
            _config = optionsSnapshot.CurrentValue;
            _backgroundTask = Task.Run(ProcessLogQueueAsync);
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            var scopeInfo = state?.ToString() ?? string.Empty;
            return new DisposableScope(scopeInfo);
        }

        private async Task ProcessLogQueueAsync()
        {
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60)))
                {
                    foreach (var logItem in _logQueue.GetConsumingEnumerable(cts.Token))
                    {
                        await SendEmailAsync(logItem.message, logItem.exception);
                    }
                }

            }
            catch (OperationCanceledException)
            {

            }
        }

        private async Task SendEmailAsync(string message, Exception? exception)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(_config.SenderEmail);
                ArgumentOutOfRangeException.ThrowIfLessThan(_config.EmailRecipients.Count(), 0);
                using var mailMessage = new MailMessage
                {

                    From = new MailAddress(_config.SenderEmail),
                    Subject = "Log MessageOriginBelongsTo",
                    Body = $"{message}{(exception != null ? $"\n\nException:\n{exception.StackTrace}" : string.Empty)}",
                    IsBodyHtml = false
                };

                foreach (var recipient in _config.EmailRecipients)
                    mailMessage.To.Add(recipient);

                foreach (var cc in _config.CcRecipients)
                    mailMessage.CC.Add(cc);

                foreach (var bcc in _config.BccRecipients)
                    mailMessage.Bcc.Add(bcc);

                using var smtpClient = new SmtpClient(_config.SmtpServer, _config.SmtpPort)
                {
                    Credentials = new NetworkCredential(_config.Username, _config.Password),
                    EnableSsl = _config.EnableSsl
                };

                await smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send log email: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _logQueue.CompleteAdding();
            try
            {
                _backgroundTask.Wait();
            }
            catch { }
            _logQueue.Dispose();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (int)logLevel >= (int)_config.CoreLogLevel;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = $"{DateTime.UtcNow} [Machine: {Environment.MachineName}] [Level: {logLevel}] {formatter(state, exception)}";


            _logQueue.Add((message, exception));
        }
    }
}