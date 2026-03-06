//-----------------------------------------------------------------------
// <copyright file="TelegramWebhookInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;


[Authorize]
[DefaultClassOptions]
public partial class TelegramWebhookInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private IList<UpdateType>? _allowedUpdates = new ObservableCollection<UpdateType>();
    private bool _hasCustomCertificate;
    private string _ipAddress = string.Empty;
    private DateTime? _lastErrorDate;
    private string _lastErrorMessage = string.Empty;
    private DateTime? _lastSynchronizationErrorDate;
    private int? _maxConnections;
    private int _pendingUpdateCount;

    private string _url = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual IList<UpdateType>? AllowedUpdates
    {
        get => _allowedUpdates;
        set
        {
            if (_allowedUpdates != value)
            {
                OnPropertyChanging(nameof(AllowedUpdates));
                _allowedUpdates = value;
                OnPropertyChanged(nameof(AllowedUpdates));
            }
        }
    }


    public virtual bool HasCustomCertificate
    {
        get => _hasCustomCertificate;
        set
        {
            if (_hasCustomCertificate != value)
            {
                OnPropertyChanging(nameof(HasCustomCertificate));
                _hasCustomCertificate = value;
                OnPropertyChanged(nameof(HasCustomCertificate));
            }
        }
    }


    public virtual string IpAddress
    {
        get => _ipAddress;
        set
        {
            if (_ipAddress != value)
            {
                OnPropertyChanging(nameof(IpAddress));
                _ipAddress = value;
                OnPropertyChanged(nameof(IpAddress));
            }
        }
    }


    public virtual DateTime? LastErrorDate
    {
        get => _lastErrorDate;
        set
        {
            if (_lastErrorDate != value)
            {
                OnPropertyChanging(nameof(LastErrorDate));
                _lastErrorDate = value;
                OnPropertyChanged(nameof(LastErrorDate));
            }
        }
    }


    public virtual string LastErrorMessage
    {
        get => _lastErrorMessage;
        set
        {
            if (_lastErrorMessage != value)
            {
                OnPropertyChanging(nameof(LastErrorMessage));
                _lastErrorMessage = value;
                OnPropertyChanged(nameof(LastErrorMessage));
            }
        }
    }


    public virtual DateTime? LastSynchronizationErrorDate
    {
        get => _lastSynchronizationErrorDate;
        set
        {
            if (_lastSynchronizationErrorDate != value)
            {
                OnPropertyChanging(nameof(LastSynchronizationErrorDate));
                _lastSynchronizationErrorDate = value;
                OnPropertyChanged(nameof(LastSynchronizationErrorDate));
            }
        }
    }


    public virtual int? MaxConnections
    {
        get => _maxConnections;
        set
        {
            if (_maxConnections != value)
            {
                OnPropertyChanging(nameof(MaxConnections));
                _maxConnections = value;
                OnPropertyChanged(nameof(MaxConnections));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int PendingUpdateCount
    {
        get => _pendingUpdateCount;
        set
        {
            if (_pendingUpdateCount != value)
            {
                OnPropertyChanging(nameof(PendingUpdateCount));
                _pendingUpdateCount = value;
                OnPropertyChanged(nameof(PendingUpdateCount));
            }
        }
    }


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