//-----------------------------------------------------------------------
// <copyright file="EfCommandDiagnosticsInterceptor.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Diagnostics;

namespace TacosPortal.Services
{
    public class EfCommandDiagnosticsInterceptor(IHttpAuditContextService ctx, ILogger<EfCommandDiagnosticsInterceptor> logger) : DbCommandInterceptor
    {
        private readonly TimeSpan _slow = TimeSpan.FromMilliseconds(400);
        private readonly ConcurrentDictionary<Guid, Stopwatch> _timers = new();

        private void Start(CommandEventData eventData)
        {
            var sw = Stopwatch.StartNew();
            _timers[eventData.CommandId] = sw;
        }

        private void StopAndLog(string kind, DbCommand command, CommandEndEventData eventData)
        {
            if (_timers.TryRemove(eventData.CommandId, out var sw))
            {
                sw.Stop();
                if (sw.Elapsed >= _slow)
                {
                    logger.LogWarning("SLOW {Kind} ({Elapsed}): {Sql}",
                        kind, sw.Elapsed, command.CommandText);
                }
            }
        }

        public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            try
            {
                logger.LogError(eventData.Exception, "EF COMMAND FAILED: {Sql}", command.CommandText);
                _timers.TryRemove(eventData.CommandId, out _);
                base.CommandFailed(command, eventData);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in CommandFailed: {ex.ToString()}");
                throw;
            }

        }

        public override DbDataReader ReaderExecuted(
            DbCommand command, CommandExecutedEventData eventData,
            DbDataReader result)
        {
            try
            {
                StopAndLog("READ", command, eventData);
                return base.ReaderExecuted(command, eventData, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in ReaderExecuted");
                throw;
            }

        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            try
            {
                Start(eventData);
                return base.ReaderExecuting(command, eventData, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in ReaderExecution :{ex.ToString()}");
                throw;
            }

        }
    }
}
