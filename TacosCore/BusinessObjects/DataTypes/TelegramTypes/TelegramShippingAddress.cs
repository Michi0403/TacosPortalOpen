//-----------------------------------------------------------------------
// <copyright file="TelegramShippingAddress.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramShippingAddress : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _city = string.Empty;

    private string _countryCode = string.Empty;
    private string _postCode = string.Empty;
    private string _state = string.Empty;
    private string _streetLine1 = string.Empty;
    private string _streetLine2 = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string City
    {
        get => _city;
        set { OnPropertyChanging(nameof(City)); _city = value; OnPropertyChanged(nameof(City)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string CountryCode
    {
        get => _countryCode;
        set { OnPropertyChanging(nameof(CountryCode)); _countryCode = value; OnPropertyChanged(nameof(CountryCode)); }
    }

    [InverseProperty(nameof(TelegramOrderInfo.ShippingAddress))]
    public virtual IList<TelegramOrderInfo>? OrderInfoThisShippingAddressBelongsTo { get; set; } = new ObservableCollection<TelegramOrderInfo>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PostCode
    {
        get => _postCode;
        set { OnPropertyChanging(nameof(PostCode)); _postCode = value; OnPropertyChanged(nameof(PostCode)); }
    }

    [InverseProperty(nameof(TelegramShippingQuery.ShippingAddress))]
    public virtual IList<TelegramShippingQuery>? ShippingQueryThisShippingAddressBelongsTo { get; set; } = new ObservableCollection<TelegramShippingQuery>();

    public virtual string State
    {
        get => _state;
        set { OnPropertyChanging(nameof(State)); _state = value; OnPropertyChanged(nameof(State)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string StreetLine1
    {
        get => _streetLine1;
        set { OnPropertyChanging(nameof(StreetLine1)); _streetLine1 = value; OnPropertyChanged(nameof(StreetLine1)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string StreetLine2
    {
        get => _streetLine2;
        set { OnPropertyChanging(nameof(StreetLine2)); _streetLine2 = value; OnPropertyChanged(nameof(StreetLine2)); }
    }
}
