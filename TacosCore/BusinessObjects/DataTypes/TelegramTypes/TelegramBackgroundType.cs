//-----------------------------------------------------------------------
// <copyright file="TelegramBackgroundType.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    public abstract partial class TelegramBackgroundType : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public virtual IList<TelegramChatBackground>? ChatBackgroundThisBackgroundTypeBelongsTo { get; set; } = new ObservableCollection<TelegramChatBackground>();
        public abstract BackgroundTypeKind Type { get; }
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundTypeFill : TelegramBackgroundType
    {

        private int _darkThemeDimming;

        private TelegramBackgroundFill _fill = null!;

        private Guid? _fillID;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int DarkThemeDimming
        {
            get => _darkThemeDimming;
            set { OnPropertyChanging(nameof(DarkThemeDimming)); _darkThemeDimming = value; OnPropertyChanged(nameof(DarkThemeDimming)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public required virtual TelegramBackgroundFill Fill
        {
            get => _fill;
            set { OnPropertyChanging(nameof(Fill)); _fill = value; OnPropertyChanged(nameof(Fill)); }
        }
        [ForeignKey("Fill")]
        public virtual Guid? FillID
        {
            get => _fillID;
            set { OnPropertyChanging(nameof(FillID)); _fillID = value; OnPropertyChanged(nameof(FillID)); }
        }

        public override BackgroundTypeKind Type => BackgroundTypeKind.Fill;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundTypeWallpaper : TelegramBackgroundType
    {

        private int _darkThemeDimming;

        private TelegramDocument _document = null!;

        private Guid? _documentID;

        private bool _isBlurred;

        private bool _isMoving;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int DarkThemeDimming
        {
            get => _darkThemeDimming;
            set { OnPropertyChanging(nameof(DarkThemeDimming)); _darkThemeDimming = value; OnPropertyChanged(nameof(DarkThemeDimming)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual TelegramDocument Document
        {
            get => _document;
            set { OnPropertyChanging(nameof(Document)); _document = value; OnPropertyChanged(nameof(Document)); }
        }
        [ForeignKey("Document")]
        public virtual Guid? DocumentID
        {
            get => _documentID;
            set { OnPropertyChanging(nameof(DocumentID)); _documentID = value; OnPropertyChanged(nameof(DocumentID)); }
        }
        public virtual bool IsBlurred
        {
            get => _isBlurred;
            set { OnPropertyChanging(nameof(IsBlurred)); _isBlurred = value; OnPropertyChanged(nameof(IsBlurred)); }
        }
        public virtual bool IsMoving
        {
            get => _isMoving;
            set { OnPropertyChanging(nameof(IsMoving)); _isMoving = value; OnPropertyChanged(nameof(IsMoving)); }
        }

        public override BackgroundTypeKind Type => BackgroundTypeKind.Wallpaper;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundTypePattern : TelegramBackgroundType
    {

        private TelegramDocument _document;

        private Guid? _documentID;

        private TelegramBackgroundFill _fill = null!;

        private Guid? _fillID;

        private int _intensity;

        private bool _isInverted;

        private bool _isMoving;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual TelegramDocument Document
        {
            get => _document;
            set { OnPropertyChanging(nameof(Document)); _document = value; OnPropertyChanged(nameof(Document)); }
        }
        [ForeignKey("Document")]
        public virtual Guid? DocumentID
        {
            get => _documentID;
            set { OnPropertyChanging(nameof(DocumentID)); _documentID = value; OnPropertyChanged(nameof(DocumentID)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public required virtual TelegramBackgroundFill Fill
        {
            get => _fill;
            set { OnPropertyChanging(nameof(Fill)); _fill = value; OnPropertyChanged(nameof(Fill)); }
        }
        [ForeignKey("Fill")]
        public virtual Guid? FillID
        {
            get => _fillID;
            set { OnPropertyChanging(nameof(FillID)); _fillID = value; OnPropertyChanged(nameof(FillID)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Intensity
        {
            get => _intensity;
            set { OnPropertyChanging(nameof(Intensity)); _intensity = value; OnPropertyChanged(nameof(Intensity)); }
        }
        public virtual bool IsInverted
        {
            get => _isInverted;
            set { OnPropertyChanging(nameof(IsInverted)); _isInverted = value; OnPropertyChanged(nameof(IsInverted)); }
        }
        public virtual bool IsMoving
        {
            get => _isMoving;
            set { OnPropertyChanging(nameof(IsMoving)); _isMoving = value; OnPropertyChanged(nameof(IsMoving)); }
        }

        public override BackgroundTypeKind Type => BackgroundTypeKind.Pattern;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundTypeChatTheme : TelegramBackgroundType
    {

        private string _themeName = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual string ThemeName
        {
            get => _themeName;
            set { OnPropertyChanging(nameof(ThemeName)); _themeName = value; OnPropertyChanged(nameof(ThemeName)); }
        }

        public override BackgroundTypeKind Type => BackgroundTypeKind.ChatTheme;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramChatBackground : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramBackgroundType _type = null!;

        private Guid? _typeID;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [InverseProperty(nameof(TelegramMessage.ChatBackgroundSet))]

        [JsonIgnore]
        public virtual IList<TelegramMessage>? MessageChatBackgroundSetBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [InverseProperty(nameof(TelegramBackgroundType.ChatBackgroundThisBackgroundTypeBelongsTo))]
        public required virtual TelegramBackgroundType Type
        {
            get => _type;
            set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
        }
        [ForeignKey("Type")]
        public virtual Guid? TypeID
        {
            get => _typeID;
            set { OnPropertyChanging(nameof(TypeID)); _typeID = value; OnPropertyChanged(nameof(TypeID)); }
        }
    }
}
