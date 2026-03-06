//-----------------------------------------------------------------------
// <copyright file="TelegramUpdate.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;





[Authorize]
[DefaultClassOptions]
public abstract partial class TelegramUpdate : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _updateId;
    private UpdateType _updateType = UpdateType.Unknown;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int UpdateId
    {
        get => _updateId;
        set
        {
            if (_updateId != value)
            {
                OnPropertyChanging(nameof(UpdateId));
                _updateId = value;
                OnPropertyChanged(nameof(UpdateId));
            }
        }
    }

    [JsonIgnore]
    public virtual UpdateType UpdateType
    {
        get => _updateType;
        set
        {
            if (_updateType != value)
            {
                OnPropertyChanging(nameof(UpdateType));
                _updateType = value;
                OnPropertyChanged(nameof(UpdateType));
            }
        }
    }
}