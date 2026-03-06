//-----------------------------------------------------------------------
// <copyright file="TelegramInlineQueryResultsButton.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultsButton : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _startParameter = string.Empty;

    private string _text = string.Empty;
    private TelegramWebAppInfo _webApp = null!;
    private Guid? _webAppId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    public virtual string StartParameter
    {
        get => _startParameter;
        set { OnPropertyChanging(nameof(StartParameter)); _startParameter = value; OnPropertyChanged(nameof(StartParameter)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public virtual TelegramWebAppInfo WebApp
    {
        get => _webApp;
        set { OnPropertyChanging(nameof(WebApp)); _webApp = value; OnPropertyChanged(nameof(WebApp)); }
    }




    [ForeignKey("WebApp")]
    public virtual Guid? WebAppID
    {
        get => _webAppId;
        set { OnPropertyChanging(nameof(WebAppID)); _webAppId = value; OnPropertyChanged(nameof(WebAppID)); }
    }
}
