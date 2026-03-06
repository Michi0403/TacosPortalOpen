//-----------------------------------------------------------------------
// <copyright file="TelegramInputProfilePhoto.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public abstract partial class TelegramInputProfilePhoto : BaseObject
{
    public abstract InputProfilePhotoType Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputProfilePhotoStatic : TelegramInputProfilePhoto, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramInputFile _photo = null!;

    private Guid? _photoId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public required virtual TelegramInputFile Photo
    {
        get => _photo;
        set { OnPropertyChanging(nameof(Photo)); _photo = value; OnPropertyChanged(nameof(Photo)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("Photo")]
    public virtual Guid? PhotoID
    {
        get => _photoId;
        set { OnPropertyChanging(nameof(PhotoID)); _photoId = value; OnPropertyChanged(nameof(PhotoID)); }
    }

    public override InputProfilePhotoType Type => InputProfilePhotoType.Static;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputProfilePhotoAnimated : TelegramInputProfilePhoto, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramInputFile _animation = null!;

    private Guid? _animationId;
    private double? _mainFrameTimestamp;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public required virtual TelegramInputFile Animation
    {
        get => _animation;
        set { OnPropertyChanging(nameof(Animation)); _animation = value; OnPropertyChanged(nameof(Animation)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("Animation")]
    public virtual Guid? AnimationID
    {
        get => _animationId;
        set { OnPropertyChanging(nameof(AnimationID)); _animationId = value; OnPropertyChanged(nameof(AnimationID)); }
    }

    public virtual double? MainFrameTimestamp
    {
        get => _mainFrameTimestamp;
        set { OnPropertyChanging(nameof(MainFrameTimestamp)); _mainFrameTimestamp = value; OnPropertyChanged(nameof(MainFrameTimestamp)); }
    }

    public override InputProfilePhotoType Type => InputProfilePhotoType.Animated;
}
