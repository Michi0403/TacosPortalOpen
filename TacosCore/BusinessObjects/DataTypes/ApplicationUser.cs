//-----------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosCore.BusinessObjects.DataTypes;

public class ApplicationUser : TacoPermissionPolicyUser, ISecurityUserWithLoginInfo, ISecurityUserLockout, INotifyCollectionChanged, IObjectSpaceLink
{
    private TelegramUser? telegramUserThisApplicationUserBelongsTo;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => UserLogins.OfType<ISecurityUserLoginInfo>();
    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
    {
        ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey = providerUserKey;
        result.User = this;
        return result;
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public override void OnCreated()
    {
        if (!string.IsNullOrWhiteSpace(StoredPassword) && !StoredPassword.EndsWith("=="))
        {
            SetPassword(StoredPassword);
        }
        base.OnCreated();
    }

    [Browsable(false)]
    public virtual int AccessFailedCount { get; set; }
    [NotMapped]
    public virtual Dictionary<string, string> CustomClaims { get; set; } = new();

    [Browsable(false)]
    [Aggregated]
    [FieldSize(255)]
    public virtual string? Email { get; set; }

    [Browsable(false)]
    public virtual DateTime LockoutEnd { get; set; }
    [NotMapped]
    public virtual string? LoginProviderUserId { get; set; }
    [InverseProperty(nameof(TelegramUser.ApplicationUserThisTelegramUserBelongsTo))]

    public virtual TelegramUser? TelegramUserThisApplicationUserBelongsTo
    {
        get => telegramUserThisApplicationUserBelongsTo;
        set { OnPropertyChanging(nameof(TelegramUserThisApplicationUserBelongsTo)); telegramUserThisApplicationUserBelongsTo = value; OnPropertyChanged(nameof(TelegramUserThisApplicationUserBelongsTo)); }
    }

    public virtual IList<ApplicationUserLoginInfo> UserLogins { get; set; } = new ObservableCollection<ApplicationUserLoginInfo>();
}
