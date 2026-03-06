//-----------------------------------------------------------------------
// <copyright file="TelegramPhotoSize.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public class TelegramPhotoSizeGroup : BaseObject
{
    public virtual IList<TelegramPhotoSize>? Photos { get; set; } = new ObservableCollection<TelegramPhotoSize>();
}



[Authorize]
[DefaultClassOptions]
public partial class TelegramPhotoSize : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _height;

    private int _width;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramAnimation.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramAnimation>? Animations { get; set; } = new ObservableCollection<TelegramAnimation>();

    [InverseProperty(nameof(TelegramAudio.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramAudio>? Audios { get; set; } = new ObservableCollection<TelegramAudio>();

    [InverseProperty(nameof(TelegramDocument.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramDocument>? Documents { get; set; } = new ObservableCollection<TelegramDocument>();

    [InverseProperty(nameof(TelegramExternalReplyInfo.Photo))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplies { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramGame.Photo))]
    [JsonIgnore]
    public virtual IList<TelegramGame>? Games { get; set; } = new ObservableCollection<TelegramGame>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }

    [InverseProperty(nameof(TelegramMessage.NewChatPhoto))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesAsNewChatPhoto { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramMessage.Photo))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesAsPhoto { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramChatShared.Photo))]
    [JsonIgnore]
    public virtual IList<TelegramChatShared>? SharedChats { get; set; } = new ObservableCollection<TelegramChatShared>();

    [InverseProperty(nameof(TelegramSharedUser.Photo))]
    [JsonIgnore]
    public virtual IList<TelegramSharedUser>? SharedUsers { get; set; } = new ObservableCollection<TelegramSharedUser>();

    [InverseProperty(nameof(TelegramSticker.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramSticker>? Stickers { get; set; } = new ObservableCollection<TelegramSticker>();

    [InverseProperty(nameof(TelegramVideo.Cover))]
    [JsonIgnore]
    public virtual IList<TelegramVideo>? VideoCovers { get; set; } = new ObservableCollection<TelegramVideo>();

    [InverseProperty(nameof(TelegramVideoNote.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramVideoNote>? VideoNotes { get; set; } = new ObservableCollection<TelegramVideoNote>();

    [InverseProperty(nameof(TelegramVideo.Thumbnail))]
    [JsonIgnore]
    public virtual IList<TelegramVideo>? VideoThumbnails { get; set; } = new ObservableCollection<TelegramVideo>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }
}

