//-----------------------------------------------------------------------
// <copyright file="TelegramInputMessageContent.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramInputMessageContent : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputTextMessageContent : TelegramInputMessageContent
{
    private TelegramLinkPreviewOptions _linkPreviewOptions = null!;
    private Guid? _linkPreviewOptionsID;
    private string _messageText = string.Empty;
    private ParseMode _parseMode;

    public virtual IList<TelegramMessageEntity>? Entities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual TelegramLinkPreviewOptions LinkPreviewOptions
    {
        get => _linkPreviewOptions;
        set { OnPropertyChanging(nameof(LinkPreviewOptions)); _linkPreviewOptions = value; OnPropertyChanged(nameof(LinkPreviewOptions)); }
    }

    [ForeignKey("LinkPreviewOptions")]
    public virtual Guid? LinkPreviewOptionsID
    {
        get => _linkPreviewOptionsID;
        set { OnPropertyChanging(nameof(LinkPreviewOptionsID)); _linkPreviewOptionsID = value; OnPropertyChanged(nameof(LinkPreviewOptionsID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string MessageText
    {
        get => _messageText;
        set { OnPropertyChanging(nameof(MessageText)); _messageText = value; OnPropertyChanged(nameof(MessageText)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputLocationMessageContent : TelegramInputMessageContent, INotifyPropertyChanging, INotifyPropertyChanged
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

    public virtual int? Heading
    {
        get => _heading;
        set { OnPropertyChanging(nameof(Heading)); _heading = value; OnPropertyChanged(nameof(Heading)); }
    }

    public virtual double? HorizontalAccuracy
    {
        get => _horizontalAccuracy;
        set { OnPropertyChanging(nameof(HorizontalAccuracy)); _horizontalAccuracy = value; OnPropertyChanged(nameof(HorizontalAccuracy)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual double Latitude
    {
        get => _latitude;
        set { OnPropertyChanging(nameof(Latitude)); _latitude = value; OnPropertyChanged(nameof(Latitude)); }
    }

    public virtual int? LivePeriod
    {
        get => _livePeriod;
        set { OnPropertyChanging(nameof(LivePeriod)); _livePeriod = value; OnPropertyChanged(nameof(LivePeriod)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual double Longitude
    {
        get => _longitude;
        set { OnPropertyChanging(nameof(Longitude)); _longitude = value; OnPropertyChanged(nameof(Longitude)); }
    }

    public virtual int? ProximityAlertRadius
    {
        get => _proximityAlertRadius;
        set { OnPropertyChanging(nameof(ProximityAlertRadius)); _proximityAlertRadius = value; OnPropertyChanged(nameof(ProximityAlertRadius)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputVenueMessageContent : TelegramInputMessageContent, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _address = string.Empty;
    private string _foursquareId = string.Empty;
    private string _foursquareType = string.Empty;
    private string _googlePlaceId = string.Empty;
    private string _googlePlaceType = string.Empty;
    private double _latitude;
    private double _longitude;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Address
    {
        get => _address;
        set { OnPropertyChanging(nameof(Address)); _address = value; OnPropertyChanged(nameof(Address)); }
    }

    public virtual string FoursquareId
    {
        get => _foursquareId;
        set { OnPropertyChanging(nameof(FoursquareId)); _foursquareId = value; OnPropertyChanged(nameof(FoursquareId)); }
    }

    public virtual string FoursquareType
    {
        get => _foursquareType;
        set { OnPropertyChanging(nameof(FoursquareType)); _foursquareType = value; OnPropertyChanged(nameof(FoursquareType)); }
    }

    public virtual string GooglePlaceId
    {
        get => _googlePlaceId;
        set { OnPropertyChanging(nameof(GooglePlaceId)); _googlePlaceId = value; OnPropertyChanged(nameof(GooglePlaceId)); }
    }

    public virtual string GooglePlaceType
    {
        get => _googlePlaceType;
        set { OnPropertyChanging(nameof(GooglePlaceType)); _googlePlaceType = value; OnPropertyChanged(nameof(GooglePlaceType)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual double Latitude
    {
        get => _latitude;
        set { OnPropertyChanging(nameof(Latitude)); _latitude = value; OnPropertyChanged(nameof(Latitude)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual double Longitude
    {
        get => _longitude;
        set { OnPropertyChanging(nameof(Longitude)); _longitude = value; OnPropertyChanged(nameof(Longitude)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputContactMessageContent : TelegramInputMessageContent, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _phoneNumber = string.Empty;
    private string _vcard = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public required virtual string FirstName
    {
        get => _firstName;
        set { OnPropertyChanging(nameof(FirstName)); _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }

    public virtual string LastName
    {
        get => _lastName;
        set { OnPropertyChanging(nameof(LastName)); _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public required virtual string PhoneNumber
    {
        get => _phoneNumber;
        set { OnPropertyChanging(nameof(PhoneNumber)); _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
    }

    public virtual string Vcard
    {
        get => _vcard;
        set { OnPropertyChanging(nameof(Vcard)); _vcard = value; OnPropertyChanged(nameof(Vcard)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputInvoiceMessageContent : TelegramInputMessageContent, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _currency = string.Empty;
    private string _description = string.Empty;
    private bool _isFlexible;
    private int? _maxTipAmount;
    private bool _needEmail;
    private bool _needName;
    private bool _needPhoneNumber;
    private bool _needShippingAddress;
    private string _payload = string.Empty;
    private int? _photoHeight;
    private int? _photoSize;
    private string _photoUrl = string.Empty;
    private int? _photoWidth;
    private IList<TelegramLabeledPrice>? _prices = new ObservableCollection<TelegramLabeledPrice>();
    private string _providerData = string.Empty;
    private string _providerToken = string.Empty;
    private bool _sendEmailToProvider;
    private bool _sendPhoneNumberToProvider;
    private IList<int>? _suggestedTipAmounts = new ObservableCollection<int>();
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Currency
    {
        get => _currency;
        set { OnPropertyChanging(nameof(Currency)); _currency = value; OnPropertyChanged(nameof(Currency)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    public virtual bool IsFlexible
    {
        get => _isFlexible;
        set { OnPropertyChanging(nameof(IsFlexible)); _isFlexible = value; OnPropertyChanged(nameof(IsFlexible)); }
    }

    public virtual int? MaxTipAmount
    {
        get => _maxTipAmount;
        set { OnPropertyChanging(nameof(MaxTipAmount)); _maxTipAmount = value; OnPropertyChanged(nameof(MaxTipAmount)); }
    }

    public virtual bool NeedEmail
    {
        get => _needEmail;
        set { OnPropertyChanging(nameof(NeedEmail)); _needEmail = value; OnPropertyChanged(nameof(NeedEmail)); }
    }

    public virtual bool NeedName
    {
        get => _needName;
        set { OnPropertyChanging(nameof(NeedName)); _needName = value; OnPropertyChanged(nameof(NeedName)); }
    }

    public virtual bool NeedPhoneNumber
    {
        get => _needPhoneNumber;
        set { OnPropertyChanging(nameof(NeedPhoneNumber)); _needPhoneNumber = value; OnPropertyChanged(nameof(NeedPhoneNumber)); }
    }

    public virtual bool NeedShippingAddress
    {
        get => _needShippingAddress;
        set { OnPropertyChanging(nameof(NeedShippingAddress)); _needShippingAddress = value; OnPropertyChanged(nameof(NeedShippingAddress)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Payload
    {
        get => _payload;
        set { OnPropertyChanging(nameof(Payload)); _payload = value; OnPropertyChanged(nameof(Payload)); }
    }

    public virtual int? PhotoHeight
    {
        get => _photoHeight;
        set { OnPropertyChanging(nameof(PhotoHeight)); _photoHeight = value; OnPropertyChanged(nameof(PhotoHeight)); }
    }

    public virtual int? PhotoSize
    {
        get => _photoSize;
        set { OnPropertyChanging(nameof(PhotoSize)); _photoSize = value; OnPropertyChanged(nameof(PhotoSize)); }
    }

    public virtual string PhotoUrl
    {
        get => _photoUrl;
        set { OnPropertyChanging(nameof(PhotoUrl)); _photoUrl = value; OnPropertyChanged(nameof(PhotoUrl)); }
    }

    public virtual int? PhotoWidth
    {
        get => _photoWidth;
        set { OnPropertyChanging(nameof(PhotoWidth)); _photoWidth = value; OnPropertyChanged(nameof(PhotoWidth)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramLabeledPrice>? Prices
    {
        get => _prices;
    }

    public virtual string ProviderData
    {
        get => _providerData;
        set { OnPropertyChanging(nameof(ProviderData)); _providerData = value; OnPropertyChanged(nameof(ProviderData)); }
    }

    public virtual string ProviderToken
    {
        get => _providerToken;
        set { OnPropertyChanging(nameof(ProviderToken)); _providerToken = value; OnPropertyChanged(nameof(ProviderToken)); }
    }

    public virtual bool SendEmailToProvider
    {
        get => _sendEmailToProvider;
        set { OnPropertyChanging(nameof(SendEmailToProvider)); _sendEmailToProvider = value; OnPropertyChanged(nameof(SendEmailToProvider)); }
    }

    public virtual bool SendPhoneNumberToProvider
    {
        get => _sendPhoneNumberToProvider;
        set { OnPropertyChanging(nameof(SendPhoneNumberToProvider)); _sendPhoneNumberToProvider = value; OnPropertyChanged(nameof(SendPhoneNumberToProvider)); }
    }

    public virtual IList<int>? SuggestedTipAmounts
    {
        get => _suggestedTipAmounts;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
}
