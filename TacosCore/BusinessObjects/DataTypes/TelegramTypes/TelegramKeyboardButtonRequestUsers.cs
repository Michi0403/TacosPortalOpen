//-----------------------------------------------------------------------
// <copyright file="TelegramKeyboardButtonRequestUsers.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramKeyboardButtonRequestUsers : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int? _maxQuantity;

    private int _requestId;
    private bool _requestName;
    private bool _requestPhoto;
    private bool _requestUsername;
    private bool? _userIsBot;
    private bool? _userIsPremium;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual int? MaxQuantity
    {
        get => _maxQuantity;
        set
        {
            if (_maxQuantity != value)
            {
                OnPropertyChanging(nameof(MaxQuantity));
                _maxQuantity = value;
                OnPropertyChanged(nameof(MaxQuantity));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int RequestId
    {
        get => _requestId;
        set
        {
            if (_requestId != value)
            {
                OnPropertyChanging(nameof(RequestId));
                _requestId = value;
                OnPropertyChanged(nameof(RequestId));
            }
        }
    }

    public virtual bool RequestName
    {
        get => _requestName;
        set
        {
            if (_requestName != value)
            {
                OnPropertyChanging(nameof(RequestName));
                _requestName = value;
                OnPropertyChanged(nameof(RequestName));
            }
        }
    }

    public virtual bool RequestPhoto
    {
        get => _requestPhoto;
        set
        {
            if (_requestPhoto != value)
            {
                OnPropertyChanging(nameof(RequestPhoto));
                _requestPhoto = value;
                OnPropertyChanged(nameof(RequestPhoto));
            }
        }
    }

    public virtual bool RequestUsername
    {
        get => _requestUsername;
        set
        {
            if (_requestUsername != value)
            {
                OnPropertyChanging(nameof(RequestUsername));
                _requestUsername = value;
                OnPropertyChanged(nameof(RequestUsername));
            }
        }
    }

    public virtual bool? UserIsBot
    {
        get => _userIsBot;
        set
        {
            if (_userIsBot != value)
            {
                OnPropertyChanging(nameof(UserIsBot));
                _userIsBot = value;
                OnPropertyChanged(nameof(UserIsBot));
            }
        }
    }

    public virtual bool? UserIsPremium
    {
        get => _userIsPremium;
        set
        {
            if (_userIsPremium != value)
            {
                OnPropertyChanging(nameof(UserIsPremium));
                _userIsPremium = value;
                OnPropertyChanged(nameof(UserIsPremium));
            }
        }
    }
}
