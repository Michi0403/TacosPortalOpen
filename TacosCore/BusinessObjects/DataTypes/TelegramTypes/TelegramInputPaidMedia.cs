//-----------------------------------------------------------------------
// <copyright file="TelegramInputPaidMedia.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public abstract partial class TelegramInputPaidMedia : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramInputFile _media = null!;

    private Guid? _mediaId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public required virtual TelegramInputFile Media
    {
        get => _media;
        set { OnPropertyChanging(nameof(Media)); _media = value; OnPropertyChanged(nameof(Media)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("Media")]
    public virtual Guid? MediaID
    {
        get => _mediaId;
        set { OnPropertyChanging(nameof(MediaID)); _mediaId = value; OnPropertyChanged(nameof(MediaID)); }
    }
    public abstract InputPaidMediaType Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputPaidMediaPhoto : TelegramInputPaidMedia
{
    public override InputPaidMediaType Type => InputPaidMediaType.Photo;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputPaidMediaVideo : TelegramInputPaidMedia, TelegramIInputMediaThumb
{
    private TelegramInputFile _cover = null!;
    private Guid? _coverId;
    private int _duration;
    private int _height;
    private int? _startTimestamp;
    private bool _supportsStreaming;
    private TelegramInputFile _thumbnail = null!;
    private Guid? _thumbnailId;
    private int _width;

    public required virtual TelegramInputFile Cover
    {
        get => _cover;
        set { OnPropertyChanging(nameof(Cover)); _cover = value; OnPropertyChanged(nameof(Cover)); }
    }

    [ForeignKey("Cover")]
    public virtual Guid? CoverID
    {
        get => _coverId;
        set { OnPropertyChanging(nameof(CoverID)); _coverId = value; OnPropertyChanged(nameof(CoverID)); }
    }

    public virtual int Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }

    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public virtual int? StartTimestamp
    {
        get => _startTimestamp;
        set { OnPropertyChanging(nameof(StartTimestamp)); _startTimestamp = value; OnPropertyChanged(nameof(StartTimestamp)); }
    }

    public virtual bool SupportsStreaming
    {
        get => _supportsStreaming;
        set { OnPropertyChanging(nameof(SupportsStreaming)); _supportsStreaming = value; OnPropertyChanged(nameof(SupportsStreaming)); }
    }

    public required virtual TelegramInputFile Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID
    {
        get => _thumbnailId;
        set { OnPropertyChanging(nameof(ThumbnailID)); _thumbnailId = value; OnPropertyChanged(nameof(ThumbnailID)); }
    }

    public override InputPaidMediaType Type => InputPaidMediaType.Video;

    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }
}
