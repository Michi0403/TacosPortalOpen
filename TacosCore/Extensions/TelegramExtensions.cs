//-----------------------------------------------------------------------
// <copyright file="TelegramExtensions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Runtime.CompilerServices;
using System.Text.Json;
using Telegram.Bot;



#if NET6_0_OR_GREATER
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;

#endif

#pragma warning disable IDE0130, MA0003

namespace TacosCore.Extensions
{
    internal static class ObjectExtensions
    {
#if NETCOREAPP3_1_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] string? parameterName = default)
            => value ?? throw new ArgumentNullException(parameterName);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>(this T? value) => value ?? throw new ArgumentNullException(null);
#endif
    }
}

namespace Microsoft.Extensions.DependencyInjection
{

    public static class TelegramBotConfigureExtensions
    {



        public static IServiceCollection ConfigureTelegramBot<TOptions>(this IServiceCollection services, Func<TOptions, JsonSerializerOptions> opt)
            where TOptions : class
            => services.Configure<TOptions>(options => JsonBotAPI.Configure(opt(options)));
    }
}

#if NET6_0_OR_GREATER
namespace Telegram.Bot
{

    public static class AuthHelpers
    {

        private static SortedDictionary<string, string> ParseValidateData(SortedDictionary<string, string> fields, string botToken, bool loginWidget = false)
        {
            if (fields.Remove("hash", out var hash))
            {
                var dataCheckString = string.Join('\n', fields.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                var secretKey = loginWidget
                    ? SHA256.HashData(Encoding.ASCII.GetBytes(botToken))
                    : HMACSHA256.HashData(Encoding.ASCII.GetBytes("TelegramWebAppData"), Encoding.ASCII.GetBytes(botToken));
                var computedHash = HMACSHA256.HashData(secretKey, Encoding.UTF8.GetBytes(dataCheckString));
                if (computedHash.SequenceEqual(Convert.FromHexString(hash)))
                    return fields;
            }
            throw new SecurityException("Invalid data hash");
        }

#pragma warning disable MA0002, MA0006






        public static SortedDictionary<string, string> ParseValidateData(string? initData, string botToken, bool loginWidget = false)
        {
            var query = HttpUtility.ParseQueryString(initData ?? string.Empty);
            return ParseValidateData(query.AllKeys.ToDictionary(key => key!, key => query[key]!), botToken, loginWidget);
        }







        public static SortedDictionary<string, string> ParseValidateData(IDictionary<string, string> fields, string botToken, bool loginWidget = false)
            => ParseValidateData(new SortedDictionary<string, string>(fields), botToken, loginWidget);
    }
#pragma warning restore MA0002 
}
#endif
