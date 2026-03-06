//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using DevExpress.ExpressApp.WebApi.Services;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;
using TacosCore.Helper;
using TacosPortal.Api.Security;
using TacosPortal.BusinessObjects;
using TacosPortal.Components;
using TacosPortal.Hubs;
using TacosPortal.Interfaces;
using TacosPortal.Services;
using TacosPortal.Services.Telegram;
using TacosPortalWebassemblyClient.Services;
using Telegram.Bot;
using ConfigurationRoot = TacosCore.BusinessObjects.ConfigurationRoot;
using CookieHandlerWASM = TacosPortalWebassemblyClient.Services.CookieHandlerWASM;

namespace TacosPortal
{
    public class Startup(IConfiguration configuration)
    {

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger(c => c.RouteTemplate = "totallynotswagger/{documentName}/swagger.json");
                _ = app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/totallynotswagger/v1/swagger.json", "TacosPortal WebApi v1");
                        c.RoutePrefix = "totallynotswagger";
                    });
            }
            else
            {
                _ = app.UseExceptionHandler("/Error");
                _ = app.UseSwagger(c => c.RouteTemplate = "totallynotswagger/{documentName}/swagger.json");
                _ = app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/totallynotswagger/v1/swagger.json", "TacosPortal WebApi v1");
                        c.RoutePrefix = "totallynotswagger";
                    });

            }
            _ = app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            _ = app.UseSession();
            _ = app.UseHsts();
            _ = app.UseHttpsRedirection();
            _ = app.UseRequestLocalization();

            _ = app.UseCertificateForwarding();

            _ = app.UseDefaultFiles();
            _ = app.UseStaticFiles();

            _ = app.UseRouting();

            _ = app.UseCors();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();
            _ = app.UseResponseCompression();
            _ = app.UseAntiforgery();
            _ = app.UseEndpoints(
                endpoints =>
                {
                    _ = endpoints.MapRazorComponents<App>()
                                .AddInteractiveServerRenderMode()
                                .AddInteractiveWebAssemblyRenderMode()
                                .AddAdditionalAssemblies(typeof(TacosPortalWebassemblyClient.Program).Assembly)

                        .AllowAnonymous();
                    _ = endpoints.MapXafEndpoints();
                    _ = endpoints.MapControllers();
                    _ = endpoints.MapHub<ChatHub>("/chathub");
                });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("=== CONFIG KEYS START ===");
            foreach (var kv in configuration.AsEnumerable())
            {
                Console.WriteLine($"{kv.Key} = {kv.Value}");
            }
            Console.WriteLine("=== CONFIG KEYS END ===");
            if (configuration is IConfigurationRoot root)
            {
                Console.WriteLine("=== CONFIG PROVIDERS START ===");
                foreach (var provider in root.Providers)
                {
                    Console.WriteLine(provider.ToString());
                }
                Console.WriteLine("=== CONFIG PROVIDERS END ===");
            }
            var configRoot = configuration
     .Get<ConfigurationRoot>();

            services.AddLogging(
                logging => LoggingHelper.ConfigureCustomLoggersWithConsoleAndDebug(
                    logging,
                    services,
                    configuration));
            services.ConfigureApplicationCookie(
                options =>
                {
                    options.Cookie.Name = ".AspNetCore.Cookies";
                    options.Cookie.SameSite = SameSiteMode.None;

                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.Cookie.HttpOnly = false;
                });
            services.AddCascadingAuthenticationState();
            services.Configure<CookieAuthenticationOptions>(
                CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.Cookie.Name = ".AspNetCore.Cookies";
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = false;
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.CookieManager = new ChunkingCookieManager();
                });
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<TacosPortalWebassemblyClient.Interfaces.INotificationService, WebassemblyClientNotificationService>(
                );
            services.Configure<CircuitOptions>(
                o =>
    o.DisconnectedCircuitRetentionPeriod = TimeSpan.FromSeconds(30));
            services.AddRazorPages();
            services.AddRazorComponents().AddInteractiveServerComponents().AddInteractiveWebAssemblyComponents();
            services.AddSession();
            services.AddTransient<LoggingHandler>();
            services.AddScoped<CookieBlazorServerHandler>();

            services.AddScoped<CustomRevalidatingAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
            services.AddScoped<ITacosApi, TacosPortalApiService>();
            services.AddScoped<WebAPIClient>();
            services.AddScoped<IWebAPI, WebAPIClient>();
            services.AddCascadingAuthenticationState();

            try
            {
                services.AddScoped<UpdateHandler>();
                services.AddScoped<ReceiverService>();
                services.AddHostedService<PollingService>();

                services.Configure<BotConfigurationCoreOptions>(
                    configuration.GetSection("BotConfigurationCore"));
                services.AddHttpClient("telegram_bot_client")
                    .AddTypedClient<ITelegramBotClient>(
                        (httpClient, sp) =>
                        {
                            ArgumentNullException.ThrowIfNull(configRoot);
                            var botConfig = configRoot.BotConfigurationCore;
                            ArgumentNullException.ThrowIfNull(botConfig);
                            var options = new TelegramBotClientOptions(botConfig.BotToken);
                            return new TelegramBotClient(options, httpClient);
                        });
                services.AddScoped<TelegramWorkerService>();
                services.AddHostedService<ScopedServiceRunnerHostedService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Startup.cs setting up TelegramBot:{ex.ToString()}");
            }
            services.AddDevExpressBlazor(
                options =>
                {
                    options.BootstrapVersion = BootstrapVersion.v5;
                    options.SizeMode = SizeMode.Medium;
                });
            services.AddScoped<IClaimsTransformation, XafRoleClaimsTransformation>();
            var cookieContainer = new CookieContainer();
            services.AddScoped<CookieContainer>(sp => cookieContainer);
            services.AddHttpClient("WebPush", c =>
            {
                c.DefaultRequestVersion = new Version(2, 0);
                c.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
            });
            services.AddSingleton<RawWebPushSender>();

            services.AddHttpClient("API")
                .ConfigureHttpClient((sp, client) =>
                {
                    var env = sp.GetRequiredService<IWebHostEnvironment>();
                    ArgumentNullException.ThrowIfNull(configRoot);
                    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
                    var baseUrl = $"http://localhost:{port}";
                    var normalizedBase = baseUrl.Replace("0.0.0.0", "localhost").TrimEnd('/');
                    client.BaseAddress = new Uri(normalizedBase, UriKind.Absolute);
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    client.Timeout = TimeSpan.FromSeconds(30);
                })
                .ConfigurePrimaryHttpMessageHandler(
                    () =>
                    {
                        var handler = new HttpClientHandler
                        {
                            AutomaticDecompression = DecompressionMethods.All,
                            UseProxy = false,
                            UseCookies = true,
                            UseDefaultCredentials = true,
                            CookieContainer = cookieContainer,
                            PreAuthenticate = true
                        };
                        return handler;
                    })
                .AddHttpMessageHandler<CookieBlazorServerHandler>()
                .AddHttpMessageHandler<LoggingHandler>();
            services.AddScoped(
                (sp) =>
                {
                    var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("API");
                    return client;
                });

            services.AddOptions();
            services.AddDbContextFactory<TacoContext>(
                (serviceProvider, options) =>
                {
                    var connectionString = configuration.GetConnectionString("ConnectionString");
                    options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseChangeTrackingProxies();
                    options.UseChangeTrackingProxies();
                    options.UseObjectSpaceLinkProxies();
                    options.UseXafServiceProviderContainer(serviceProvider);
                    options.UseLazyLoadingProxies();
                    options.UseSecurity(serviceProvider);

                    var auditInterceptor = serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>();
                    var efCommandDiagnosticsInterceptor = serviceProvider.GetRequiredService<EfCommandDiagnosticsInterceptor>();
                    options.AddInterceptors(auditInterceptor, efCommandDiagnosticsInterceptor);
                },
                ServiceLifetime.Scoped);
            services.AddScoped<CookieHandlerWASM>();

            services.AddHttpClient("WasmClient").ConfigureHttpClient((sp, client) =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                ArgumentNullException.ThrowIfNull(configRoot);

                var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
                var baseUrl = $"http://localhost:{port}";
                var normalizedBase = baseUrl.Replace("0.0.0.0", "localhost").TrimEnd('/');
                client.BaseAddress = new Uri(normalizedBase, UriKind.Absolute);
                client.DefaultRequestHeaders.ExpectContinue = false;
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
                .AddHttpMessageHandler<CookieHandlerWASM>();

            services.AddScoped(
                sp =>
                {
                    var factory = sp.GetRequiredService<IHttpClientFactory>();
                    var client = factory.CreateClient("WasmClient");
                    client.Timeout = TimeSpan.FromSeconds(120);
                    client.DefaultRequestHeaders.ExpectContinue = false;
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, X-Requested-With, Content-MessageType, Accept");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-MessageType", "application/json; odata.metadata=full");
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                    return client;
                });
            services.AddXafAspNetCoreSecurity(
                configuration,
                options =>
                {
                    options.RoleType = typeof(TacoPermissionPolicyRole);
                    options.UserType = typeof(TacoPermissionPolicyUser);
                    options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);

                    options.SupportNavigationPermissionsForTypes = false;

                })
                .AddAuthenticationStandard(options => options.IsSupportChangePassword = true);
            services.AddMvc();
            services.AddSingleton<WeatherForecastService>();
            services.AddSignalR()
                 .AddMessagePackProtocol(options =>
                 {
                     options.SerializerOptions = MessagePackSerializerOptions.Standard
                         .WithResolver(ContractlessStandardResolver.Instance)
                         .WithSecurity(MessagePackSecurity.UntrustedData);
                 })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                options.PayloadSerializerOptions.WriteIndented = true;
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                options.PayloadSerializerOptions.IgnoreReadOnlyFields = false;
                options.PayloadSerializerOptions.IgnoreReadOnlyProperties = false;
                options.PayloadSerializerOptions.IncludeFields = false;
                options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.PayloadSerializerOptions.AllowTrailingCommas = true;
                options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.PayloadSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;


            });
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpAuditContextService, HttpAuditContextService>();
            services.AddScoped<AuditSaveChangesInterceptor>();
            services.AddScoped<EfCommandDiagnosticsInterceptor>();
            services.AddSingleton<IAmbientUserContext, AmbientUserContext>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessorService>();

            services.AddScoped<ThemeService>();


            services.Configure<ConfigurationRoot>(configuration);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ConfigurationRoot>>().Value);
            services.AddScoped<IAuthenticationTokenProvider, JwtTokenProviderService>();
            services.AddXafWebApi(
                builder =>
                {
                    builder.ConfigureOptions(
                        options =>
                        {
                            options.BusinessObject<ApplicationUser>()
                                .ConfigureController(
                                    c =>
                                    {
                                        c.AllActions();
                                    });
                            options.BusinessObject<ApplicationPushSubscription>()
                                .ConfigureController(
                                    c =>
                                    {
                                        c.AllActions();
                                    });

                            options.BusinessObject<TacoPermissionPolicyRole>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<PermissionPolicyTypePermissionObject>()
                                .ConfigureController(c => c.AllActions());
                            options.BusinessObject<ApplicationUserLoginInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<PermissionPolicyActionPermissionObject>()
                                .ConfigureController(c => c.AllActions());
                            options.BusinessObject<PermissionPolicyMemberPermissionsObject>()
                                .ConfigureController(c => c.AllActions());
                            options.BusinessObject<PermissionPolicyNavigationPermissionObject>()
                                .ConfigureController(c => c.AllActions());
                            options.BusinessObject<PermissionPolicyObjectPermissionsObject>()
                                .ConfigureController(c => c.AllActions());

                            options.BusinessObject<DatabaseLog>().ConfigureController(c => c.AllActions());

                            options.ConfigureBusinessObjectActionEndpoints(opt => opt.EnableActionEndpoints = true);
                            options.BusinessObject<TacoTeam>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TacoTeamChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramAcceptedGiftTypes>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramAffiliateInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramAnimation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramApiResponse>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramAudio>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramAuthorizationRequestParameters>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundFillFreeformGradient>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundFillGradient>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundFillSolid>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundTypeChatTheme>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundTypeFill>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundTypePattern>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBackgroundTypeWallpaper>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBirthdate>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommand>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeDefault>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeAllPrivateChats>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeAllGroupChats>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeAllChatAdministrators>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeChatAdministrators>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotCommandScopeChatMember>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotDescription>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotName>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBotShortDescription>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessBotRights>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessMessagesDeleted>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessConnection>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessIntro>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessLocation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessOpeningHours>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramBusinessOpeningHoursInterval>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramCallbackGame>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramCallbackQuery>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatAdministratorRights>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatBackground>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatBotRightsUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatBoost>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatBoostRemoved>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatBoostUpdated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatFullInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatId>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatInviteLink>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatJoinRequest>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatLocation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberOwner>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberAdministrator>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberMember>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberRestricted>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberLeft>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberBanned>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatMemberUpdated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatPermissions>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChatShared>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramChosenInlineResult>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramContact>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramCopyTextButton>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramCredentials>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramDataCredentials>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramDice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramDocument>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramEncryptedCredentials>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramEncryptedPassportElement>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramExternalReplyInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramFileCredentials>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramForceReplyMarkup>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramForumTopic>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramForumTopicCreated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramForumTopicEdited>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGame>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGameHighScore>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGift>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiftInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiftList>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiveaway>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiveawayCompleted>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiveawayCreated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramGiveawayWinners>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineKeyboardButton>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineKeyboardMarkup>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineKeyboardRow>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultArticle>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultGif>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultMpeg4Gif>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultAudio>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultVoice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultDocument>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultLocation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultVenue>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultContact>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultGame>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedGif>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedMpeg4Gif>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedSticker>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedDocument>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedVoice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQueryResultCachedAudio>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInlineQuery>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputFileId>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputFileStream>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputFileUrl>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputMediaAnimation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputMediaAudio>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputMediaDocument>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputMediaPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputMediaVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputTextMessageContent>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputLocationMessageContent>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputVenueMessageContent>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputContactMessageContent>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputInvoiceMessageContent>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputPaidMediaPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputPaidMediaVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputPollOption>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputProfilePhotoStatic>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputProfilePhotoAnimated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputSticker>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputStoryContentPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInputStoryContentVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramInvoice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramKeyboardButtonPollType>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramKeyboardButtonRequestChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramKeyboardButtonRequestUsers>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramKeyboardButton>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramKeyboardRow>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramLabeledPrice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramLinkPreviewOptions>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramLocationAddress>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramLocation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramLoginUrl>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMaskPosition>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMenuButtonCommands>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMenuButtonDefault>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMenuButtonWebApp>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessage>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageAutoDeleteTimerChanged>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageEntity>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageId>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageOrigin>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageOriginHiddenUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageOriginUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageOriginChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageOriginChannel>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramMessageReactionUpdated>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramOrderInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMediaInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMediaPhoto>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMediaPreview>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMediaPurchased>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMediaVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPaidMessagePriceChanged>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportData>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorDataField>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorFrontSide>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorReverseSide>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorSelfie>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorFile>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorFiles>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorTranslationFile>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorTranslationFiles>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportElementErrorUnspecified>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportScope>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPersonalDetails>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportFile>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPhotoSize>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPoll>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPollAnswer>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPollOption>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPhotoSizeGroup>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPreCheckoutQuery>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPreparedInlineMessage>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramProximityAlertTriggered>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReactionCount>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReactionTypeEmoji>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReactionTypeCustomEmoji>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReactionTypePaid>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramRefundedPayment>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReplyKeyboardMarkup>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReplyKeyboardRemove>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramResponseParameters>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramReplyParameters>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramResidentialAddress>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramRevenueWithdrawalStatePending>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramRevenueWithdrawalStateSucceeded>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramRevenueWithdrawalStateFailed>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportScopeElementOneOfSeveral>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramPassportScopeElementOne>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSecureData>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSecureValue>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSentWebAppMessage>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSharedUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramShippingAddress>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramShippingOption>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramShippingQuery>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStarAmount>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStarTransaction>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStarTransactions>().ConfigureController(c => c.AllActions());

                            options.BusinessObject<TelegramSticker>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStickerSet>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStory>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaPosition>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryArea>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaTypeLink>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaTypeLocation>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaTypeSuggestedReaction>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaTypeUniqueGift>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramStoryAreaTypeWeather>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSuccessfulPayment>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramSwitchInlineQueryChosenChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTextQuote>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTGFile>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerAffiliateProgram>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerFragment>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerTelegramAds>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerTelegramApi>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramTransactionPartnerOther>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUniqueGiftBackdropColors>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUniqueGiftBackdrop>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUniqueGiftInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUniqueGiftModel>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUniqueGift>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUser>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUserChat>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUsersShared>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUserProfilePhotos>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramUserChatBoosts>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVenue>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideoChatEnded>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideoChatParticipantsInvited>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideoChatScheduled>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideoChatStarted>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideoNote>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVideo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramVoice>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramWebAppData>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramWebAppInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramWebhookInfo>().ConfigureController(c => c.AllActions());
                            options.BusinessObject<TelegramWriteAccessAllowed>().ConfigureController(c => c.AllActions());

                        });

                    builder.Modules
                        .Add<TacosPortalModule>();
                    builder.ObjectSpaceProviders
                        .AddSecuredEFCore(options =>
                        { options.PreFetchReferenceProperties(); })
                        .WithDbContext<TacoContext>(
                            (serviceProvider, options) =>
                            {
                                string? connectionString = null;
                                if (configuration.GetConnectionString("ConnectionString") != null)
                                {
                                    var connString = configuration.GetConnectionString("ConnectionString");
                                    ArgumentNullException.ThrowIfNullOrWhiteSpace(connString);
                                    connectionString = connString;
                                }

                                options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseChangeTrackingProxies();
                                options.UseChangeTrackingProxies();
                                options.UseObjectSpaceLinkProxies();

                                options.UseLazyLoadingProxies();

                                var auditInterceptor = serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>();

                                var efCommandDiagnosticsInterceptor = serviceProvider.GetRequiredService<EfCommandDiagnosticsInterceptor>();
                                options.AddInterceptors(auditInterceptor, efCommandDiagnosticsInterceptor);
                            })
                        .AddNonPersistent();

                    builder.Security
                        .UseIntegratedMode(
                            options =>
                            {
                                options.Lockout.Enabled = true;

                                options.RoleType = typeof(TacoPermissionPolicyRole);
                                options.UserType = typeof(ApplicationUser);
                                options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
                                options.SupportNavigationPermissionsForTypes = true;
                                options.Events.OnSecurityStrategyCreated += securityStrategy => ((SecurityStrategy)securityStrategy).PermissionsReloadMode =
                                    PermissionsReloadMode.CacheOnFirstAccess;
                            })
                        .AddPasswordAuthentication(
                            options =>
                            {
                                options.IsSupportChangePassword = true;
                                options.Events.OnCustomizeClaims = ctx =>
                                {
                                    var idClaim = ctx.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                                    if (!Guid.TryParse(idClaim, out var userId))
                                        return;

                                    var osFactory = ctx.ServiceProvider.GetRequiredService<INonSecuredObjectSpaceFactory>();
                                    using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationUser>();
                                    var user = os.FirstOrDefault<ApplicationUser>(u => u.ID == userId);
                                    if (user is null) return;

                                    var hasRole = new HashSet<string>(
                                        ctx.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value),
                                        StringComparer.Ordinal);

                                    foreach (var role in user.Roles)
                                    {
                                        if (!string.IsNullOrWhiteSpace(role?.Name) && hasRole.Add(role.Name))
                                            ctx.Claims.Add(new Claim(ClaimTypes.Role, role.Name));
                                    }
                                };

                                options.Events.OnCustomizeLoginToken = ctx =>
                                {
                                    var osFactory = ctx.ServiceProvider.GetRequiredService<INonSecuredObjectSpaceFactory>();
                                    using var os = osFactory.CreateNonSecuredObjectSpace<ApplicationUser>();
                                    var user = os.FirstOrDefault<ApplicationUser>(u => u.ID == new Guid(ctx.UserId));
                                    if (user is null) return;

                                    foreach (var role in user.Roles)
                                        ctx.Claims.Add(new Claim(ClaimTypes.Role, role.Name));
                                };
                            });

                    builder.AddBuildStep(
                        application =>
                        {
                            application.ApplicationName = "SetupApplication.TacosPortal";
                            application.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
#if DEBUG
                            services.AddDirectoryBrowser();
                            if (Debugger.IsAttached &&
                                (application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema))
                            {
                                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                                application.DatabaseVersionMismatch += (s, e) =>
                                {
                                    e.Updater.Update();
                                    e.Handled = true;
                                };
                            }
#endif
                        });
                },
                configuration);
            services.AddResponseCompression
                (opts =>
            {
                opts.EnableForHttps = true;




                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
        "application/octet-stream"
    });
            });



            var authentication = services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    "Cookies",
                    options =>
                    {
                        options.Cookie.Name = ".AspNetCore.Cookies";
                        options.Cookie.SameSite = SameSiteMode.None;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.LoginPath = "/login";
                        options.LogoutPath = "/logout";
                        options.Cookie.HttpOnly = false;
                    })
                .AddJwtBearer(
                    options =>
                    {
                        ArgumentNullException.ThrowIfNull(configRoot);
                        ArgumentNullException.ThrowIfNull(configRoot.AuthenticationCore);
                        ArgumentNullException.ThrowIfNull(configRoot.AuthenticationCore.JwtCore);
                        ArgumentNullException.ThrowIfNull(configRoot.AuthenticationCore.JwtCore.IssuerSigningKey);
                        options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = configRoot.AuthenticationCore.JwtCore.Issuer,
            ValidAudience = configRoot.AuthenticationCore.JwtCore.Audience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey =
                    new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configRoot.AuthenticationCore.JwtCore.IssuerSigningKey))
        };
                    })
                .AddApplicationCookie();
            services.AddAuthorization(
                options =>
                {
                    options.DefaultPolicy =
                        new AuthorizationPolicyBuilder(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser()
                            .RequireXafAuthentication()
                            .Build();
                    options.AddPolicy("AdminOnly", p => p.RequireRole("Administrators"));
                    options.AddPolicy("AdminAndTGAdminOnly", p => { p.RequireRole("TelegramAdmins", "Administrators"); });
                });

            services.Configure<IdentityOptions>(o =>
            {
                o.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
                o.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                o.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                o.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
            });
            services
                .AddControllers(mvc =>
                {
                })
                .AddOData(
                    (options, serviceProvider) =>
                    {
                        var edm = new EdmModelBuilder(serviceProvider).GetEdmModel();
                        options
                            .AddRouteComponents("api/odata", edm, odataServices =>
                            {
                            })
                            .Select()
                            .Filter()
                            .Expand()
                            .OrderBy()
                            .Count()
                            .SetMaxTop(null)

                            .EnableQueryFeatures(null);
                    });



            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TacosPortal WebApi v1",
                    Version = "v1",
                    Description = @"Use AddXafWebApi(options) in the TacosPortal.Blazor.Server\Startup.cs file to make Business Objects available in the Web API."
                });

                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            Array.Empty<string>()
        }
    });
            });
            services.AddCors(
                options =>
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins(
                            "https://legendarymichi.eu",
                            "https://localhost:5556",
                            "https://localhost:5555",
                            "http://localhost:5055",
                            "https://localhost:5001",
                            "https://localhost:5003",
                            "http://localhost:5002",
                            "http://localhost:5000",
                            "https://localhost",
                            "https://127.0.0.1",
                            "https://tacosportal.big-dick.eu",
                            "https://big-dick.eu",
                            "https://localhost:44319")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }));
            services.Configure<JsonOptions>(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.IgnoreReadOnlyFields = false;
                    options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    options.JsonSerializerOptions.IncludeFields = false;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
                });
            services.Configure<ForwardedHeadersOptions>(
                options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });
        }
    }
}
