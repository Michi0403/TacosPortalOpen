//-----------------------------------------------------------------------
// <copyright file="TelegramChatJoinRequest.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatJoinRequest : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _bio = string.Empty;
    private TelegramChat _chat = null!;

    private Guid? _chatID;
    private DateTime _date;
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private TelegramChatInviteLink _inviteLink = null!;
    private Guid? _inviteLinkID;
    private long _userChatId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string Bio
    {
        get => _bio;
        set { OnPropertyChanging(nameof(Bio)); _bio = value; OnPropertyChanged(nameof(Bio)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.ChatJoinRequestThisChatBelongsTo))]
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
    public required virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ChatJoinRequestThisUserBelongsTo))]
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

    [InverseProperty(nameof(TelegramChatInviteLink.ChatJoinRequestThisChatInviteLinkBelongsTo))]
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
    [LongIntIDSanity]
    public virtual long UserChatId
    {
        get => _userChatId;
        set { OnPropertyChanging(nameof(UserChatId)); _userChatId = value; OnPropertyChanged(nameof(UserChatId)); }
    }
}
