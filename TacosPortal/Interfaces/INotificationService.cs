//-----------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Blazor;

namespace TacosPortal.Interfaces
{
    public interface INotificationService
    {
        void Show(string providerName, string title, string message, ToastRenderStyle renderStyle);
        void ShowError(string providerName, string message, string title = "Error");
        void ShowInfo(string providerName, string message, string title = "Info");
        void ShowRegular(string providerName, string message, string title = "Error");
        void ShowSuccess(string providerName, string message, string title = "Success");
        void ShowWarning(string providerName, string message, string title = "Warning");
    }
}