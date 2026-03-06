//-----------------------------------------------------------------------
// <copyright file="AuditSaveChangesInterceptor.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosPortal.Services
{
    public class AuditSaveChangesInterceptor(ICurrentUserAccessor user, ILogger<AuditSaveChangesInterceptor> logger) : SaveChangesInterceptor
    {
        private static string FormatValues(PropertyValues? values, ILogger logger)
        {
            try
            {
                if (values == null) return string.Empty;

                var sb = new StringBuilder();
                foreach (var prop in values.Properties)
                {
                    var val = values[prop];
                    sb.Append(prop.Name)
                      .Append('=')
                      .Append(val?.ToString() ?? "NULL")
                      .Append("; ");
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in FormatValues: {ex.ToString()}");
                return string.Empty;
            }

        }
        private static string GetPrimaryKeyValue(EntityEntry e, ILogger logger)
        {
            try
            {
                var key = e.Properties.Where(p => p.Metadata.IsPrimaryKey()).Select(p => p.CurrentValue ?? p.OriginalValue);
                return string.Join(",", key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetPrimaryKeyValue ex: {ex.ToString()}");
                throw;
            }

        }

        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            try
            {
                logger.LogInformation($">> SaveChangesFailedAsync {eventData.ToString()} .");
                return base.SaveChangesFailedAsync(eventData, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SaveChangesFailedAsync: Problems in creating Database Log for eventData:{eventData.ToString()} Ex: {ex.ToString()}");
                throw;
            }

        }
        public override int SavedChanges(SaveChangesCompletedEventData e, int result)
        {
            try
            {
                logger.LogInformation($">> Saved {result} entities (including audit rows) {e.ToString()}.");
                return base.SavedChanges(e, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SavedChanges: Problems in creating Database Log for e:{e.ToString()} result:{result.ToString()} Ex: {ex.ToString()}");
                throw;
            }

        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(eventData);
                logger.LogInformation($"Data Access: {eventData.ToString()} ICurrentUserAccessor: {user.ToString()}");

                var db = eventData.Context!;
                if (db == null) return base.SavingChanges(eventData, result);

                var now = DateTime.UtcNow;
                var userId = user.UserId;
                var userName = user.UserName;
                var requestId = user.RequestId;

                static bool IsAudit(EntityEntry e) => e.Entity is DatabaseLog;

                var entries = db.ChangeTracker.Entries()
                    .Where(e => !IsAudit(e) && e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                    .ToList();

                if (entries.Count == 0 || (userId == null)) return base.SavingChanges(eventData, result);

                var auditSet = db.Set<DatabaseLog>();
                foreach (var e in entries)
                {
                    var entityType = e.Metadata.ClrType.Name;
                    var key = GetPrimaryKeyValue(e, logger);

                    switch (e.State)
                    {
                        case EntityState.Added:
                            auditSet.Add(new DatabaseLog
                            {
                                EntityType = entityType,
                                EntityKey = key,
                                Operation = "Insert",
                                UtcTimestamp = now,
                                UserId = userId,
                                UserName = userName,
                                RequestId = requestId,
                                NewValue = FormatValues(e.CurrentValues, logger)
                            });
                            break;

                        case EntityState.Deleted:
                            auditSet.Add(new DatabaseLog
                            {
                                EntityType = entityType,
                                EntityKey = key,
                                Operation = "Delete",
                                UtcTimestamp = now,
                                UserId = userId,
                                UserName = userName,
                                RequestId = requestId,
                                NewValue = FormatValues(e.CurrentValues, logger)
                            });
                            break;

                        case EntityState.Modified:
                            foreach (var p in e.Properties.Where(p => p.IsModified))
                            {
                                if (p.Metadata.IsPrimaryKey()) continue;

                                var oldVal = p.OriginalValue?.ToString();
                                var newVal = p.CurrentValue?.ToString();
                                if (oldVal == newVal) continue;

                                auditSet.Add(new DatabaseLog
                                {
                                    EntityType = entityType,
                                    EntityKey = key,
                                    Operation = "Update",
                                    PropertyName = p.Metadata.Name,
                                    OldValue = oldVal,
                                    NewValue = newVal,
                                    UtcTimestamp = now,
                                    UserId = userId,
                                    UserName = userName,
                                    RequestId = requestId
                                });
                            }
                            break;
                    }
                }

                return base.SavingChanges(eventData, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in SavingChanges: Problems in creating Database Log for {eventData.ToString()} Ex: {ex.ToString()}");
                throw;
            }

        }
    }
}
