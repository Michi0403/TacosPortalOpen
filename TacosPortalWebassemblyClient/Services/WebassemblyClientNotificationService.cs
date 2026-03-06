//-----------------------------------------------------------------------
// <copyright file="WebassemblyClientNotificationService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Blazor;
using TacosPortalWebassemblyClient.Interfaces;

namespace TacosPortalWebassemblyClient.Services
{
    public class WebassemblyClientNotificationService(IToastNotificationService toastService, ILogger<WebassemblyClientNotificationService> logger) : INotificationService
    {

        public void Show(string providerName, string title, string message, ToastRenderStyle renderStyle)
        {
            try
            {
                toastService.ShowToast(
               new ToastOptions
               {
                   ProviderName = providerName,
                   Title = title,
                   Text = message,
                   RenderStyle = renderStyle,
                   ThemeMode = ToastThemeMode.Auto,
                   ShowCloseButton = true,
                   ShowIcon = true,
                   FreezeOnClick = true,
                   SizeMode = SizeMode.Large,
               });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error showing toast notification: {MessageOriginBelongsTo}", message);
            }

        }
        public void ShowError(string providerName, string message, string title = "Error") => Show(providerName, title, message, ToastRenderStyle.Danger);

        public void ShowInfo(string providerName, string message, string title = "Info") => Show(providerName, title, message, ToastRenderStyle.Info);
        public void ShowRegular(string providerName, string message, string title = "Error") => Show(providerName, title, message, ToastRenderStyle.Primary);
        public void ShowSuccess(string providerName, string message, string title = "Success") => Show(providerName, title, message, ToastRenderStyle.Success);
        public void ShowWarning(string providerName, string message, string title = "Warning") => Show(providerName, title, message, ToastRenderStyle.Warning);
    }
}
