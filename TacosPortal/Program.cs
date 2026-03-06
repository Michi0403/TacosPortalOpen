//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.AspNetCore.DesignTime;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using System.Net;
using System.Reflection;
using TacosCore.Helper;

namespace TacosPortal;

public class Program : IDesignTimeApplicationFactory
{





    XafApplication IDesignTimeApplicationFactory.Create()
    {
        IHostBuilder hostBuilder = CreateHostBuilder(Array.Empty<string>());
        return DesignTimeApplicationFactoryHelper.Create(hostBuilder);
    }

    private static bool ContainsArgument(string[] args, string argument)
    {
        return args.Any(arg => arg.TrimStart('/').TrimStart('-').ToLower() == argument.ToLower());
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(
                (context, config) =>
                {
                    Console.WriteLine($"Environment: {context.HostingEnvironment.EnvironmentName}");
                    Console.WriteLine($"ContentRootPath: {context.HostingEnvironment.ContentRootPath}");
                    Console.WriteLine($"BaseDirectory: {AppContext.BaseDirectory}");
                    var appSettings = Path.Combine(context.HostingEnvironment.ContentRootPath, "appsettings.json");
                    var envSettings = Path.Combine(
                        context.HostingEnvironment.ContentRootPath,
                        $"appsettings.{context.HostingEnvironment.EnvironmentName}.json");
                    Console.WriteLine($"appsettings.json exists: {File.Exists(appSettings)}");
                    Console.WriteLine($"env appsettings exists: {File.Exists(envSettings)}");
                    config.Sources.Clear();
                    var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    if (appPath != null && !string.IsNullOrWhiteSpace(appPath))
                    {
                        config.SetBasePath(appPath);
                    }
                    else if (!string.IsNullOrWhiteSpace(context.HostingEnvironment.ContentRootPath))
                    {
                        config.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    }
#if DEBUG
                    config.AddJsonFile(
                        $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                        optional: true,
                        reloadOnChange: true);

#else
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#endif
                }
               );

        host.ConfigureWebHostDefaults(
                       webBuilder =>
                       {
                           webBuilder.UseWebRoot("wwwroot");
                           webBuilder.UseStaticWebAssets();
                           webBuilder.ConfigureKestrel(
                               (context, options) =>
                                   {
                                       options.Limits.Http2.MaxStreamsPerConnection = 2048;
                                       options.Limits.MinResponseDataRate = null;
                                       options.Limits.MinRequestBodyDataRate = null;

                                   });
                           webBuilder.UseStartup<Startup>();
                       }).ConfigureLogging(
                   (context, loggingBuilder) =>
                   {
                       loggingBuilder.ClearProviders();
                       var services = loggingBuilder.Services;
                       LoggingHelper.ConfigureCustomLoggersWithConsoleAndDebug(
                           loggingBuilder,
                           services,
                           context.Configuration);
                   });

        return host;
    }

    public static int Main(string[] args)
    {
        ServicePointManager.Expect100Continue = false;
        if (ContainsArgument(args, "help") || ContainsArgument(args, "h"))
        {
            Console.WriteLine("Updates the database when its version does not match the application's version.");
            Console.WriteLine();
            Console.WriteLine(
                $"    {Assembly.GetExecutingAssembly().GetName().Name}.exe --updateDatabase [--forceUpdate --silent]");
            Console.WriteLine();
            Console.WriteLine(
                "--forceUpdate - Marks that the database must be updated whether its version matches the application's version or not.");
            Console.WriteLine(
                "--silent - Marks that database update proceeds automatically and does not require any interaction with the user.");
            Console.WriteLine();
            Console.WriteLine($"Exit codes: 0 - {DBUpdaterStatus.UpdateCompleted}");
            Console.WriteLine($"            1 - {DBUpdaterStatus.UpdateError}");
            Console.WriteLine($"            2 - {DBUpdaterStatus.UpdateNotNeeded}");
        }
        else
        {
            FrameworkSettings.DefaultSettingsCompatibilityMode = FrameworkSettingsCompatibilityMode.Latest;
            SecurityStrategy.AutoAssociationReferencePropertyMode = ReferenceWithoutAssociationPermissionsMode.AllMembers;
            IHost host = CreateHostBuilder(args).Build();
            if (ContainsArgument(args, "updateDatabase"))
            {
                using (var serviceScope = host.Services.CreateScope())
                {
                    return serviceScope.ServiceProvider
                        .GetRequiredService<IDBUpdater>()
                        .Update(ContainsArgument(args, "forceUpdate"), ContainsArgument(args, "silent"));
                }
            }
            else
            {
                host.Run();
            }
        }
        return 0;
    }

}
