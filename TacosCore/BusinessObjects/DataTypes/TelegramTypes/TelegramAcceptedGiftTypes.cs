//-----------------------------------------------------------------------
// <copyright file="TelegramAcceptedGiftTypes.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramAcceptedGiftTypes : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private bool _limitedGifts;
        private bool _premiumSubscription;
        private bool _uniqueGifts;

        private bool _unlimitedGifts;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


        public virtual bool LimitedGifts
        {
            get => _limitedGifts;
            set { OnPropertyChanging(nameof(LimitedGifts)); _limitedGifts = value; OnPropertyChanged(nameof(LimitedGifts)); }
        }


        public virtual bool PremiumSubscription
        {
            get => _premiumSubscription;
            set { OnPropertyChanging(nameof(PremiumSubscription)); _premiumSubscription = value; OnPropertyChanged(nameof(PremiumSubscription)); }
        }


        public virtual bool UniqueGifts
        {
            get => _uniqueGifts;
            set { OnPropertyChanging(nameof(UniqueGifts)); _uniqueGifts = value; OnPropertyChanged(nameof(UniqueGifts)); }
        }


        public virtual bool UnlimitedGifts
        {
            get => _unlimitedGifts;
            set { OnPropertyChanging(nameof(UnlimitedGifts)); _unlimitedGifts = value; OnPropertyChanged(nameof(UnlimitedGifts)); }
        }
    }
}
