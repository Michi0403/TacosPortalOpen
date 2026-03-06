//-----------------------------------------------------------------------
// <copyright file="DuplicatePropertyCheckerService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TacosPortal.BusinessObjects;

namespace TacosPortal.Services
{
    public interface IDuplicatePropertyCheckerService
    {
        void CheckForDuplicateProperties();
    }

    public class DuplicatePropertyCheckerService(ILogger<DuplicatePropertyCheckerService> logger, IDbContextFactory<TacoContext> contextFactory) : IDuplicatePropertyCheckerService
    {


        public void CheckForDuplicateProperties()
        {
            try
            {
                using (var tacoContext = contextFactory.CreateDbContext())
                {
                    var entityTypes = tacoContext.Model.GetEntityTypes()
               .Select(et => et.ClrType)
               .Where(t => t != null)
               .Distinct();

                    foreach (var type in entityTypes)
                    {
                        var allProperties = new List<PropertyInfo>();

                        var currentType = type;
                        while (currentType != null && currentType != typeof(object))
                        {
                            var declaredProps = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                            allProperties.AddRange(declaredProps);

                            currentType = currentType.BaseType;
                        }

                        var grouped = allProperties.GroupBy(p => p.Name)
                                                   .Where(g => g.Count() > 1)
                                                   .ToList();

                        if (grouped.Any())
                        {
                            logger.LogCritical($"❗ Duplicate property names found in type hierarchy: {type.FullName}");
                            foreach (var group in grouped)
                            {
                                logger.LogCritical($"  ⚠ Property '{group.Key.ToString()}' defined in:");
                                foreach (var prop in group)
                                {
                                    logger.LogCritical($"     - {prop.DeclaringType?.FullName}");
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while checking for duplicate properties.{ex.ToString()}");
            }

        }

    }
}
