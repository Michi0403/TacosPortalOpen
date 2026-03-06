//-----------------------------------------------------------------------
// <copyright file="TelegramBirthdate.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBirthdate : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private int _day;

        private int _month;

        private int? _year;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Day
        {
            get => _day;
            set
            {
                OnPropertyChanging(nameof(Day));
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Month
        {
            get => _month;
            set
            {
                OnPropertyChanging(nameof(Month));
                _month = value;
                OnPropertyChanged(nameof(Month));
            }
        }

        public virtual int? Year
        {
            get => _year;
            set
            {
                OnPropertyChanging(nameof(Year));
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }
    }
}
