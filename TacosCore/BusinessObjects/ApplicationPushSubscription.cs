//-----------------------------------------------------------------------
// <copyright file="ApplicationPushSubscription.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TacosCore.BusinessObjects.DataTypes;

namespace TacosCore.BusinessObjects
{
    [DefaultClassOptions]
    public class ApplicationPushSubscription : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string auth = default!;
        private DateTime createdUtc = DateTime.UtcNow;

        private string endpoint = default!;
        private string p256dh = default!;
        private DateTime? updatedUtc;
        private ApplicationUser user = default!;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [Required, MaxLength(255)]
        public virtual string Auth
        {
            get => auth;
            set { OnPropertyChanging(nameof(Auth)); auth = value; OnPropertyChanged(nameof(Auth)); }
        }

        public virtual DateTime CreatedUtc
        {
            get => createdUtc;
            set { OnPropertyChanging(nameof(CreatedUtc)); createdUtc = value; OnPropertyChanged(nameof(CreatedUtc)); }
        }


        [Required, MaxLength(2048)]
        public virtual string Endpoint
        {
            get => endpoint;
            set { OnPropertyChanging(nameof(Endpoint)); endpoint = value; OnPropertyChanged(nameof(Endpoint)); }
        }
        [Required, MaxLength(255)]
        public virtual string P256dh
        {
            get => p256dh;
            set { OnPropertyChanging(nameof(P256dh)); p256dh = value; OnPropertyChanged(nameof(P256dh)); }
        }
        public virtual DateTime? UpdatedUtc
        {
            get => updatedUtc;
            set { OnPropertyChanging(nameof(UpdatedUtc)); updatedUtc = value; OnPropertyChanged(nameof(UpdatedUtc)); }
        }

        [Required]
        public virtual ApplicationUser User
        {
            get => user;
            set { OnPropertyChanging(nameof(User)); user = value; OnPropertyChanged(nameof(User)); }
        }
    }
}
