//-----------------------------------------------------------------------
// <copyright file="TelegramChatAdministratorRights.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatAdministratorRights : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _canChangeInfo;
    private bool _canDeleteMessages;
    private bool _canDeleteStories;
    private bool? _canEditMessages;
    private bool _canEditStories;
    private bool _canInviteUsers;
    private bool _canManageChat;
    private bool? _canManageTopics;
    private bool _canManageVideoChats;
    private bool? _canPinMessages;
    private bool? _canPostMessages;
    private bool _canPostStories;
    private bool _canPromoteMembers;
    private bool _canRestrictMembers;

    private bool _isAnonymous;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool CanChangeInfo
    {
        get => _canChangeInfo;
        set { OnPropertyChanging(nameof(CanChangeInfo)); _canChangeInfo = value; OnPropertyChanged(nameof(CanChangeInfo)); }
    }

    public virtual bool CanDeleteMessages
    {
        get => _canDeleteMessages;
        set { OnPropertyChanging(nameof(CanDeleteMessages)); _canDeleteMessages = value; OnPropertyChanged(nameof(CanDeleteMessages)); }
    }

    public virtual bool CanDeleteStories
    {
        get => _canDeleteStories;
        set { OnPropertyChanging(nameof(CanDeleteStories)); _canDeleteStories = value; OnPropertyChanged(nameof(CanDeleteStories)); }
    }

    public virtual bool? CanEditMessages
    {
        get => _canEditMessages;
        set { OnPropertyChanging(nameof(CanEditMessages)); _canEditMessages = value; OnPropertyChanged(nameof(CanEditMessages)); }
    }

    public virtual bool CanEditStories
    {
        get => _canEditStories;
        set { OnPropertyChanging(nameof(CanEditStories)); _canEditStories = value; OnPropertyChanged(nameof(CanEditStories)); }
    }

    public virtual bool CanInviteUsers
    {
        get => _canInviteUsers;
        set { OnPropertyChanging(nameof(CanInviteUsers)); _canInviteUsers = value; OnPropertyChanged(nameof(CanInviteUsers)); }
    }

    public virtual bool CanManageChat
    {
        get => _canManageChat;
        set { OnPropertyChanging(nameof(CanManageChat)); _canManageChat = value; OnPropertyChanged(nameof(CanManageChat)); }
    }

    public virtual bool? CanManageTopics
    {
        get => _canManageTopics;
        set { OnPropertyChanging(nameof(CanManageTopics)); _canManageTopics = value; OnPropertyChanged(nameof(CanManageTopics)); }
    }

    public virtual bool CanManageVideoChats
    {
        get => _canManageVideoChats;
        set { OnPropertyChanging(nameof(CanManageVideoChats)); _canManageVideoChats = value; OnPropertyChanged(nameof(CanManageVideoChats)); }
    }

    public virtual bool? CanPinMessages
    {
        get => _canPinMessages;
        set { OnPropertyChanging(nameof(CanPinMessages)); _canPinMessages = value; OnPropertyChanged(nameof(CanPinMessages)); }
    }

    public virtual bool? CanPostMessages
    {
        get => _canPostMessages;
        set { OnPropertyChanging(nameof(CanPostMessages)); _canPostMessages = value; OnPropertyChanged(nameof(CanPostMessages)); }
    }

    public virtual bool CanPostStories
    {
        get => _canPostStories;
        set { OnPropertyChanging(nameof(CanPostStories)); _canPostStories = value; OnPropertyChanged(nameof(CanPostStories)); }
    }

    public virtual bool CanPromoteMembers
    {
        get => _canPromoteMembers;
        set { OnPropertyChanging(nameof(CanPromoteMembers)); _canPromoteMembers = value; OnPropertyChanged(nameof(CanPromoteMembers)); }
    }

    public virtual bool CanRestrictMembers
    {
        get => _canRestrictMembers;
        set { OnPropertyChanging(nameof(CanRestrictMembers)); _canRestrictMembers = value; OnPropertyChanged(nameof(CanRestrictMembers)); }
    }

    public virtual bool IsAnonymous
    {
        get => _isAnonymous;
        set { OnPropertyChanging(nameof(IsAnonymous)); _isAnonymous = value; OnPropertyChanged(nameof(IsAnonymous)); }
    }
    public virtual IList<TelegramKeyboardButtonRequestChat>? KeyboardButtonsToBotAdministratorRights { get; set; } = new ObservableCollection<TelegramKeyboardButtonRequestChat>();

    public virtual IList<TelegramKeyboardButtonRequestChat>? KeyboardButtonsToUserAdministratorRights { get; set; } = new ObservableCollection<TelegramKeyboardButtonRequestChat>();

    [InverseProperty(nameof(TacoPermissionPolicyRole.ChatAdministratorRights))]
    public virtual IList<TacoPermissionPolicyRole>? RolesThisChatAdministratorRightsBelongsTo { get; set; } = new ObservableCollection<TacoPermissionPolicyRole>();

    [InverseProperty(nameof(TelegramChatBotRightsUser.ChatAdministratorRights))]
    public virtual IList<TelegramChatBotRightsUser>? TelegramChatBotRightsUserThisTelegramChatAdministratorRightsBelongsTo { get; set; } = new ObservableCollection<TelegramChatBotRightsUser>();
}
