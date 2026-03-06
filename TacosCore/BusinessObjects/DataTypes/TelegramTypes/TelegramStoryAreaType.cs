//-----------------------------------------------------------------------
// <copyright file="TelegramStoryAreaType.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramStoryAreaType : BaseObject
{
    public abstract StoryAreaTypeKind Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramStoryAreaTypeLocation : TelegramStoryAreaType, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramLocationAddress _address = null!;
    private Guid? _addressID;

    private double _latitude;
    private double _longitude;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramLocationAddress Address
    {
        get => _address;
        set { OnPropertyChanging(nameof(Address)); _address = value; OnPropertyChanged(nameof(Address)); }
    }

    [ForeignKey("Address")]
    public virtual Guid? AddressID
    {
        get => _addressID;
        set { OnPropertyChanging(nameof(AddressID)); _addressID = value; OnPropertyChanged(nameof(AddressID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Latitude
    {
        get => _latitude;
        set { OnPropertyChanging(nameof(Latitude)); _latitude = value; OnPropertyChanged(nameof(Latitude)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Longitude
    {
        get => _longitude;
        set { OnPropertyChanging(nameof(Longitude)); _longitude = value; OnPropertyChanged(nameof(Longitude)); }
    }

    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Location;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramStoryAreaTypeSuggestedReaction : TelegramStoryAreaType, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _isDark;
    private bool _isFlipped;
    private TelegramReactionType _reactionType = default!;

    private Guid? _reactionTypeID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool IsDark
    {
        get => _isDark;
        set { OnPropertyChanging(nameof(IsDark)); _isDark = value; OnPropertyChanged(nameof(IsDark)); }
    }

    public virtual bool IsFlipped
    {
        get => _isFlipped;
        set { OnPropertyChanging(nameof(IsFlipped)); _isFlipped = value; OnPropertyChanged(nameof(IsFlipped)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual TelegramReactionType ReactionType
    {
        get => _reactionType;
        set { OnPropertyChanging(nameof(ReactionType)); _reactionType = value; OnPropertyChanged(nameof(ReactionType)); }
    }

    [ForeignKey("ReactionType")]
    public virtual Guid? ReactionTypeID
    {
        get => _reactionTypeID;
        set { OnPropertyChanging(nameof(ReactionTypeID)); _reactionTypeID = value; OnPropertyChanged(nameof(ReactionTypeID)); }
    }

    public override StoryAreaTypeKind Type => StoryAreaTypeKind.SuggestedReaction;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramStoryAreaTypeLink : TelegramStoryAreaType, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _url = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Link;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramStoryAreaTypeWeather : TelegramStoryAreaType, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _backgroundColor;
    private string _emoji = string.Empty;

    private double _temperature;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int BackgroundColor
    {
        get => _backgroundColor;
        set { OnPropertyChanging(nameof(BackgroundColor)); _backgroundColor = value; OnPropertyChanged(nameof(BackgroundColor)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Emoji
    {
        get => _emoji;
        set { OnPropertyChanging(nameof(Emoji)); _emoji = value; OnPropertyChanged(nameof(Emoji)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Temperature
    {
        get => _temperature;
        set { OnPropertyChanging(nameof(Temperature)); _temperature = value; OnPropertyChanged(nameof(Temperature)); }
    }

    public override StoryAreaTypeKind Type => StoryAreaTypeKind.Weather;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramStoryAreaTypeUniqueGift : TelegramStoryAreaType, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _name = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }

    public override StoryAreaTypeKind Type => StoryAreaTypeKind.UniqueGift;
}
