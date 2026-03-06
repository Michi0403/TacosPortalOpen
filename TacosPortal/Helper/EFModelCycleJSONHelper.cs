//-----------------------------------------------------------------------
// <copyright file="EFModelCycleJSONHelper.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace TacosPortal.Helper
{

    public static class EFModelCycleJSONHelper
    {

        private static void Traverse(
            IEntityType type,
            Stack<string> path,
            HashSet<IEntityType> visited,
            ILogger? logger,
            int depth,
            int maxDepth)
        {
            if (depth > maxDepth)
            {
                logger?.LogInformation("⚠️ Deep navigation path detected (> {MaxDepth}): {Path}", maxDepth, string.Join(" → ", path));
                return;
            }

            if (visited.Contains(type))
            {
                logger?.LogWarning("❌ CYCLE DETECTED: {CyclePath}", string.Join(" → ", path) + " → " + type.Name);
                return;
            }

            _ = visited.Add(type);
            path.Push(type.Name);

            foreach (var nav in type.GetNavigations())
            {
                Traverse(nav.TargetEntityType, path, new HashSet<IEntityType>(visited), logger, depth + 1, maxDepth);
            }

            _ = path.Pop();
        }

        public static void CheckForNavigationCycles(DbContext context, ILogger? logger = null, int maxDepth = 64)
        {
            _ = new HashSet<string>();
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                var path = new Stack<string>();
                Traverse(entityType, path, new HashSet<IEntityType>(), logger, 0, maxDepth);
            }
        }

        public static class ShallowEntityCleaner
        {
            public static T Clean<T>(T entity) where T : class, new()
            {
                if (entity == null) return null;

                var entityType = typeof(T);
                var result = new T();

                foreach (var prop in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.CanWrite || !prop.CanRead) continue;

                    var value = prop.GetValue(entity);
                    var type = prop.PropertyType;

                    if (value == null || type.IsPrimitive || type == typeof(string) || type.IsEnum || type == typeof(Guid) || type == typeof(DateTime))
                    {
                        prop.SetValue(result, value);
                    }
                    else if (!typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
                    {
                        var idProp = type.GetProperty("ID");
                        if (idProp != null)
                        {
                            var navValue = idProp.GetValue(value);
                            if (navValue != null)
                            {
                                var navStub = Activator.CreateInstance(type);
                                idProp.SetValue(navStub, navValue);
                                prop.SetValue(result, navStub);
                            }
                        }
                    }
                }

                return result;
            }
            public static Dictionary<string, object?> ShallowFlatten(object entity)
            {
                var dict = new Dictionary<string, object?>();
                var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    var value = prop.GetValue(entity);
                    var type = prop.PropertyType;

                    if (value == null || type.IsPrimitive || type == typeof(string) || type == typeof(Guid) || type == typeof(DateTime))
                    {
                        dict[prop.Name] = value;
                    }
                    else if (type.GetProperty("ID") is { } idProp)
                    {
                        var idVal = idProp.GetValue(value);
                        dict[$"{prop.Name}Id"] = idVal;
                    }
                }

                return dict;
            }
        }
        public static class EntityCloner
        {
            public static object CreateShallowSerializableClone(object entity)
            {
                if (entity == null) return null;

                Type entityType = entity.GetType();
                var clone = Activator.CreateInstance(entityType)!;

                foreach (PropertyInfo prop in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.CanRead || !prop.CanWrite)
                        continue;

                    var value = prop.GetValue(entity);

                    if (value == null || prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(DateTime) || prop.PropertyType.IsEnum)
                    {
                        prop.SetValue(clone, value);
                    }
                    else if (!typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        var navObject = value;
                        if (navObject != null)
                        {
                            var navType = navObject.GetType();
                            var shallowNav = Activator.CreateInstance(navType);

                            var idProp = navType.GetProperty("ID");
                            if (idProp != null)
                            {
                                var idValue = idProp.GetValue(navObject);
                                idProp.SetValue(shallowNav, idValue);
                                prop.SetValue(clone, shallowNav);
                            }
                        }
                    }
                }

                return clone;
            }
        }
    }
}
