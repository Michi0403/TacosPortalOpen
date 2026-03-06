//-----------------------------------------------------------------------
// <copyright file="TelegramBackgroundFill.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    public abstract partial class TelegramBackgroundFill : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public abstract BackgroundFillType Type { get; }
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundFillSolid : TelegramBackgroundFill
    {

        private int _color;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Color
        {
            get => _color;
            set { OnPropertyChanging(nameof(Color)); _color = value; OnPropertyChanged(nameof(Color)); }
        }

        public override BackgroundFillType Type => BackgroundFillType.Solid;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundFillGradient : TelegramBackgroundFill
    {

        private int _bottomColor;

        private int _rotationAngle;

        private int _topColor;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int BottomColor
        {
            get => _bottomColor;
            set { OnPropertyChanging(nameof(BottomColor)); _bottomColor = value; OnPropertyChanged(nameof(BottomColor)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int RotationAngle
        {
            get => _rotationAngle;
            set { OnPropertyChanging(nameof(RotationAngle)); _rotationAngle = value; OnPropertyChanged(nameof(RotationAngle)); }
        }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int TopColor
        {
            get => _topColor;
            set { OnPropertyChanging(nameof(TopColor)); _topColor = value; OnPropertyChanged(nameof(TopColor)); }
        }

        public override BackgroundFillType Type => BackgroundFillType.Gradient;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBackgroundFillFreeformGradient : TelegramBackgroundFill
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public virtual IList<int>? Colors { get; set; } = new ObservableCollection<int>();

        public override BackgroundFillType Type => BackgroundFillType.FreeformGradient;
    }
}
