//-----------------------------------------------------------------------
// <copyright file="WebAPIClient.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.Extensions;

namespace TacosPortal.Services;
public interface IWebAPI : IDisposable
{

    Task<bool> DeleteAsync<T>(T item, CancellationToken? externalToken = null);

    Task<bool> DeleteRefAsync<TMain, TRef>(TMain mainType, TRef objectToDelete, CancellationToken? externalToken = null);

    Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken? externalToken = null, string? additionalQuery = null);
    Task<IEnumerable<object>> GetAllAsync(
        Type typeToGet,
        CancellationToken? externalToken = null,
        string? additionalQuery = null);
    Task<IEnumerable<T>> GetAllRefByQueryAsync<T, TRef>(string additionalQuery, CancellationToken? externalToken = null);
    Task<T> GetEntityByPathAsync<T>(string relativePath, CancellationToken? externalToken = null);
    Task<(string message, ApplicationUser? user)> GetMyUserAsync(CancellationToken? externalToken = null);
    Task<ApplicationUser> GetUserInfoAsync(CancellationToken? externalToken = null);
    Task<HttpResponseMessage> LoginAsync(string userName, string password, CancellationToken? externalToken = null);
    Task<bool> LogoutAsync(CancellationToken? externalToken = null);

    Task<bool> PatchAsync<T>(T item, CancellationToken? externalToken = null);

    Task<bool> PatchPartialAsync<TMain, TPartial>(TPartial tPartial, CancellationToken? externalToken = null);

    Task<string> PostAsync<TMain, T>(T item, CancellationToken? externalToken = null);

    Task<bool> PostRefAsync<T, TRef>(TRef refType, CancellationToken? externalToken = null);

    Task<bool> PutAsync<T>(T item, CancellationToken? externalToken = null);

    Task<bool> PutRefAsync<T, TRef>(T mainType, CancellationToken? externalToken = null);
}
public class WebAPIClient : IWebAPI
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<WebAPIClient> _logger;
    private bool disposedValue;
    private JsonSerializerOptions jsonSerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        PropertyNamingPolicy = null,
        IgnoreReadOnlyFields = false,
        IgnoreReadOnlyProperties = false,
        IncludeFields = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        AllowTrailingCommas = true,
        Converters = {
        new JsonStringEnumConverter()
    },
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString

    };

    public WebAPIClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<WebAPIClient> logger)
    {
        _logger = logger;
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        _httpClient = httpClientFactory.CreateClient("API");


        _httpContextAccessor = httpContextAccessor;

        if (_httpClient.BaseAddress == null)
        {
            ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
            var request = httpContextAccessor.HttpContext?.Request;
            string baseAddress;
            baseAddress = $"{request?.Scheme}://{request?.Host.Value.TrimEnd('/')}/";
            _httpClient.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
            ArgumentNullException.ThrowIfNull(_httpClient.BaseAddress);
        }

    }

    private void AttachAuthCookie(HttpRequestMessage request)
    {
        try
        {
            var cookie = _httpContextAccessor.HttpContext?.Request?.Cookies[".AspNetCore.Cookies"];
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                request.Headers.Add("Cookie", $".AspNetCore.Cookies={cookie}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in AttachAuthCookie Request {ex.ToString()}");
            throw;
        }

    }

    private object GetKey<T>(T item)
    {
        try
        {
            var keyProp = typeof(T).GetProperties()
            .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                                 p.GetCustomAttribute<KeyAttribute>() != null);
            ArgumentNullException.ThrowIfNull(keyProp);
            ArgumentNullException.ThrowIfNull(item);
            var propValue = keyProp.GetValue(item);
            ArgumentNullException.ThrowIfNull(propValue);
            return propValue;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetKey Request {ex.ToString()}");
            throw;
        }

    }
    private object? GetKeyFromMainType<TMain, TSub>(TSub subItem)
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
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetKeyFromMainType for {ex.ToString()}");
            throw;
        }
    }
    private static string GetRoute<T>() => $"api/odata/{typeof(T).Name}";
    private static string GetRoute(Type typeToGet) => $"api/odata/{typeToGet.Name}";

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _httpClient?.Dispose();

            }



            disposedValue = true;
        }
    }

    public async Task<bool> DeleteAsync<T>(T item, CancellationToken? externalToken = null)
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var key = GetKey(item);
            if (key is null) return false;

            var url = $"{GetRoute<T>()}({key})";
            var response = await _httpClient.DeleteAsync(new Uri(url, UriKind.Relative), cancellationToken: linkedCts.Token).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteAsync Request {ex.ToString()}");
            throw;
        }

    }

    public async Task<bool> DeleteRefAsync<T, TRef>(T mainType, TRef objectToDelete, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(mainType);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var mainKey = GetKey(mainType);
            var refKey = GetKey(objectToDelete);

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

                var response = await _httpClient.DeleteAsync(new Uri(url, UriKind.Relative), linkedCts.Token).ConfigureAwait(false);

                _ = response.EnsureSuccessStatusCode();
                return true;
            }

            return false;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteRefAsync Request {ex.ToString()}");
            throw;
        }
    }
    public void Dispose()
    {

        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken? externalToken = null, string? additionalQuery = null)
    {
        try
        {
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

            var oDataRespone = await _httpClient.GetFromJsonAsync<ODataResponse<T>>(new Uri(url, UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);

            if (oDataRespone == null)
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
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetAllAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<IEnumerable<object>> GetAllAsync(Type typeToGet, CancellationToken? externalToken = null, string? additionalQuery = null)
    {
        try
        {
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
            var oDataRespone = await _httpClient.GetFromJsonAsync<ODataResponse<object>>(new Uri(url, UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);

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
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetAllAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<IEnumerable<T>> GetAllRefByQueryAsync<T, TRef>(string query, CancellationToken? externalToken = null)
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            string url;
            url = query.StartsWith("api/odata/", StringComparison.OrdinalIgnoreCase)
                ? query
                : $"{GetRoute<T>()}?{query}";
            var oDataRespone = await _httpClient.GetFromJsonAsync<ODataResponse<T>>(new Uri(url, UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);

            if (oDataRespone == null)
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
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<T> GetEntityByPathAsync<T>(string relativePath, CancellationToken? externalToken = null)
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

            var result = await _httpClient.GetFromJsonAsync<T>(
                new Uri(relativePath, UriKind.Relative),
                options: jsonSerializerOptions,
                cancellationToken: linkedCts.Token
            ).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(result);
            return result;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in GetEntityByPathAsync HTTPRequestException: {ex}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetEntityByPathAsync: {ex}");
            throw;
        }
    }

    public async Task<(string message, ApplicationUser? user)> GetMyUserAsync(CancellationToken? externalToken = null)
    {
        try
        {

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("api/Authentication/UserInfo", UriKind.Relative)))
            {
                AttachAuthCookie(request);
                var response = await _httpClient.SendAsync(request, cancellationToken: linkedCts.Token).ConfigureAwait(false);
                _ = response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode
                    ? ("Success", await response.Content.ReadFromJsonAsync<ApplicationUser>(options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false))
                    : response.StatusCode == HttpStatusCode.Unauthorized
                        ? ("Unauthorized", null)
                        : ("Failed", null);
            }

        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, $"TaskCanceledException in GetMyUserAsync Request {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetMyUserAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<ApplicationUser> GetUserInfoAsync(CancellationToken? externalToken = null)
    {
        try
        {

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
#if DEBUG
            var testHttp = await _httpClient.GetAsync(new Uri("api/Authentication/UserInfo", UriKind.Relative), cancellationToken: linkedCts.Token).ConfigureAwait(false);
            if (testHttp != null && testHttp.IsSuccessStatusCode)
            {
                var stringi = await testHttp.Content.ReadAsStringAsync();
                if (stringi != null)
                {
                    _logger.LogInformation(stringi);
                    _logger.LogInformation(testHttp.ToString());

                }
            }

#endif
            var user = await _httpClient.GetFromJsonAsync<ApplicationUser>(new Uri("api/Authentication/UserInfo", UriKind.Relative), options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(user);
            return user;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GetUserInfoAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<HttpResponseMessage> LoginAsync(string userName, string password, CancellationToken? externalToken = null)
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);

            string Password = string.IsNullOrWhiteSpace(password) ? string.Empty : password;

            var myAnonymousObject = new { userName, Password };
            var json = JsonSerializer.Serialize(myAnonymousObject, options: jsonSerializerOptions);

            var response = await _httpClient.PostAsJsonAsync(new Uri("api/Authentication/LoginAsync", UriKind.Relative), new { userName, Password }, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            _ = response.EnsureSuccessStatusCode();
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in LoginAsync Request {ex.ToString()}");
            throw;
        }

    }


    public async Task<bool> LogoutAsync(CancellationToken? externalToken = null)
    {
        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var response = await _httpClient.PostAsync(new Uri("api/Authentication/LogoutAsync", UriKind.Relative), null, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            _ = response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in LogoutAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<bool> PatchAsync<T>(T item, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(item);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var key = GetKey(item);
            if (key is null) return false;

            var url = $"{GetRoute<T>()}({key})";
            var jsonstring = item.ToJsonString(jsonSerializerOptions);
            var response = await _httpClient.PatchAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }

    }
    public async Task<bool> PatchPartialAsync<TMain, TPartial>(TPartial tPartial, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(tPartial);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var key = GetKeyFromMainType<TMain, TPartial>(tPartial);
            if (key is null) return false;

            var url = $"{GetRoute<TMain>()}({key})";
            var jsonString = tPartial.ToJsonString(jsonSerializerOptions);
            var response = await _httpClient.PatchAsJsonAsync<TPartial>(new Uri(url, UriKind.Relative), tPartial, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }

    }
    public async Task<string> PostAsync<TMain, T>(T item, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(item);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var json = JsonSerializer.Serialize(item, jsonSerializerOptions);
            string url;
            url = $"{GetRoute<TMain>()}";

            _logger.LogInformation($"POST URL:{url} MessageType:{item.GetType().FullName} Object:{item.ToJsonString(jsonSerializerOptions)}");
            var response = await _httpClient.PostAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(response);
            var responseContent = await response.Content
                .ReadAsStringAsync(cancellationToken: linkedCts.Token)
                .ConfigureAwait(false);
            return responseContent;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PostAsync Request {ex.ToString()}");
            throw;
        }

    }

    public async Task<bool> PostRefAsync<T, TRef>(TRef refType, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(refType);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);


            StringBuilder stringBuilderRefStringContent = new();
            StringBuilder stringBuilderUrl = new();
            var propertyInfos = refType.GetType().GetProperties();
            foreach (var tRefType in propertyInfos.Where(x => x.PropertyType.GetInterfaces()
        .Any(i => !i.IsGenericType &&
                  i.GetGenericTypeDefinition() != typeof(IEnumerable<>) &&
                  i.GetType() == typeof(TRef))))
            {
                var singleEntry = tRefType.GetValue(refType);
                if (singleEntry != null && singleEntry is TRef singleEntryTypeOfTref)
                {
                    try
                    {
                        ArgumentNullException.ThrowIfNull(singleEntryTypeOfTref);
                        var refKey = GetKey(singleEntryTypeOfTref);
                        _ = stringBuilderRefStringContent.Clear();
                        _ = stringBuilderRefStringContent.Append(_httpClient.BaseAddress);
                        _ = stringBuilderRefStringContent.Append($"{GetRoute<TRef>()}({refKey})");
                        string updatestring = stringBuilderRefStringContent.ToString();
                        OdataID odataId = new() { ID = updatestring };
                        _ = stringBuilderUrl.Clear();

                        _ = stringBuilderUrl.Append($"{GetRoute<T>()}/{tRefType.Name}/$ref");


                        var response = await _httpClient.PutAsJsonAsync(
                                    new Uri(stringBuilderUrl.ToString(), UriKind.Relative),
                                    odataId).ConfigureAwait(false);
                        _ = response.EnsureSuccessStatusCode();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error in PutAsync Request while iterating through collectionOfTRefType");
                    }

                }

            }

            return true;

        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }
    }

    public async Task<bool> PutAsync<T>(T item, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(item);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var key = GetKey(item);
            if (key is null) return false;

            var url = $"{GetRoute<T>()}({key})";
            var jsonstring = item.ToJsonString(jsonSerializerOptions);
            var response = await _httpClient.PutAsJsonAsync<T>(new Uri(url, UriKind.Relative), item, options: jsonSerializerOptions, cancellationToken: linkedCts.Token).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }

    }

    public async Task<bool> PutRefAsync<T, TRef>(T mainType, CancellationToken? externalToken = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(mainType);
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, externalToken ?? CancellationToken.None);
            var key = GetKey(mainType);

            if (key is null) return false;

            StringBuilder stringBuilderRefStringContent = new();
            StringBuilder stringBuilderUrl = new();
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
                            ArgumentNullException.ThrowIfNull(item);
                            var refKey = GetKey(item);
                            _ = stringBuilderRefStringContent.Clear();
                            _ = stringBuilderRefStringContent.Append(_httpClient.BaseAddress);
                            _ = stringBuilderRefStringContent.Append($"{GetRoute<TRef>()}({refKey})");
                            string updatestring = stringBuilderRefStringContent.ToString();
                            OdataID odataId = new() { ID = updatestring };
                            _ = stringBuilderUrl.Clear();

                            _ = stringBuilderUrl.Append($"{GetRoute<T>()}({key})/{collectionType.Name}/$ref");


                            var response = await _httpClient.PutAsJsonAsync(
                                        new Uri(stringBuilderUrl.ToString(), UriKind.Relative),
                                        odataId).ConfigureAwait(false);
                            _ = response.EnsureSuccessStatusCode();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error in PutAsync Request while iterating through collectionOfTRefType");
                        }
                    }
                }

            }

            return true;

        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"❌ JSON error: {ex.Message}");
            _logger.LogError(ex, $"📍 Path: {ex.Path}");
            _logger.LogError(ex, $"📦 Line: {ex.LineNumber}");
            if (ex.InnerException != null)
                _logger.LogError(ex, $"Inner Exception: {ex.InnerException.ToString()}");
            throw;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, $"Error in LoadAsync HTTPRequestexception: {ex.ToString()}");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PutAsync Request {ex.ToString()}");
            throw;
        }
    }
}