//-----------------------------------------------------------------------
// <copyright file="HttpExtensions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TacosCore.Extensions
{
    public static class HttpExtensions
    {
        public static JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = null,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            IncludeFields = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true,
            Converters = { new JsonStringEnumConverter() },
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString

        };

        private static object? GetKeyFromMainType<TMain, TSub>(this TSub subItem, ILogger logger)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(subItem);
                var mainKeyProp = typeof(TMain).GetProperties()
                    .FirstOrDefault(p =>
                        p.GetCustomAttribute<KeyAttribute>() != null ||
                        string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase));

                if (mainKeyProp == null) return null;

                var subProp = subItem.GetType().GetProperty(mainKeyProp.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
                return subProp?.GetValue(subItem);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetKeyFromMainType Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in GetKeyFromMainType for {ex.ToString()}");
                throw;
            }
        }

        public static void CreateWASMClient(this IHttpClientFactory clientFactory, NavigationManager navigationManager, ILogger logger,
           out HttpClient? httpClient)
        {
            try
            {
                httpClient = clientFactory.CreateClient("WasmClient");

                if (httpClient.BaseAddress == null)
                {
                    httpClient.BaseAddress = new Uri(navigationManager.BaseUri);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in CreateClient {ex.ToString()}");
                httpClient = null;
            }
        }

        public static async Task<bool> DeleteAsync<T>(this HttpClient httpClient, T item, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(item);
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling DeleteAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var key = GetKey(item, logger);
                if (key is null) return false;

                var url = $"{GetRoute<T>()}({key})";
                Console.WriteLine($"URL:{url} MessageType:{item.GetType().FullName} Object:{item.ToJsonString(jsonSerializerOptions)}");
                var response = await httpClient.DeleteAsync(new Uri(url, UriKind.Relative), cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in DeleteRefAsync Request {ex.ToString()}");
                Console.WriteLine($"Error in DeleteAsync Request {ex.ToString()}");
                throw;
            }

        }
        public static async Task<bool> DeleteRefAsync<T, TRef>(this HttpClient httpClient, T mainType, TRef objectToDelete, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(mainType);
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling DeleteRefAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var mainKey = GetKey(mainType, logger);
                var refKey = GetKey(objectToDelete, logger);

                if (mainKey is null || refKey is null) return false;

                var propertyInfos = mainType.GetType().GetProperties();

                foreach (var prop in propertyInfos)
                {
                    if (prop.PropertyType == typeof(string)) continue;

                    bool isEnumerableOfTRef = prop.PropertyType.GetInterfaces()
                        .Any(i => i.IsGenericType &&
                                  i.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                                  i.GetGenericArguments()[0] == typeof(TRef));

                    if (!isEnumerableOfTRef) continue;

                    var collection = prop.GetValue(mainType) as IEnumerable<TRef>;
                    if (collection == null || !collection.Contains(objectToDelete)) continue;

                    var url = $"{GetRoute<T>()}({mainKey})/{prop.Name}({refKey})/$ref";

                    var response = await httpClient.DeleteAsync(new Uri(url, UriKind.Relative), linkedCts.Token).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to delete $ref from {typeof(T).Name}.{prop.Name}. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                        return false;
                    }

                    Console.WriteLine($"Successfully removed reference: {typeof(TRef).Name} from {typeof(T).Name}.{prop.Name}");
                    return true;
                }

                return false;
            }
            catch (JsonException ex)
            {

                logger.LogError(ex, $"JsonException in DeleteRefAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in DeleteRefAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in DeleteRefAsync Request {ex.ToString()}");
                throw;
            }
        }

        public static async Task<IEnumerable<T>> GetAllAsync<T>(this HttpClient httpClient, ILogger logger, CancellationToken? externalToken = null, string? additionalQuery = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling GetAllAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                string url;
                if (additionalQuery != null)
                {
                    url = $"{GetRoute<T>()}?{additionalQuery}";
                }
                else
                {
                    url = GetRoute<T>();
                }

                var oDataRespone = await httpClient.GetFromJsonAsync<ODataResponse<T>>(new Uri(url, UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false); ;

                if (oDataRespone == null || (oDataRespone != null && oDataRespone.Value == null))
                    return Enumerable.Empty<T>();
                else
                {
                    ArgumentNullException.ThrowIfNull(oDataRespone);
                    var odataValue = oDataRespone.Value;
                    ArgumentNullException.ThrowIfNull(odataValue);
                    return odataValue;
                }

            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"JsonException in GetAllAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetAllAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in GetAllAsync Request {ex.ToString()}");
                throw;
            }
        }

        public static async Task<IEnumerable<object>> GetAllAsync(this HttpClient httpClient, Type typeToGet, ILogger logger, CancellationToken? externalToken = null, string? additionalQuery = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling GetAllAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                string url;
                if (additionalQuery != null)
                {
                    url = $"{GetRoute(typeToGet)}?{additionalQuery}";
                }
                else
                {
                    url = GetRoute(typeToGet);
                }
                Console.WriteLine($"GET URL:{url} MessageType:{typeToGet.GetType().FullName}");
                var oDataRespone = await httpClient.GetFromJsonAsync<ODataResponse<object>>(new Uri(url, UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false); ;

                if (oDataRespone == null)
                    return Enumerable.Empty<object>();
                else
                {
                    ArgumentNullException.ThrowIfNull(oDataRespone);
                    var odataValue = oDataRespone.Value;
                    ArgumentNullException.ThrowIfNull(odataValue);
                    return odataValue;
                }
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"JsonException in GetAllAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetAllAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in GetAllAsync Request {ex.ToString()}");
                throw;
            }
        }

        public static object GetKey<T>(this T item, ILogger logger)
        {
            try
            {
                var keyProp = typeof(T).GetProperties()
                    .FirstOrDefault(
                        p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                            p.GetCustomAttribute<KeyAttribute>() != null);
                ArgumentNullException.ThrowIfNull(keyProp);
                ArgumentNullException.ThrowIfNull(item);
                var propValue = keyProp.GetValue(item);
                ArgumentNullException.ThrowIfNull(propValue);
                return propValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex} Error in GetKey Request {ex.ToString()}");

                logger.LogError(ex, $"Error in GetAllAsync Request {ex.ToString()}");
                throw;
            }

        }

        public static async Task<(string message, ApplicationUser? user)> GetMyUserAsync(this HttpClient httpClient, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling GetMyUserAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("api/Authentication/UserInfo", UriKind.Relative)))
                {

                    var response = await httpClient.SendAsync(request, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                    return response.IsSuccessStatusCode
                        ? ("Success", await response.Content.ReadFromJsonAsync<ApplicationUser>(cancellationToken: linkedCts.Token).ConfigureAwait(false))
                        : response.StatusCode == HttpStatusCode.Unauthorized
                            ? ("Unauthorized", null)
                            : ("Failed", null);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetMyUserAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex.ToString()} Error in GetMyUserAsync Request");
                throw;
            }
        }
        public static string GetRoute<T>() => $"api/odata/{typeof(T).Name}";
        public static string GetRoute(this Type typeToGet) => $"api/odata/{typeToGet.Name}";

        public static async Task<ApplicationUser> GetUserInfoAsync(this HttpClient httpClient, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling GetUserInfoAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

                var response = await httpClient.GetAsync(new Uri("api/Authentication/UserInfo", UriKind.Relative), cancellationToken: linkedCts.Token).ConfigureAwait(false);

                _ = response.EnsureSuccessStatusCode();

                var userModel = await response.Content.ReadFromJsonAsync<ApplicationUser>(cancellationToken: linkedCts.Token).ConfigureAwait(false);
                ArgumentNullException.ThrowIfNull(userModel);
                return userModel;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetUserInfoAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex.ToString()} Error in GetUserInfoAsync Request ");
                throw;
            }
        }
        public static async Task<HttpResponseMessage> LoginAsync(this HttpClient httpClient, string? UserName, string? password, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling LoginAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

                string Password = string.IsNullOrWhiteSpace(password) ? string.Empty : password;

                var myAnonymousObject = new { UserName, Password };
                var json = JsonSerializer.Serialize(myAnonymousObject, jsonSerializerOptions);


                var response = await httpClient.PostAsJsonAsync(new Uri("api/Authentication/LoginAsync", UriKind.Relative), new { UserName, Password }, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response;

            }
            catch (HttpRequestException ex)
            {
                logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in LoginAsync Request {ex.ToString()}");
                Console.WriteLine($"Error in LoginAsync Request {ex.ToString()}");
                throw;
            }

        }


        public static async Task<bool> LogoutAsync(this HttpClient httpClient, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling LogoutAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

                var response = await httpClient.PostAsync(new Uri("api/Authentication/LogoutAsync", UriKind.Relative), null, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Response null or not Successful but send");
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"JsonException in GetAllAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in LogoutAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex.ToString()} Error in LogoutAsync Request ");
                throw;
            }
        }

        public static async Task<bool> PatchAsync<T>(this HttpClient httpClient, T item, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(item);
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling PatchAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var key = GetKey(item, logger);
                if (key is null) return false;

                var url = $"{GetRoute<T>()}({key})";
                var jsonstring = item.ToJsonString(jsonSerializerOptions);
                Console.WriteLine($"PATCH URL:{url} MessageType:{item.GetType().FullName}  Object:{jsonstring}");
                var response = await httpClient.PatchAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (JsonException ex)
            {

                logger.LogError(ex, $"JsonException in PatchAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in PatchAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in PutAsync Request {ex.ToString()}");
                throw;
            }

        }
        public static async Task<bool> PatchPartialAsync<TMain, TPartial>(this HttpClient httpClient, TPartial tPartial, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(tPartial);
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling PatchPartialAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var key = GetKeyFromMainType<TMain, TPartial>(tPartial, logger);
                if (key is null) return false;

                var url = $"{GetRoute<TMain>()}({key})";
                var jsonString = tPartial.ToJsonString(jsonSerializerOptions);
                Console.WriteLine($"PATCHPARTIAL URL:{url} Object:{jsonString}");
                var response = await httpClient.PatchAsJsonAsync<TPartial>(new Uri(url, UriKind.Relative), tPartial, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (JsonException ex)
            {

                logger.LogError(ex, $"JsonException in PatchPartialAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in PatchPartialAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in PutAsync Request {ex.ToString()}");
                throw;
            }

        }


        public static async Task<string> PostAsync<TMain, T>(this HttpClient httpClient, T item, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(item);
                ArgumentNullException.ThrowIfNull(httpClient);
                ArgumentNullException.ThrowIfNull(httpClient.BaseAddress);
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var json = JsonSerializer.Serialize(item, jsonSerializerOptions);
                string url;
                url = $"{GetRoute<TMain>()}";

                Console.WriteLine($"POST URL:{url} MessageType:{item.GetType().FullName} Object:{item.ToJsonString(jsonSerializerOptions)}");
                var response = await httpClient.PostAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                ArgumentNullException.ThrowIfNull(response);
                _ = response.EnsureSuccessStatusCode();
                var responseProcess = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync(cancellationToken: linkedCts.Token).ConfigureAwait(false)
                    : default;
                ArgumentNullException.ThrowIfNullOrWhiteSpace(responseProcess);
                return responseProcess;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"JsonException in PostAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in PostAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in PostAsync Request {ex.ToString()}");
                throw;
            }

        }

        public static async Task<bool> PutAsync<T>(this HttpClient httpClient, T item, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(httpClient);
                ArgumentNullException.ThrowIfNull(item);
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var key = GetKey(item, logger);
                if (key is null) return false;

                var url = $"{GetRoute<T>()}({key})";
                var jsonstring = item.ToJsonString(jsonSerializerOptions);
                Console.WriteLine($"PUT URL:{url} MessageType:{item.GetType().FullName} Object:{jsonstring}");
                var response = await httpClient.PutAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, $"JsonException in PutAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} Error in PutAsync Request {ex.ToString()}");
                throw;
            }

        }

        public static async Task<bool> PutRefAsync<T, TRef>(this HttpClient httpClient, T mainType, ILogger logger, CancellationToken? externalToken = null)
        {
            try
            {
                if (httpClient.BaseAddress == null) throw new ArgumentNullException(nameof(httpClient.BaseAddress), "BaseAddress of HttpClient is null. Please set it before calling PutRefAsync.");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
                var key = GetKey(mainType, logger);

                if (key is null) return false;

                StringBuilder stringBuilderRefStringContent = new();
                StringBuilder stringBuilderUrl = new();
                ArgumentNullException.ThrowIfNull(mainType);
                var propertyInfos = mainType.GetType().GetProperties();
                foreach (var collectionType in propertyInfos.Where(x => x.PropertyType != typeof(string) && x.PropertyType.GetInterfaces()
            .Any(i => i.IsGenericType &&
                      i.GetGenericTypeDefinition() == typeof(IEnumerable<>) &&
                      i.GetGenericArguments()[0] == typeof(TRef))))
                {
                    var collection = collectionType.GetValue(mainType);
                    if (collection != null && collection is IEnumerable<TRef> collectionOfTRefType)
                    {
                        foreach (var item in collectionOfTRefType)
                        {
                            try
                            {
                                var refKey = GetKey(item, logger);
                                _ = stringBuilderRefStringContent.Clear();
                                _ = stringBuilderRefStringContent.Append(httpClient.BaseAddress);
                                _ = stringBuilderRefStringContent.Append($"{GetRoute<TRef>()}({refKey})");
                                string updatestring = stringBuilderRefStringContent.ToString();
                                OdataID odataId = new() { ID = updatestring };
                                _ = stringBuilderUrl.Clear();

                                _ = stringBuilderUrl.Append($"{GetRoute<T>()}({key})/{collectionType.Name}/$ref");

                                var response = await httpClient.PutAsJsonAsync(
                                            new Uri(stringBuilderUrl.ToString(), UriKind.Relative),
                                            odataId).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex} Error in PutAsync Request for {item?.ToString()}");
                            }
                        }
                    }

                }

                return true;

            }
            catch (JsonException ex)
            {

                logger.LogError(ex, $"JsonException in PutRefAsync Request {ex.ToString()}");
                Console.WriteLine($"{ex} ❌ JSON error: {ex.Message}");
                Console.WriteLine($"{ex} 📍 Path: {ex.Path}");
                Console.WriteLine($"{ex} 📦 Line: {ex.LineNumber}");
                if (ex.InnerException != null)
                    Console.WriteLine($"{ex} Inner Exception: {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in PutRefAsync Request {ex.ToString()}");
                Console.WriteLine($"Error in PutAsync Request {ex.ToString()}");
                throw;
            }
        }
    }
}
