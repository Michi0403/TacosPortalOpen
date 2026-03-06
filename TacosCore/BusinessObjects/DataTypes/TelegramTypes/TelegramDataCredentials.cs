//-----------------------------------------------------------------------
// <copyright file="TelegramDataCredentials.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramDataCredentials : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _dataHash = string.Empty;
    private string _secret = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string DataHash
    {
        get => _dataHash;
        set { OnPropertyChanging(nameof(DataHash)); _dataHash = value; OnPropertyChanged(nameof(DataHash)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Secret
    {
        get => _secret;
        set { OnPropertyChanging(nameof(Secret)); _secret = value; OnPropertyChanged(nameof(Secret)); }
    }
}
