//-----------------------------------------------------------------------
// <copyright file="TacoTeamChat.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosCore.BusinessObjects.DataTypes
{
    [Authorize]
    [DefaultClassOptions]
    public class TacoTeamChat : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramChatBotRightsUser botAssigned = null!;
        private Guid? botAssignedID;



        private TacoTeam? tacoTeamThisAdminChatBelongsTo = null!;
        private Guid? teamChatID;
        private TelegramChat telegramChat = null!;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [InverseProperty(nameof(TelegramChatBotRightsUser.TacoTeamsChatsThisBotUserRightsIsPartOf))]

        public virtual TelegramChatBotRightsUser BotAssigned
        {
            get => botAssigned;
            set { OnPropertyChanging(nameof(BotAssigned)); botAssigned = value; OnPropertyChanged(nameof(BotAssigned)); }
        }
        [ForeignKey("BotAssigned")]
        public virtual Guid? BotAssignedID
        {
            get => botAssignedID;
            set { OnPropertyChanging(nameof(BotAssignedID)); botAssignedID = value; OnPropertyChanged(nameof(BotAssignedID)); }
        }
        public virtual string Name { get; set; } = null!;

        [InverseProperty(nameof(TacoTeam.TeamAdminChat))]
        public virtual TacoTeam? TacoTeamThisAdminChatBelongsTo
        {
            get => tacoTeamThisAdminChatBelongsTo;
            set { OnPropertyChanging(nameof(TacoTeamThisAdminChatBelongsTo)); tacoTeamThisAdminChatBelongsTo = value; OnPropertyChanged(nameof(TacoTeamThisAdminChatBelongsTo)); }
        }
        [ForeignKey("TelegramChat")]
        public virtual Guid? TeamChatID
        {
            get => teamChatID;
            set { OnPropertyChanging(nameof(TeamChatID)); teamChatID = value; OnPropertyChanged(nameof(TeamChatID)); }
        }



        [InverseProperty(nameof(TacoTeam.TeamChats))]
        public virtual IList<TacoTeam>? TeamThisTeamChatBelongsTo { get; set; } = new ObservableCollection<TacoTeam>();
        [InverseProperty(nameof(TelegramChat.TacoTeamChatThisChatBelongsTo))]

        public virtual TelegramChat TelegramChat
        {
            get => telegramChat;
            set { OnPropertyChanging(nameof(TelegramChat)); telegramChat = value; OnPropertyChanged(nameof(TelegramChat)); }
        }
    }
}