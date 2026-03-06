//-----------------------------------------------------------------------
// <copyright file="ApiDesignTimeTacoContextFactory.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TacosPortal.BusinessObjects
{
    public class ApiDesignTimeTacoContextFactory : IDesignTimeDbContextFactory<TacoContext>
    {
        public TacoContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TacoContext>();
            var configRoot = configuration.Get<TacosCore.BusinessObjects.ConfigurationRoot>();
            ArgumentNullException.ThrowIfNull(configRoot);
            ArgumentNullException.ThrowIfNull(configRoot.ConnectionStringsCore);
            _ = optionsBuilder.UseSqlServer(configRoot.ConnectionStringsCore.DefaultConnection, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseChangeTrackingProxies()
                .UseChangeTrackingProxies()
                .UseObjectSpaceLinkProxies();

            return new TacoContext(optionsBuilder.Options);
        }
    }
}
