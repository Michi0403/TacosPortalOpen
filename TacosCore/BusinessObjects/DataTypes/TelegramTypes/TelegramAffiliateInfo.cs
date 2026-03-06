//-----------------------------------------------------------------------
// <copyright file="TelegramAffiliateInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramAffiliateInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {
        private TelegramChat? _affiliateChat;
        private Guid? _affiliateChatID;
        private TelegramUser? _affiliateUser;

        private Guid? _affiliateUserID;
        private int _amount;
        private int _commissionPerMille;
        private int? _nanostarAmount;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


        public virtual TelegramChat? AffiliateChat
        {
            get => _affiliateChat;
            set { OnPropertyChanging(nameof(AffiliateChat)); _affiliateChat = value; OnPropertyChanged(nameof(AffiliateChat)); }
        }

        [ForeignKey("AffiliateChat")]
        public virtual Guid? AffiliateChatID
        {
            get => _affiliateChatID;
            set { OnPropertyChanging(nameof(AffiliateChatID)); _affiliateChatID = value; OnPropertyChanged(nameof(AffiliateChatID)); }
        }


        public virtual TelegramUser? AffiliateUser
        {
            get => _affiliateUser;
            set { OnPropertyChanging(nameof(AffiliateUser)); _affiliateUser = value; OnPropertyChanged(nameof(AffiliateUser)); }
        }

        [ForeignKey("AffiliateUser")]
        public virtual Guid? AffiliateUserID
        {
            get => _affiliateUserID;
            set { OnPropertyChanging(nameof(AffiliateUserID)); _affiliateUserID = value; OnPropertyChanged(nameof(AffiliateUserID)); }
        }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int Amount
        {
            get => _amount;
            set { OnPropertyChanging(nameof(Amount)); _amount = value; OnPropertyChanged(nameof(Amount)); }
        }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual int CommissionPerMille
        {
            get => _commissionPerMille;
            set { OnPropertyChanging(nameof(CommissionPerMille)); _commissionPerMille = value; OnPropertyChanged(nameof(CommissionPerMille)); }
        }


        public virtual int? NanostarAmount
        {
            get => _nanostarAmount;
            set { OnPropertyChanging(nameof(NanostarAmount)); _nanostarAmount = value; OnPropertyChanged(nameof(NanostarAmount)); }
        }
    }
}
