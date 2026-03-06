//-----------------------------------------------------------------------
// <copyright file="TacoTeam.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;

namespace TacosCore.BusinessObjects.DataTypes
{
    [Authorize]
    [DefaultClassOptions]
    public class TacoTeam : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TacoTeamChat? teamAdminChat;
        private Guid? teamAdminChatID;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public virtual string Name { get; set; } = null!;
        [InverseProperty(nameof(TacoPermissionPolicyRole.TeamsThisRoleBelongsTo))]
        public virtual IList<TacoPermissionPolicyRole>? RolesBelongToThisTeam { get; set; } = new ObservableCollection<TacoPermissionPolicyRole>();
        [InverseProperty(nameof(TacoTeamChat.TacoTeamThisAdminChatBelongsTo))]
        public virtual TacoTeamChat? TeamAdminChat
        {
            get => teamAdminChat;
            set { OnPropertyChanging(nameof(TeamAdminChat)); teamAdminChat = value; OnPropertyChanged(nameof(TeamAdminChat)); }
        }
        [ForeignKey("TeamAdminChat")]
        public virtual Guid? TeamAdminChatID
        {
            get => teamAdminChatID;
            set { OnPropertyChanging(nameof(TeamAdminChatID)); teamAdminChatID = value; OnPropertyChanged(nameof(TeamAdminChatID)); }
        }

        [InverseProperty(nameof(TacoTeamChat.TeamThisTeamChatBelongsTo))]
        public virtual IList<TacoTeamChat>? TeamChats { get; set; } = new ObservableCollection<TacoTeamChat>();

    }
}