//-----------------------------------------------------------------------
// <copyright file="TelegramChatMember.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramChatMember : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramUser _user = default!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool IsAdmin { get; set; }
    public virtual bool IsInChat { get; set; }

    public virtual bool? IsNew { get; set; }

    [InverseProperty(nameof(TelegramChatMemberUpdated.NewChatMember))]
    public virtual IList<TelegramChatMemberUpdated>? NewChatMemberUpdatedThisChatMemberBelongsTo { get; set; } = new ObservableCollection<TelegramChatMemberUpdated>();
    [InverseProperty(nameof(TelegramChatMemberUpdated.OldChatMember))]
    public virtual IList<TelegramChatMemberUpdated>? OldChatMemberUpdatedThisChatMemberBelongsTo { get; set; } = new ObservableCollection<TelegramChatMemberUpdated>();
    public virtual ChatMemberStatus Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(User.ChatMemberThisUserBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberOwner : TelegramChatMember
{
    private string _customTitle = string.Empty;

    private bool _isAnonymous;

    public virtual string CustomTitle
    {
        get => _customTitle;
        set { OnPropertyChanging(nameof(CustomTitle)); _customTitle = value; OnPropertyChanged(nameof(CustomTitle)); }
    }

    public virtual bool IsAnonymous
    {
        get => _isAnonymous;
        set { OnPropertyChanging(nameof(IsAnonymous)); _isAnonymous = value; OnPropertyChanged(nameof(IsAnonymous)); }
    }
    public virtual ChatMemberStatus Status => ChatMemberStatus.Creator;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberAdministrator : TelegramChatMember, INotifyPropertyChanging, INotifyPropertyChanged
{

    private bool _canBeEdited;
    private bool _canChangeInfo;
    private bool _canDeleteMessages;
    private bool _canDeleteStories;
    private bool _canEditMessages;
    private bool _canEditStories;
    private bool _canInviteUsers;
    private bool _canManageChat;
    private bool _canManageTopics;
    private bool _canManageVideoChats;
    private bool _canPinMessages;
    private bool _canPostMessages;
    private bool _canPostStories;
    private bool _canPromoteMembers;
    private bool _canRestrictMembers;
    private string _customTitle = string.Empty;
    private bool _isAnonymous;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool CanBeEdited
    {
        get => _canBeEdited;
        set { OnPropertyChanging(nameof(CanBeEdited)); _canBeEdited = value; OnPropertyChanged(nameof(CanBeEdited)); }
    }

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

    public virtual bool CanEditMessages
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

    public virtual bool CanManageTopics
    {
        get => _canManageTopics;
        set { OnPropertyChanging(nameof(CanManageTopics)); _canManageTopics = value; OnPropertyChanged(nameof(CanManageTopics)); }
    }

    public virtual bool CanManageVideoChats
    {
        get => _canManageVideoChats;
        set { OnPropertyChanging(nameof(CanManageVideoChats)); _canManageVideoChats = value; OnPropertyChanged(nameof(CanManageVideoChats)); }
    }

    public virtual bool CanPinMessages
    {
        get => _canPinMessages;
        set { OnPropertyChanging(nameof(CanPinMessages)); _canPinMessages = value; OnPropertyChanged(nameof(CanPinMessages)); }
    }

    public virtual bool CanPostMessages
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

    public virtual string CustomTitle
    {
        get => _customTitle;
        set { OnPropertyChanging(nameof(CustomTitle)); _customTitle = value; OnPropertyChanged(nameof(CustomTitle)); }
    }

    public virtual bool IsAnonymous
    {
        get => _isAnonymous;
        set { OnPropertyChanging(nameof(IsAnonymous)); _isAnonymous = value; OnPropertyChanged(nameof(IsAnonymous)); }
    }

    public virtual ChatMemberStatus Status => ChatMemberStatus.Administrator;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberMember : TelegramChatMember, INotifyPropertyChanging, INotifyPropertyChanged
{

    private DateTime? _untilDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual ChatMemberStatus Status => ChatMemberStatus.Member;


    public virtual DateTime? UntilDate
    {
        get => _untilDate;
        set
        {
            OnPropertyChanging(nameof(UntilDate));
            _untilDate = value;
            OnPropertyChanged(nameof(UntilDate));
        }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberRestricted : TelegramChatMember, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _canAddWebPagePreviews;
    private bool _canChangeInfo;
    private bool _canInviteUsers;
    private bool _canManageTopics;
    private bool _canPinMessages;
    private bool _canSendAudios;
    private bool _canSendDocuments;
    private bool _canSendMessages;
    private bool _canSendOtherMessages;
    private bool _canSendPhotos;
    private bool _canSendPolls;
    private bool _canSendVideoNotes;
    private bool _canSendVideos;
    private bool _canSendVoiceNotes;

    private bool _isMember;
    private DateTime? _untilDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool CanAddWebPagePreviews
    {
        get => _canAddWebPagePreviews;
        set { OnPropertyChanging(nameof(CanAddWebPagePreviews)); _canAddWebPagePreviews = value; OnPropertyChanged(nameof(CanAddWebPagePreviews)); }
    }

    public virtual bool CanChangeInfo
    {
        get => _canChangeInfo;
        set { OnPropertyChanging(nameof(CanChangeInfo)); _canChangeInfo = value; OnPropertyChanged(nameof(CanChangeInfo)); }
    }

    public virtual bool CanInviteUsers
    {
        get => _canInviteUsers;
        set { OnPropertyChanging(nameof(CanInviteUsers)); _canInviteUsers = value; OnPropertyChanged(nameof(CanInviteUsers)); }
    }

    public virtual bool CanManageTopics
    {
        get => _canManageTopics;
        set { OnPropertyChanging(nameof(CanManageTopics)); _canManageTopics = value; OnPropertyChanged(nameof(CanManageTopics)); }
    }

    public virtual bool CanPinMessages
    {
        get => _canPinMessages;
        set { OnPropertyChanging(nameof(CanPinMessages)); _canPinMessages = value; OnPropertyChanged(nameof(CanPinMessages)); }
    }

    public virtual bool CanSendAudios
    {
        get => _canSendAudios;
        set { OnPropertyChanging(nameof(CanSendAudios)); _canSendAudios = value; OnPropertyChanged(nameof(CanSendAudios)); }
    }

    public virtual bool CanSendDocuments
    {
        get => _canSendDocuments;
        set { OnPropertyChanging(nameof(CanSendDocuments)); _canSendDocuments = value; OnPropertyChanged(nameof(CanSendDocuments)); }
    }

    public virtual bool CanSendMessages
    {
        get => _canSendMessages;
        set { OnPropertyChanging(nameof(CanSendMessages)); _canSendMessages = value; OnPropertyChanged(nameof(CanSendMessages)); }
    }

    public virtual bool CanSendOtherMessages
    {
        get => _canSendOtherMessages;
        set { OnPropertyChanging(nameof(CanSendOtherMessages)); _canSendOtherMessages = value; OnPropertyChanged(nameof(CanSendOtherMessages)); }
    }

    public virtual bool CanSendPhotos
    {
        get => _canSendPhotos;
        set { OnPropertyChanging(nameof(CanSendPhotos)); _canSendPhotos = value; OnPropertyChanged(nameof(CanSendPhotos)); }
    }

    public virtual bool CanSendPolls
    {
        get => _canSendPolls;
        set { OnPropertyChanging(nameof(CanSendPolls)); _canSendPolls = value; OnPropertyChanged(nameof(CanSendPolls)); }
    }

    public virtual bool CanSendVideoNotes
    {
        get => _canSendVideoNotes;
        set { OnPropertyChanging(nameof(CanSendVideoNotes)); _canSendVideoNotes = value; OnPropertyChanged(nameof(CanSendVideoNotes)); }
    }

    public virtual bool CanSendVideos
    {
        get => _canSendVideos;
        set { OnPropertyChanging(nameof(CanSendVideos)); _canSendVideos = value; OnPropertyChanged(nameof(CanSendVideos)); }
    }

    public virtual bool CanSendVoiceNotes
    {
        get => _canSendVoiceNotes;
        set { OnPropertyChanging(nameof(CanSendVoiceNotes)); _canSendVoiceNotes = value; OnPropertyChanged(nameof(CanSendVoiceNotes)); }
    }

    public virtual bool IsMember
    {
        get => _isMember;
        set { OnPropertyChanging(nameof(IsMember)); _isMember = value; OnPropertyChanged(nameof(IsMember)); }
    }

    public virtual ChatMemberStatus Status => ChatMemberStatus.Restricted;

    public virtual DateTime? UntilDate
    {
        get => _untilDate;
        set { OnPropertyChanging(nameof(UntilDate)); _untilDate = value; OnPropertyChanged(nameof(UntilDate)); }
    }
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberLeft : TelegramChatMember, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual ChatMemberStatus Status => ChatMemberStatus.Left;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberBanned : TelegramChatMember, INotifyPropertyChanging, INotifyPropertyChanged
{

    private DateTime? _untilDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual ChatMemberStatus Status => ChatMemberStatus.Kicked;

    public virtual DateTime? UntilDate
    {
        get => _untilDate;
        set { OnPropertyChanging(nameof(UntilDate)); _untilDate = value; OnPropertyChanged(nameof(UntilDate)); }
    }
}
