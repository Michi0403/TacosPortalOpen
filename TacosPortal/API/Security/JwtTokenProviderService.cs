using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ExceptionServices;
using System.Text;

namespace TacosPortal.Api.Security
{
    public class JwtTokenProviderService(SignInManager signInManager, IConfiguration configuration, ILogger<JwtTokenProviderService> logger) : IAuthenticationTokenProvider
    {
        public string Authenticate(object logonParameters)
        {
            try
            {
                var result = signInManager.AuthenticateByLogonParameters(logonParameters);
                if (result.Succeeded)
                {
                    var configRoot = configuration.Get<TacosCore.BusinessObjects.ConfigurationRoot>();
                    ArgumentNullException.ThrowIfNull(configRoot);
                    ArgumentNullException.ThrowIfNull(configRoot.AuthenticationCore);
                    ArgumentNullException.ThrowIfNull(configRoot.AuthenticationCore.JwtCore);
                    ArgumentNullException.ThrowIfNullOrWhiteSpace(
                        configRoot.AuthenticationCore.JwtCore.IssuerSigningKey);
                    var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configRoot.AuthenticationCore.JwtCore.IssuerSigningKey));
                    var token = new JwtSecurityToken(
                        issuer: configRoot.AuthenticationCore.JwtCore.Issuer,
                        audience: configRoot.AuthenticationCore.JwtCore.Audience,
                        claims: result.Principal.Claims,
                        expires: DateTime.Now.AddDays(2),
                        signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                if (result.Error is DevExpress.ExpressApp.IUserFriendlyException)
                {
                    ExceptionDispatchInfo.Throw(result.Error);
                }
                throw new AuthenticationException("TelegramUser name or password is incorrect.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in JwtTokenProviderService");
                return string.Empty;
            }

        }
    }

}

