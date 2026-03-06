//-----------------------------------------------------------------------
// <copyright file="TelegramVideoNote.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramVideoNote : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _duration;

    private int _length;
    private TelegramPhotoSize _thumbnail = null!;
    private Guid? _thumbnailID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




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

    [InverseProperty(nameof(TelegramExternalReplyInfo.VideoNote))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisVideoNoteBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Length
    {
        get => _length;
        set
        {
            if (_length != value)
            {
                OnPropertyChanging(nameof(Length));
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.VideoNote))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisVideoNoteBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

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
}