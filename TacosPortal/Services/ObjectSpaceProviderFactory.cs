//-----------------------------------------------------------------------
// <copyright file="ObjectSpaceProviderFactory.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.EntityFrameworkCore.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Security;
using TacosPortal.BusinessObjects;

namespace TacosPortal.Services;

public sealed class ObjectSpaceProviderFactory(ISecurityStrategyBase security, ITypesInfo typesInfo, IXafDbContextFactory<TacoContext> dbFactory) : IObjectSpaceProviderFactory
{



    IEnumerable<IObjectSpaceProvider> IObjectSpaceProviderFactory.CreateObjectSpaceProviders()
    {
        yield return new SecuredEFCoreObjectSpaceProvider<TacoContext>((ISelectDataSecurityProvider)security, dbFactory, typesInfo);
        yield return new NonPersistentObjectSpaceProvider(typesInfo, null);
    }
}
