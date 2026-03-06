//-----------------------------------------------------------------------
// <copyright file="TelegramAnimation.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
    public partial class TelegramAnimation : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int _duration;
        private string? _fileName;
        private int _height;
        private string? _mimeType;
        private TelegramPhotoSize? _thumbnail;
        private Guid? _thumbnailID;

        private int _width;

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

        [InverseProperty(nameof(TelegramExternalReplyInfo.Animation))]
        [JsonIgnore]
        public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisAnimationBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


        public virtual string? FileName
        {
            get => _fileName;
            set { OnPropertyChanging(nameof(FileName)); _fileName = value; OnPropertyChanged(nameof(FileName)); }
        }

        [InverseProperty(nameof(TelegramGame.Animation))]
        [JsonIgnore]
        public virtual IList<TelegramGame>? GameThisAnimationBelongsTo { get; set; } = new ObservableCollection<TelegramGame>();


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Height
        {
            get => _height;
            set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
        }

        [InverseProperty(nameof(TelegramMessage.Animation))]
        [JsonIgnore]
        public virtual IList<TelegramMessage>? MessageAnimationBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


        public virtual string? MimeType
        {
            get => _mimeType;
            set { OnPropertyChanging(nameof(MimeType)); _mimeType = value; OnPropertyChanged(nameof(MimeType)); }
        }


        [InverseProperty(nameof(TelegramPhotoSize.Animations))]
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


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Width
        {
            get => _width;
            set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
        }
    }
}
