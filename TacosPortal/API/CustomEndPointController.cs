//-----------------------------------------------------------------------
// <copyright file="CustomEndPointController.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosPortal.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomEndPointController(ISecurityProvider securityProvider, IObjectSpaceFactory securedObjectSpaceFactory,
            IStandardAuthenticationService securityAuthenticationService, ILogger<CustomEndPointController> logger) : ControllerBase
    {

        [HttpGet(nameof(CanCreate))]
        [Produces("application/json")]
        public IActionResult CanCreate(string typeName)
        {
            try
            {
                var strategy = (SecurityStrategy)securityProvider.GetSecurity();
                var objectType = strategy.TypesInfo.PersistentTypes.First(info => info.Name == typeName).Type;
                return Ok(strategy.CanCreate(objectType));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in CanCreate {ex.ToString()}");
                return BadRequest("Can Create failed internally caller not valid or not authenticated?");
            }

        }
        [HttpPost(nameof(ChangePassword))]
        [Authorize(Roles = "Administrators")]
        [SwaggerOperation(
      "Sets the StoredInPassword Password as Password",
      "Refer to the following help topic for more information on authentication methods in the XAF Security System: <a href='https://docs.devexpress.com/eXpressAppFramework/119064/data-security-and-safety/security-system/authentication'>Authentication</a>.")]
        public async Task<IActionResult> ChangePassword([ FromBody ][ SwaggerRequestBody(
                                                                           @"For example: <br /> { ""userName"": ""Admin"", ""password"": """" }" ) ]
        ApplicationUser userToChange)
        {
            try
            {
                var xafUser = (ApplicationUser)securityProvider.GetSecurity().User;
                if (xafUser.IsUserInRole("Administrators"))
                {

                    using (var objectSpace =
                                            securedObjectSpaceFactory.CreateObjectSpace<ApplicationUser>())
                    {
                        var changeUser =
                                                    objectSpace.FirstOrDefault<ApplicationUser>(
                            x
                                                                                                            => x.ID ==
                                userToChange.ID);
                        changeUser.SetPassword(userToChange.StoredPassword);
                        objectSpace.CommitChanges();
                    }

                    return Ok();
                }
                return NoContent();
            }
            catch (AuthenticationException ex)
            {
                logger.LogError(ex, $"AuthenticationError in ChangePassword {ex.ToString()}");
                return Unauthorized("User name or password is incorrect or no Administrator.");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Exception in ChangePassword {ex.ToString()}");
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }

    }
}
