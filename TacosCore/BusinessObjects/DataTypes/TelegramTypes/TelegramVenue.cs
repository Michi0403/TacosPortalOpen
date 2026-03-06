//-----------------------------------------------------------------------
// <copyright file="TelegramVenue.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramVenue : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _address = string.Empty;
    private string _foursquareId = string.Empty;
    private string _foursquareType = string.Empty;
    private string _googlePlaceId = string.Empty;
    private string _googlePlaceType = string.Empty;
    private TelegramLocation _location = null!;

    private Guid? _locationID;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Address
    {
        get => _address;
        set
        {
            if (_address != value)
            {
                OnPropertyChanging(nameof(Address));
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Venue))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisVenueBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


    public virtual string FoursquareId
    {
        get => _foursquareId;
        set
        {
            if (_foursquareId != value)
            {
                OnPropertyChanging(nameof(FoursquareId));
                _foursquareId = value;
                OnPropertyChanged(nameof(FoursquareId));
            }
        }
    }


    public virtual string FoursquareType
    {
        get => _foursquareType;
        set
        {
            if (_foursquareType != value)
            {
                OnPropertyChanging(nameof(FoursquareType));
                _foursquareType = value;
                OnPropertyChanged(nameof(FoursquareType));
            }
        }
    }


    public virtual string GooglePlaceId
    {
        get => _googlePlaceId;
        set
        {
            if (_googlePlaceId != value)
            {
                OnPropertyChanging(nameof(GooglePlaceId));
                _googlePlaceId = value;
                OnPropertyChanged(nameof(GooglePlaceId));
            }
        }
    }


    public virtual string GooglePlaceType
    {
        get => _googlePlaceType;
        set
        {
            if (_googlePlaceType != value)
            {
                OnPropertyChanging(nameof(GooglePlaceType));
                _googlePlaceType = value;
                OnPropertyChanged(nameof(GooglePlaceType));
            }
        }
    }

    public virtual TelegramLocation Location
    {
        get => _location;
        set
        {
            if (_location != value)
            {
                OnPropertyChanging(nameof(Location));
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey(nameof(Location))]
    public virtual Guid? LocationID
    {
        get => _locationID;
        set
        {
            if (_locationID != value)
            {
                OnPropertyChanging(nameof(LocationID));
                _locationID = value;
                OnPropertyChanged(nameof(LocationID));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.Venue))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisVenueBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                OnPropertyChanging(nameof(Title));
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}