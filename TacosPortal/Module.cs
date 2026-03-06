//-----------------------------------------------------------------------
// <copyright file="Module.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using TacosPortal.Services;

namespace TacosPortal;


public sealed class TacosPortalModule : ModuleBase
{
    public TacosPortalModule()
    {



        RequiredModuleTypes.Add(typeof(SystemModule));
        RequiredModuleTypes.Add(typeof(SecurityModule));
        SecurityModule.UsedExportedTypes = UsedExportedTypes.Custom;
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
        var ambient = objectSpace.ServiceProvider.GetRequiredService<IAmbientUserContext>();

        ModuleUpdater updater = new TacosPortal.DatabaseUpdate.Updater(objectSpace, versionFromDB, ambient);

        return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application)
    {
        base.Setup(application);

    }
}
