//-----------------------------------------------------------------------
// <copyright file="TelegramAudio.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramAudio : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private int _duration;
        private string? _fileName;
        private string? _mimeType;
        private string? _performer;
        private TelegramPhotoSize? _thumbnail;
        private Guid? _thumbnailID;
        private string? _title;

        public TelegramAudio()
        {
        }

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
            set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        [InverseProperty(nameof(TelegramExternalReplyInfo.Audio))]
        [JsonIgnore]
        public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisAudioBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


        public virtual string? FileName
        {
            get => _fileName;
            set { OnPropertyChanging(nameof(FileName)); _fileName = value; OnPropertyChanged(nameof(FileName)); }
        }

        [InverseProperty(nameof(TelegramMessage.Audio))]
        [JsonIgnore]
        public virtual IList<TelegramMessage>? MessageThisAudioBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


        public virtual string? MimeType
        {
            get => _mimeType;
            set { OnPropertyChanging(nameof(MimeType)); _mimeType = value; OnPropertyChanged(nameof(MimeType)); }
        }


        public virtual string? Performer
        {
            get => _performer;
            set { OnPropertyChanging(nameof(Performer)); _performer = value; OnPropertyChanged(nameof(Performer)); }
        }


        [InverseProperty(nameof(TelegramPhotoSize.Audios))]
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


        public virtual string? Title
        {
            get => _title;
            set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
        }
    }
}
