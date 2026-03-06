//-----------------------------------------------------------------------
// <copyright file="ThemeJsChangeDispatcher.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TacosCore.BusinessObjects.Web;
using TacosPortal.Services;


public class ThemeJsChangeDispatcher : ComponentBase, IThemeChangeRequestDispatcher, IAsyncDisposable, IDisposable
{
    private IJSObjectReference? _module;

    private Theme? _pendingTheme;
    private bool disposedValue;








    void IDisposable.Dispose()
    {

        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    [Inject]
    private ISafeJSRuntime? JsRuntime { get; set; }
    [Inject]
    private ThemeService Themes { get; set; } = new ThemeService();

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _pendingTheme = null;
            }



            disposedValue = true;
        }
    }







    protected override async Task OnAfterRenderAsync(bool firstRender)
    {

        if (firstRender && JsRuntime != null)
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./switcher-resources/theme-controller.js").ConfigureAwait(false);
        Themes.ThemeChangeRequestDispatcher = this;
        if (Themes.ActiveTheme == null)
            Themes.SetActiveThemeByName(InitialThemeName);
        await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            GC.SuppressFinalize(this);
            if (_module != null)
                await _module.DisposeAsync().ConfigureAwait(false);
        }
        catch (JSDisconnectedException)
        {

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (Themes.ThemeChangeRequestDispatcher == this)
                Themes.ThemeChangeRequestDispatcher = null;
        }
    }

    public async void RequestThemeChange(Theme theme)
    {
        try
        {
            if (_pendingTheme == theme) return;
            _pendingTheme = theme;

            if (_module != null)
                await _module.InvokeVoidAsync("ThemeController.setStylesheetLinks",
                    theme.Name,
                    Themes.GetBootstrapThemeCssUrl(theme),
                    theme.BootstrapThemeMode,
                    Themes.GetThemeCssUrl(theme),
                    Themes.GetHighlightJSThemeCssUrl(theme),
                    DotNetObjectReference.Create(this)).ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error requesting theme:{theme.Name} change: {ex.ToString()}");

        }

    }

    [JSInvokable]
    public async Task ThemeLoadedAsync()
    {
        try
        {
            if (Themes.ThemeLoadNotifier != null && _pendingTheme != null)
            {
                await Themes.ThemeLoadNotifier.NotifyThemeLoadedAsync(_pendingTheme).ConfigureAwait(false);
            }
            _pendingTheme = null;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error notifying theme loaded for:{_pendingTheme?.Name} change: {ex.ToString()}");

        }

    }

    [Parameter]
    public required string InitialThemeName { get; set; }
}
