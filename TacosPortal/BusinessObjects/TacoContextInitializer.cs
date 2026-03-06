//-----------------------------------------------------------------------
// <copyright file="TacoContextInitializer.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.EFCore.DesignTime;
using Microsoft.EntityFrameworkCore;

namespace TacosPortal.BusinessObjects
{
    public class TacoContextInitializer : DbContextTypesInfoInitializerBase
    {
        protected override DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TacoContext>()
                .UseSqlServer(";")
                .UseChangeTrackingProxies()
                .UseObjectSpaceLinkProxies();
            return new TacoContext(optionsBuilder.Options);
        }
    }
}
