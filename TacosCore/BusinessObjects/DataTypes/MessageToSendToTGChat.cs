//-----------------------------------------------------------------------
// <copyright file="MessageToSendToTGChat.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;



namespace TacosCore.BusinessObjects.DataTypes
{
    [Authorize]
    [DefaultClassOptions]
    public class MessageToSendToTGChat : BaseObject
    {
        [Required]

        public virtual ApplicationUser? ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public virtual Guid? ApplicationUserID { get; set; }

        public virtual bool IsSend { get; set; }
        [Required]

        public virtual TelegramInputTextMessageContent? MessageText { get; set; }
        [ForeignKey("MessageText")]
        public virtual Guid? MessageTextID { get; set; }
        [ForeignKey("TelegramChat")]
        public virtual Guid? SendToChatID { get; set; }
        [Required]

        public virtual TelegramChat? TelegramChat { get; set; }
    }
}
