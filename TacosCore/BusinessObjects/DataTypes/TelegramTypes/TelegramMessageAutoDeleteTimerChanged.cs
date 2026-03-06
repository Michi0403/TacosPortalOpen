//-----------------------------------------------------------------------
// <copyright file="TelegramMessageAutoDeleteTimerChanged.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramMessageAutoDeleteTimerChanged : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _messageAutoDeleteTime;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int MessageAutoDeleteTime
    {
        get => _messageAutoDeleteTime;
        set
        {
            OnPropertyChanging(nameof(MessageAutoDeleteTime));
            _messageAutoDeleteTime = value;
            OnPropertyChanged(nameof(MessageAutoDeleteTime));
        }
    }
}