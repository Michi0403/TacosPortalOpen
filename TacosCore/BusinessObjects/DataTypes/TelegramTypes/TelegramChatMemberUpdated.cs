//-----------------------------------------------------------------------
// <copyright file="TelegramChatMemberUpdated.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatMemberUpdated : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChat _chat = null!;

    private Guid? _chatID;
    private DateTime _date;
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private TelegramChatInviteLink _inviteLink = null!;
    private Guid? _inviteLinkID;
    private TelegramChatMember? _newChatMember;
    private Guid? _newChatMemberID;
    private TelegramChatMember? _oldChatMember;
    private Guid? _oldChatMemberID;
    private bool _viaChatFolderInviteLink;
    private bool _viaJoinRequest;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.ChatMemberUpdatedThisChatBelongsTo))]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => _chatID;
        set { OnPropertyChanging(nameof(ChatID)); _chatID = value; OnPropertyChanged(nameof(ChatID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ChatMemberUpdatedThisUserBelongsTo))]
    public virtual TelegramUser From
    {
        get => _from;
        set { OnPropertyChanging(nameof(From)); _from = value; OnPropertyChanged(nameof(From)); }
    }

    [ForeignKey("From")]
    public virtual Guid? FromID
    {
        get => _fromID;
        set { OnPropertyChanging(nameof(FromID)); _fromID = value; OnPropertyChanged(nameof(FromID)); }
    }

    [InverseProperty(nameof(TelegramChatInviteLink.ChatMemberUpdatedThisChatInviteLinkBelongsTo))]
    public virtual TelegramChatInviteLink InviteLink
    {
        get => _inviteLink;
        set { OnPropertyChanging(nameof(InviteLink)); _inviteLink = value; OnPropertyChanged(nameof(InviteLink)); }
    }

    [ForeignKey("InviteLink")]
    public virtual Guid? InviteLinkID
    {
        get => _inviteLinkID;
        set { OnPropertyChanging(nameof(InviteLinkID)); _inviteLinkID = value; OnPropertyChanged(nameof(InviteLinkID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChatMember.NewChatMemberUpdatedThisChatMemberBelongsTo))]
    public virtual TelegramChatMember? NewChatMember
    {
        get => _newChatMember;
        set { OnPropertyChanging(nameof(NewChatMember)); _newChatMember = value; OnPropertyChanged(nameof(NewChatMember)); }
    }

    [ForeignKey("NewChatMember")]
    public virtual Guid? NewChatMemberID
    {
        get => _newChatMemberID;
        set { OnPropertyChanging(nameof(NewChatMemberID)); _newChatMemberID = value; OnPropertyChanged(nameof(NewChatMemberID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChatMember.OldChatMemberUpdatedThisChatMemberBelongsTo))]
    public virtual TelegramChatMember? OldChatMember
    {
        get => _oldChatMember;
        set { OnPropertyChanging(nameof(OldChatMember)); _oldChatMember = value; OnPropertyChanged(nameof(OldChatMember)); }
    }

    [ForeignKey("OldChatMember")]
    public virtual Guid? OldChatMemberID
    {
        get => _oldChatMemberID;
        set { OnPropertyChanging(nameof(OldChatMemberID)); _oldChatMemberID = value; OnPropertyChanged(nameof(OldChatMemberID)); }
    }

    public virtual bool ViaChatFolderInviteLink
    {
        get => _viaChatFolderInviteLink;
        set { OnPropertyChanging(nameof(ViaChatFolderInviteLink)); _viaChatFolderInviteLink = value; OnPropertyChanged(nameof(ViaChatFolderInviteLink)); }
    }

    public virtual bool ViaJoinRequest
    {
        get => _viaJoinRequest;
        set { OnPropertyChanging(nameof(ViaJoinRequest)); _viaJoinRequest = value; OnPropertyChanged(nameof(ViaJoinRequest)); }
    }
}
