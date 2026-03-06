//-----------------------------------------------------------------------
// <copyright file="WebApiApplicationSetup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.AspNetCore;
using DevExpress.ExpressApp.AspNetCore.WebApi;
using System.Diagnostics;

namespace TacosPortal.Services;

public class WebApiApplicationSetup(ILogger<WebApiApplicationSetup> logger) : IWebApiApplicationSetup
{
    public void SetupApplication(AspNetCoreApplication application)
    {
        try
        {
            application.Modules.Add(new TacosPortalModule());
            application.ObjectSpaceCreated += (sender, args) =>
            {
                if (sender is CompositeObjectSpace compositeObjectSpace)
                {
                    compositeObjectSpace.PopulateAdditionalObjectSpaces(application);
                }
            };

#if DEBUG
            if (Debugger.IsAttached)
            {
                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during WebApiApplicationSetup setup: {MessageOriginBelongsTo}", ex.Message);
        }

    }
}
