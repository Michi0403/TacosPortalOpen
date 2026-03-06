//-----------------------------------------------------------------------
// <copyright file="TelegramApiResponse.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public class TelegramApiResponse : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private string? _description;
        private int? _errorCode;

        private bool _ok;
        private TelegramResponseParameters? _parameters;
        private Guid? _parametersID;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




        public virtual string? Description
        {
            get => _description;
            set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
        }




        public virtual int? ErrorCode
        {
            get => _errorCode;
            set { OnPropertyChanging(nameof(ErrorCode)); _errorCode = value; OnPropertyChanged(nameof(ErrorCode)); }
        }




        public virtual bool Ok
        {
            get => _ok;
            set { OnPropertyChanging(nameof(Ok)); _ok = value; OnPropertyChanged(nameof(Ok)); }
        }

        public virtual TelegramResponseParameters? Parameters
        {
            get => _parameters;
            set { OnPropertyChanging(nameof(Parameters)); _parameters = value; OnPropertyChanged(nameof(Parameters)); }
        }




        [ForeignKey("Parameters")]
        public virtual Guid? ParametersID
        {
            get => _parametersID;
            set { OnPropertyChanging(nameof(ParametersID)); _parametersID = value; OnPropertyChanged(nameof(ParametersID)); }
        }
    }
}
