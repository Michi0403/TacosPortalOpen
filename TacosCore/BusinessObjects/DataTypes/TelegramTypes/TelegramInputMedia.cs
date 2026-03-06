//-----------------------------------------------------------------------
// <copyright file="TelegramInputMedia.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramInputMedia : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _caption = string.Empty;
    private TelegramInputFile? _media;

    private Guid? _mediaID;
    private ParseMode _parseMode;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string Caption
    {
        get => _caption;
        set { OnPropertyChanging(nameof(Caption)); _caption = value; OnPropertyChanged(nameof(Caption)); }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramInputFile? Media
    {
        get => _media;
        set { OnPropertyChanging(nameof(Media)); _media = value; OnPropertyChanged(nameof(Media)); }
    }

    [ForeignKey("Media")]
    public virtual Guid? MediaID
    {
        get => _mediaID;
        set { OnPropertyChanging(nameof(MediaID)); _mediaID = value; OnPropertyChanged(nameof(MediaID)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }
    public abstract InputMediaType Type { get; }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputMediaAnimation : TelegramInputMedia, TelegramIInputMediaThumb, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _duration;
    private bool _hasSpoiler;
    private int _height;
    private bool _showCaptionAboveMedia;
    private TelegramInputFile? _thumbnail = null;
    private Guid? _thumbnailID;
    private int _width;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual int Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }


    public virtual bool HasSpoiler
    {
        get => _hasSpoiler;
        set { OnPropertyChanging(nameof(HasSpoiler)); _hasSpoiler = value; OnPropertyChanged(nameof(HasSpoiler)); }
    }


    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }


    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
    }


    public virtual TelegramInputFile? Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID
    {
        get => _thumbnailID;
        set { OnPropertyChanging(nameof(ThumbnailID)); _thumbnailID = value; OnPropertyChanged(nameof(ThumbnailID)); }
    }


    public override InputMediaType Type => InputMediaType.Animation;


    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputMediaPhoto : TelegramInputMedia, TelegramIAlbumInputMedia, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _hasSpoiler;
    private bool _showCaptionAboveMedia;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual bool HasSpoiler
    {
        get => _hasSpoiler;
        set { OnPropertyChanging(nameof(HasSpoiler)); _hasSpoiler = value; OnPropertyChanged(nameof(HasSpoiler)); }
    }


    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
    }


    public override InputMediaType Type => InputMediaType.Photo;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputMediaVideo : TelegramInputMedia, TelegramIInputMediaThumb, TelegramIAlbumInputMedia, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramInputFile? _cover;
    private int _duration;
    private bool _hasSpoiler;
    private int _height;
    private bool _showCaptionAboveMedia;
    private int? _startTimestamp;
    private bool _supportsStreaming;
    private TelegramInputFile? _thumbnail;
    private int _width;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramInputFile? Cover
    {
        get => _cover;
        set { OnPropertyChanging(nameof(Cover)); _cover = value; OnPropertyChanged(nameof(Cover)); }
    }

    [ForeignKey("Cover")]
    public virtual Guid? CoverID { get; set; }

    public virtual int Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }

    public virtual bool HasSpoiler
    {
        get => _hasSpoiler;
        set { OnPropertyChanging(nameof(HasSpoiler)); _hasSpoiler = value; OnPropertyChanged(nameof(HasSpoiler)); }
    }

    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
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

    public virtual TelegramInputFile? Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID { get; set; }

    public override InputMediaType Type => InputMediaType.Video;

    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputMediaAudio : TelegramInputMedia, TelegramIInputMediaThumb, TelegramIAlbumInputMedia, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _duration;
    private string _performer = string.Empty;
    private TelegramInputFile? _thumbnail;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual int Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }

    public virtual string Performer
    {
        get => _performer;
        set { OnPropertyChanging(nameof(Performer)); _performer = value; OnPropertyChanged(nameof(Performer)); }
    }

    public virtual TelegramInputFile? Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID { get; set; }

    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    public override InputMediaType Type => InputMediaType.Audio;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputMediaDocument : TelegramInputMedia, TelegramIInputMediaThumb, TelegramIAlbumInputMedia, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _disableContentTypeDetection;
    private TelegramInputFile? _thumbnail;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool DisableContentTypeDetection
    {
        get => _disableContentTypeDetection;
        set { OnPropertyChanging(nameof(DisableContentTypeDetection)); _disableContentTypeDetection = value; OnPropertyChanged(nameof(DisableContentTypeDetection)); }
    }

    public virtual TelegramInputFile? Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID { get; set; }

    public override InputMediaType Type => InputMediaType.Document;
}
