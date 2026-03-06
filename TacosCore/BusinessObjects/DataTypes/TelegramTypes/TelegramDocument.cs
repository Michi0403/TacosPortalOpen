//-----------------------------------------------------------------------
// <copyright file="TelegramDocument.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramDocument : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string? _fileName;
    private string? _mimeType;
    private TelegramPhotoSize? _thumbnail;

    private Guid? _thumbnailID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramExternalReplyInfo.Document))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisDocumentBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


    public virtual string? FileName
    {
        get => _fileName;
        set { OnPropertyChanging(nameof(FileName)); _fileName = value; OnPropertyChanged(nameof(FileName)); }
    }

    [InverseProperty(nameof(TelegramMessage.Document))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisDocumentBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    public virtual string? MimeType
    {
        get => _mimeType;
        set { OnPropertyChanging(nameof(MimeType)); _mimeType = value; OnPropertyChanged(nameof(MimeType)); }
    }


    [InverseProperty(nameof(TelegramPhotoSize.Documents))]
    public virtual TelegramPhotoSize? Thumbnail
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
}
