//-----------------------------------------------------------------------
// <copyright file="TelegramLocation.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramLocation : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int? _heading;
    private double? _horizontalAccuracy;

    private double _latitude;
    private int? _livePeriod;
    private double _longitude;
    private int? _proximityAlertRadius;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramChosenInlineResult.Location))]
    [JsonIgnore]
    public virtual IList<TelegramChosenInlineResult>? ChosenInlineResultThisLocationBelongsTo { get; set; } = new ObservableCollection<TelegramChosenInlineResult>();

    [InverseProperty(nameof(TelegramExternalReplyInfo.Location))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisLocationBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    public virtual int? Heading
    {
        get => _heading;
        set
        {
            if (_heading != value)
            {
                OnPropertyChanging(nameof(Heading));
                _heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }
    }

    public virtual double? HorizontalAccuracy
    {
        get => _horizontalAccuracy;
        set
        {
            if (_horizontalAccuracy != value)
            {
                OnPropertyChanging(nameof(HorizontalAccuracy));
                _horizontalAccuracy = value;
                OnPropertyChanged(nameof(HorizontalAccuracy));
            }
        }
    }

    [InverseProperty(nameof(TelegramInlineQuery.Location))]
    [JsonIgnore]
    public virtual IList<TelegramInlineQuery>? InlineQueryThisLocationBelongsTo { get; set; } = new ObservableCollection<TelegramInlineQuery>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Latitude
    {
        get => _latitude;
        set
        {
            if (_latitude != value)
            {
                OnPropertyChanging(nameof(Latitude));
                _latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }
    }

    public virtual int? LivePeriod
    {
        get => _livePeriod;
        set
        {
            if (_livePeriod != value)
            {
                OnPropertyChanging(nameof(LivePeriod));
                _livePeriod = value;
                OnPropertyChanged(nameof(LivePeriod));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Longitude
    {
        get => _longitude;
        set
        {
            if (_longitude != value)
            {
                OnPropertyChanging(nameof(Longitude));
                _longitude = value;
                OnPropertyChanged(nameof(Longitude));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.Location))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisLocationBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual int? ProximityAlertRadius
    {
        get => _proximityAlertRadius;
        set
        {
            if (_proximityAlertRadius != value)
            {
                OnPropertyChanging(nameof(ProximityAlertRadius));
                _proximityAlertRadius = value;
                OnPropertyChanged(nameof(ProximityAlertRadius));
            }
        }
    }

    [InverseProperty(nameof(TelegramVenue.Location))]
    [JsonIgnore]
    public virtual IList<TelegramVenue>? VenueThisLocationBelongsTo { get; set; } = new ObservableCollection<TelegramVenue>();
}
