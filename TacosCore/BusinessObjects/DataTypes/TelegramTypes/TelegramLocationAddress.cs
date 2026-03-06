//-----------------------------------------------------------------------
// <copyright file="TelegramLocationAddress.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramLocationAddress : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _city = string.Empty;

    private string _countryCode = string.Empty;
    private string _state = string.Empty;
    private string _street = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string City
    {
        get => _city;
        set
        {
            if (_city != value)
            {
                OnPropertyChanging(nameof(City));
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string CountryCode
    {
        get => _countryCode;
        set
        {
            if (_countryCode != value)
            {
                OnPropertyChanging(nameof(CountryCode));
                _countryCode = value;
                OnPropertyChanged(nameof(CountryCode));
            }
        }
    }

    public virtual string State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                OnPropertyChanging(nameof(State));
                _state = value;
                OnPropertyChanged(nameof(State));
            }
        }
    }

    public virtual string Street
    {
        get => _street;
        set
        {
            if (_street != value)
            {
                OnPropertyChanging(nameof(Street));
                _street = value;
                OnPropertyChanged(nameof(Street));
            }
        }
    }
}
