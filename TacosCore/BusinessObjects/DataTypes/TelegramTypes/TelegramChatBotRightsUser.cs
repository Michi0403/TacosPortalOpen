//-----------------------------------------------------------------------
// <copyright file="TelegramChatBotRightsUser.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatBotRightsUser : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUser? botUser = null!;

    private Guid? botUserID;
    private TelegramChat? chat = null!;
    private TelegramChatAdministratorRights? chatAdministratorRights = null!;

    private Guid? chatAdministratorRightsID;

    private Guid? chatID;

    private DateOnly dateCreated = DateOnly.FromDateTime(DateTime.Now);

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramUser.TelegramChatBotRightsUserThisUserBelongsTo))]
    [DeleteBehavior(DeleteBehavior.Restrict)]

    public virtual TelegramUser? BotUser
    {
        get => botUser;
        set { OnPropertyChanging(nameof(BotUser)); botUser = value; OnPropertyChanged(nameof(BotUser)); }
    }
    [ForeignKey("BotUser")]
    public virtual Guid? BotUserID
    {
        get => botUserID;
        set { OnPropertyChanging(nameof(BotUserID)); botUserID = value; OnPropertyChanged(nameof(BotUserID)); }
    }

    [InverseProperty(nameof(TelegramChat.TelegramChatBotRightsUserThisChatBelongsTo))]
    [DeleteBehavior(DeleteBehavior.Restrict)]

    public virtual TelegramChat? Chat
    {
        get => chat;
        set { OnPropertyChanging(nameof(Chat)); chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [InverseProperty(nameof(TelegramChatAdministratorRights.TelegramChatBotRightsUserThisTelegramChatAdministratorRightsBelongsTo))]

    public virtual TelegramChatAdministratorRights? ChatAdministratorRights
    {
        get => chatAdministratorRights;
        set { OnPropertyChanging(nameof(ChatAdministratorRights)); chatAdministratorRights = value; OnPropertyChanged(nameof(ChatAdministratorRights)); }
    }
    [ForeignKey("ChatAdministratorRights")]
    public virtual Guid? ChatAdministratorRightsID
    {
        get => chatAdministratorRightsID;
        set { OnPropertyChanging(nameof(ChatAdministratorRightsID)); chatAdministratorRightsID = value; OnPropertyChanged(nameof(ChatAdministratorRightsID)); }
    }
    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => chatID;
        set { OnPropertyChanging(nameof(ChatID)); chatID = value; OnPropertyChanged(nameof(ChatID)); }
    }
    public virtual DateOnly DateCreated
    {
        get => dateCreated;

    }
    public virtual DateOnly DateUpdated
    {
        get => dateCreated;
        set { OnPropertyChanging(nameof(DateCreated)); dateCreated = value; OnPropertyChanged(nameof(DateCreated)); }
    }

    [InverseProperty(nameof(TacoTeamChat.BotAssigned))]
    public virtual IList<TacoTeamChat>? TacoTeamsChatsThisBotUserRightsIsPartOf { get; set; } = new ObservableCollection<TacoTeamChat>();
}