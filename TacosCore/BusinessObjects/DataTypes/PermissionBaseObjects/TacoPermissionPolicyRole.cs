//-----------------------------------------------------------------------
// <copyright file="TacoPermissionPolicyRole.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects
{



    [DisplayName("Role")]
    [ImageName("BO_Role")]
    public class TacoPermissionPolicyRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers, ICanInitializeRole, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramChatAdministratorRights? chatAdministratorRights;





        public TacoPermissionPolicyRole()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users => Users.OfType<IPermissionPolicyUser>();

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public bool AddUser(object user)
        {
            bool result = false;
            if (user is TacoPermissionPolicyUser item)
            {
                Users.Add(item);
                result = true;
            }

            return result;
        }





        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [Obsolete("The Validation Module no longer uses this property. The corresponding validation Rules were removed.", true)]
        public bool ActiveAdminExists => false;

        [InverseProperty(nameof(TelegramChatAdministratorRights.RolesThisChatAdministratorRightsBelongsTo))]
        public virtual TelegramChatAdministratorRights? ChatAdministratorRights
        {
            get => chatAdministratorRights;
            set { OnPropertyChanging(nameof(ChatAdministratorRights)); chatAdministratorRights = value; OnPropertyChanged(nameof(ChatAdministratorRights)); }
        }


        [NotMapped]
        public virtual bool IsTelegramChatAdminRole { get { if (this.ChatAdministratorRights != null) return true; else return false; } }

        [VisibleInListView(false)]
        [Appearance("EFPermissionPolicyIsAdministrative", Enabled = false, Criteria = "IsAdministrative", Context = "DetailView")]
        [JsonIgnore]
        public override SecurityPermissionPolicy PermissionPolicy { get; set; }

        [InverseProperty(nameof(TacoTeam.RolesBelongToThisTeam))]
        public virtual IList<TacoTeam> TeamsThisRoleBelongsTo { get; set; } = new ObservableCollection<TacoTeam>();


        [VisibleInListView(false)]
        [JsonIgnore]
        public virtual IList<TacoPermissionPolicyUser> Users { get; set; } = new ObservableCollection<TacoPermissionPolicyUser>();
    }
}
