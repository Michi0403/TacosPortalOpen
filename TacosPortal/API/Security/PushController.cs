//-----------------------------------------------------------------------
// <copyright file="PushController.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosPortal.Services;

namespace TacosPortal.API.Security
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PushController(
        ISecurityProvider securityProvider,
        INonSecuredObjectSpaceFactory osFactory,
        IConfiguration cfg,
        ILogger<PushController> log) : ControllerBase
    {

        [HttpGet("publickey")]
        [AllowAnonymous]
        public IActionResult PublicKey() => Ok(new { key = cfg["Vapid:PublicKey"] });

        [HttpDelete("subscriptions")]
        public IActionResult Remove([FromBody] SubscriptionDto dto)
        {
            try
            {
                var xafUser = (ApplicationUser)securityProvider.GetSecurity().User;
                using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationPushSubscription>();
                var sub = os.FirstOrDefault<ApplicationPushSubscription>(
                    s => s.Endpoint == dto.endpoint && s.User.ID == xafUser.ID);
                if (sub != null) { os.Delete(sub); os.CommitChanges(); }
                return Ok();
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error in Remove: {ex.ToString()}");
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }

        }

        [HttpPost("subscriptions")]
        public IActionResult Save([FromBody] SubscriptionDto dto)
        {
            try
            {
                var xafUser = (ApplicationUser)securityProvider.GetSecurity().User;
                using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationPushSubscription>();

                var user = os.FirstOrDefault<ApplicationUser>(u => u.ID == xafUser.ID)
                           ?? throw new InvalidOperationException("User not found in OS");

                var existing = os.FirstOrDefault<ApplicationPushSubscription>(
                    s => s.Endpoint == dto.endpoint && s.User.ID == user.ID);

                if (existing is null)
                {
                    var sub = os.CreateObject<ApplicationPushSubscription>();
                    sub.User = user;
                    sub.Endpoint = dto.endpoint;
                    sub.P256dh = dto.keys.p256dh;
                    sub.Auth = dto.keys.auth;
                }
                else
                {
                    existing.P256dh = dto.keys.p256dh;
                    existing.Auth = dto.keys.auth;
                    existing.UpdatedUtc = DateTime.UtcNow;
                }

                os.CommitChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error in Save: {ex.ToString()}");
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }

        }

        [HttpPost("test")]
        public async Task<IActionResult> Test([FromServices] RawWebPushSender sender)
        {
            try
            {
                var xafUser = (ApplicationUser)securityProvider.GetSecurity().User;
                using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationPushSubscription>();
                var subs = os.GetObjectsQuery<ApplicationPushSubscription>()
                    .Where(s => s.User.ID == xafUser.ID).ToList();
                foreach (var s in subs)
                    await sender.SendAsync(s.Endpoint);
                return Ok(new { sent = subs.Count });
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Error in Test: {ex.ToString()}");
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }

        }

        public record KeysDto(string p256dh, string auth);
        public record SubscriptionDto(string endpoint, KeysDto keys);
    }
}
