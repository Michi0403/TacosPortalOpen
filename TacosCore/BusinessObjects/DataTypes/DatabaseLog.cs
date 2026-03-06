//-----------------------------------------------------------------------
// <copyright file="DatabaseLog.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.BaseImpl.EF;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes
{
    public class DatabaseLog : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string entityKey = default!;

        private string entityType = default!;
        private string? newValue;
        private string? oldValue;
        private string operation = default!;
        private string? propertyName;
        private string? requestId;
        private Guid? userId;
        private string? userName;
        private DateTime utcTimestamp;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public virtual string EntityKey
        {
            get => entityKey;
            set { OnPropertyChanging(nameof(EntityKey)); entityKey = value; OnPropertyChanged(nameof(EntityKey)); }
        }
        public virtual string EntityType
        {
            get => entityType;
            set { OnPropertyChanging(nameof(EntityType)); entityType = value; OnPropertyChanged(nameof(EntityType)); }
        }
        public virtual string? NewValue
        {
            get => newValue;
            set { OnPropertyChanging(nameof(NewValue)); newValue = value; OnPropertyChanged(nameof(NewValue)); }
        }
        public virtual string? OldValue
        {
            get => oldValue;
            set { OnPropertyChanging(nameof(OldValue)); oldValue = value; OnPropertyChanged(nameof(OldValue)); }
        }
        public virtual string Operation
        {
            get => operation;
            set { OnPropertyChanging(nameof(Operation)); operation = value; OnPropertyChanged(nameof(Operation)); }
        }

        public virtual string? PropertyName
        {
            get => propertyName;
            set { OnPropertyChanging(nameof(PropertyName)); propertyName = value; OnPropertyChanged(nameof(PropertyName)); }
        }

        public virtual string? RequestId
        {
            get => requestId;
            set { OnPropertyChanging(nameof(RequestId)); requestId = value; OnPropertyChanged(nameof(RequestId)); }
        }

        public virtual Guid? UserId
        {
            get => userId;
            set { OnPropertyChanging(nameof(UserId)); userId = value; OnPropertyChanged(nameof(UserId)); }
        }
        public virtual string? UserName
        {
            get => userName;
            set { OnPropertyChanging(nameof(UserName)); userName = value; OnPropertyChanged(nameof(UserName)); }
        }
        public virtual DateTime UtcTimestamp
        {
            get => utcTimestamp;
            set { OnPropertyChanging(nameof(UtcTimestamp)); utcTimestamp = value; OnPropertyChanged(nameof(UtcTimestamp)); }
        }
    }
}
