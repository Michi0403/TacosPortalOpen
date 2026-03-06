//-----------------------------------------------------------------------
// <copyright file="TelegramVideo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramVideo : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _duration;
    private string _fileName = string.Empty;
    private int _height;
    private string _mimeType = string.Empty;
    private int? _startTimestamp;
    private TelegramPhotoSize _thumbnail = null!;
    private Guid? _thumbnailID;

    private int _width;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [InverseProperty(nameof(TelegramPhotoSize.VideoCovers))]
    public virtual IList<TelegramPhotoSize>? Cover { get; set; } = new ObservableCollection<TelegramPhotoSize>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Duration
    {
        get => _duration;
        set
        {
            if (_duration != value)
            {
                OnPropertyChanging(nameof(Duration));
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Video))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisVideoBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


    public virtual string FileName
    {
        get => _fileName;
        set
        {
            if (_fileName != value)
            {
                OnPropertyChanging(nameof(FileName));
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                OnPropertyChanging(nameof(Height));
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.Video))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisVideoBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    public virtual string MimeType
    {
        get => _mimeType;
        set
        {
            if (_mimeType != value)
            {
                OnPropertyChanging(nameof(MimeType));
                _mimeType = value;
                OnPropertyChanged(nameof(MimeType));
            }
        }
    }


    public virtual int? StartTimestamp
    {
        get => _startTimestamp;
        set
        {
            if (_startTimestamp != value)
            {
                OnPropertyChanging(nameof(StartTimestamp));
                _startTimestamp = value;
                OnPropertyChanged(nameof(StartTimestamp));
            }
        }
    }

    public virtual TelegramPhotoSize Thumbnail
    {
        get => _thumbnail;
        set
        {
            if (_thumbnail != value)
            {
                OnPropertyChanging(nameof(Thumbnail));
                _thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }
    }


    [ForeignKey(nameof(Thumbnail))]
    [InverseProperty(nameof(TelegramPhotoSize.VideoThumbnails))]
    public virtual Guid? ThumbnailID
    {
        get => _thumbnailID;
        set
        {
            if (_thumbnailID != value)
            {
                OnPropertyChanging(nameof(ThumbnailID));
                _thumbnailID = value;
                OnPropertyChanged(nameof(ThumbnailID));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                OnPropertyChanging(nameof(Width));
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }
}