//-----------------------------------------------------------------------
// <copyright file="TacoPermissionPolicyUser.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects
{
    [DisplayName("User")]
    [ImageName("BO_User")]
    [DefaultProperty("UserName")]
    [RuleCriteria(
        "PermissionPolicyUser_EF_Prevent_delete_logged_in_user",
        DefaultContexts.Delete,
        "[ID] != CurrentUserId()",
        "Cannot delete the current logged-in user. Please log in using another user account and retry.")]
    public abstract class TacoPermissionPolicyUser : BaseObject, IPermissionPolicyUser, ISecurityUser, IAuthenticationActiveDirectoryUser, IAuthenticationStandardUser, ISecurityUserWithRoles
    {




        public const string ruleId_RoleRequired = "Role required";

        public const string ruleId_UserNameFormatIsCorrect = "The username is formatted correctly";





        public const string ruleId_UserNameIsUnique = "User Name is unique";





        public const string ruleId_UserNameRequired = "User Name required";

        string IAuthenticationActiveDirectoryUser.UserName
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }

        bool IAuthenticationStandardUser.ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(StoredPassword, password);
        }

        bool IAuthenticationStandardUser.ChangePasswordOnFirstLogon
        {
            get
            {
                return ChangePasswordOnFirstLogon;
            }
            set
            {
                ChangePasswordOnFirstLogon = value;
            }
        }

        string IAuthenticationStandardUser.UserName => UserName;


        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles => Roles.OfType<IPermissionPolicyRole>();

        bool ISecurityUser.IsActive => IsActive;

        string ISecurityUser.UserName => UserName;

        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                List<ISecurityRole> list = new();
                foreach (TacoPermissionPolicyRole role in Roles)
                {
                    list.Add(role);
                }

                return new ReadOnlyCollection<ISecurityRole>(list);
            }
        }

        public void SetPassword(string password)
        {
            StoredPassword = PasswordCryptographer.HashPasswordDelegate(password);
        }





        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [Obsolete(
            "The Validation Module no longer uses this property. The corresponding validation Rules were removed.",
            true)]
        public bool ActiveAdminExists => false;









        public virtual bool ChangePasswordOnFirstLogon { get; set; }







        public virtual bool IsActive { get; set; } = true;









        public virtual IList<TacoPermissionPolicyRole> Roles
        {
            get;
            set;
        } = new ObservableCollection<TacoPermissionPolicyRole>();







        [Browsable(false)]
        [SecurityBrowsable]
        public virtual string StoredPassword { get; set; }







        [RuleRequiredField("User Name required", "Save", "The user name must not be empty")]
        [RuleUniqueValue(
            "User Name is unique",
            "Save",
            "The login with the entered UserName was already registered within the system")]
        [RuleUserNameFormatIsCorrect("The username is formatted correctly", "Save")]
        public virtual string UserName { get; set; }
    }
}
