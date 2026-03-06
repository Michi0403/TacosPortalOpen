//-----------------------------------------------------------------------
// <copyright file="RawWebPushSender.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace TacosPortal.Services
{

    public sealed class RawWebPushSender
    {
        private readonly ECDsa _ecdsa;
        private readonly IHttpClientFactory _http;
        private readonly string _subject;
        private readonly string _vapidPublicKeyB64Url;

        public RawWebPushSender(IHttpClientFactory http, IConfiguration cfg)
        {
            _http = http;
            _vapidPublicKeyB64Url = cfg["Vapid:PublicKey"]!;
            _subject = cfg["Vapid:Subject"] ?? "mailto:admin@example.com";

            var pkcs8 = Convert.FromBase64String(cfg["Vapid:PrivateKey"]!);
            _ecdsa = ECDsa.Create();
            _ecdsa.ImportPkcs8PrivateKey(pkcs8, out _);
        }

        public async Task SendAsync(string endpoint, int ttlSeconds = 60)
        {
            var aud = new Uri(endpoint).GetLeftPart(UriPartial.Authority);
            var now = DateTimeOffset.UtcNow;
            var exp = now.AddHours(12).ToUnixTimeSeconds();

            var creds = new SigningCredentials(new ECDsaSecurityKey(_ecdsa), SecurityAlgorithms.EcdsaSha256);

            var payload = new JwtPayload
            {
                { "sub", _subject },
                { "aud", aud },
                { "exp", exp }
            };

            var header = new JwtHeader(creds);

            var token = new JwtSecurityToken(header, payload);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            using var req = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new ByteArrayContent(Array.Empty<byte>())
            };
            req.Headers.TryAddWithoutValidation("TTL", ttlSeconds.ToString());
            req.Headers.TryAddWithoutValidation("Authorization", $"WebPush {jwt}");
            req.Headers.TryAddWithoutValidation("Crypto-Key", $"p256ecdsa={_vapidPublicKeyB64Url}");
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            req.Content.Headers.ContentLength = 0;

            var client = _http.CreateClient("WebPush");
            var resp = await client.SendAsync(req);
            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Push failed {resp.StatusCode}: {body}");
            }
        }
    }
}
