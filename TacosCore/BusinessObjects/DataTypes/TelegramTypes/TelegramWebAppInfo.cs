//-----------------------------------------------------------------------
// <copyright file="TelegramWebAppInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;


[Authorize]
[DefaultClassOptions]
public partial class TelegramWebAppInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _url = string.Empty;


    public TelegramWebAppInfo() { }





    public TelegramWebAppInfo(string url)
    {
        Url = url;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Url
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                OnPropertyChanging(nameof(Url));
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }
    }
}