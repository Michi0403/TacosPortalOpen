//-----------------------------------------------------------------------
// <copyright file="TelegramStickerSet.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
[Authorize]
[DefaultClassOptions]
public partial class TelegramStickerSet : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _name = string.Empty;
    private StickerType _stickerType;
    private TelegramPhotoSize _thumbnail = null!;
    private string _title = string.Empty;

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


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramSticker>? Stickers { get; set; } = new ObservableCollection<TelegramSticker>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual StickerType StickerType
    {
        get => _stickerType;
        set { OnPropertyChanging(nameof(StickerType)); _stickerType = value; OnPropertyChanged(nameof(StickerType)); }
    }


    public virtual TelegramPhotoSize Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
}