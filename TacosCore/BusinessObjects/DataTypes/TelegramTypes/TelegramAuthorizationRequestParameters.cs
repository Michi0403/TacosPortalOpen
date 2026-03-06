//-----------------------------------------------------------------------
// <copyright file="TelegramAuthorizationRequestParameters.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramAuthorizationRequestParameters : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private long _botId;
        private string _nonce = string.Empty;
        private TelegramPassportScope _passportScope = null!;
        private Guid? _passportScopeID;
        private string _publicKey = string.Empty;
        private string _query = string.Empty;

        public TelegramAuthorizationRequestParameters()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));



        public override string ToString() => Uri;


        public virtual string AndroidUri => "tg:resolve?" + Query;


        [Required]
        [LongIntIDSanity]
        public virtual long BotId
        {
            get => _botId;
            set { OnPropertyChanging(nameof(BotId)); _botId = value; OnPropertyChanged(nameof(BotId)); }
        }





        public virtual string Nonce
        {
            get => _nonce;
            set { OnPropertyChanging(nameof(Nonce)); _nonce = value; OnPropertyChanged(nameof(Nonce)); }
        }

        public virtual TelegramPassportScope PassportScope
        {
            get => _passportScope;
            set { OnPropertyChanging(nameof(PassportScope)); _passportScope = value; OnPropertyChanged(nameof(PassportScope)); }
        }


        [ForeignKey("PassportScope")]
        public virtual Guid? PassportScopeID
        {
            get => _passportScopeID;
            set { OnPropertyChanging(nameof(PassportScopeID)); _passportScopeID = value; OnPropertyChanged(nameof(PassportScopeID)); }
        }


        public virtual string PublicKey
        {
            get => _publicKey;
            set { OnPropertyChanging(nameof(PublicKey)); _publicKey = value; OnPropertyChanged(nameof(PublicKey)); }
        }


        public virtual string Query
        {
            get => _query;
            set { OnPropertyChanging(nameof(Query)); _query = value; OnPropertyChanged(nameof(Query)); }
        }


        public virtual string Uri => "tg://resolve?" + Query;
    }
}
