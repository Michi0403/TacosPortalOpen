//-----------------------------------------------------------------------
// <copyright file="TelegramMessageReactionUpdated.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageReactionUpdated : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChat _actorChat;
    private Guid? _actorChatId;
    private TelegramChat _chat;

    private Guid? _chatId;
    private DateTime _date;
    private int _telegramMessageReactionUpdatedMessageID;
    private TelegramUser _user;
    private Guid? _userId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(Chat.MessageReactionUpdatedThisActorChatBelongsTo))]
    public virtual TelegramChat ActorChat
    {
        get => _actorChat;
        set { OnPropertyChanging(nameof(ActorChat)); _actorChat = value; OnPropertyChanged(nameof(ActorChat)); }
    }
    [ForeignKey("ActorChat")]
    public virtual Guid? ActorChatID
    {
        get => _actorChatId;
        set { OnPropertyChanging(nameof(ActorChatID)); _actorChatId = value; OnPropertyChanged(nameof(ActorChatID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(Chat.MessageReactionUpdatedThisChatBelongsTo))]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => _chatId;
        set { OnPropertyChanging(nameof(ChatID)); _chatId = value; OnPropertyChanged(nameof(ChatID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int MessageIdFromReactionUpdate { get; set; }

    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramReactionType> NewReaction { get; set; } = new ObservableCollection<TelegramReactionType>();

    [InverseProperty(nameof(TelegramMessageReactionNewJoin.TelegramMessageReactionUpdated))]
    public virtual IList<TelegramMessageReactionNewJoin>? NewReactionLinks { get; set; } = new ObservableCollection<TelegramMessageReactionNewJoin>();

    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramReactionType> OldReaction { get; set; } = new ObservableCollection<TelegramReactionType>();

    [InverseProperty(nameof(TelegramMessageReactionOldJoin.TelegramMessageReactionUpdated))]
    public virtual IList<TelegramMessageReactionOldJoin>? OldReactionLinks { get; set; } = new ObservableCollection<TelegramMessageReactionOldJoin>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TelegramMessageReactionUpdatedMessageID
    {
        get => _telegramMessageReactionUpdatedMessageID;
        set { OnPropertyChanging(nameof(TelegramMessageReactionUpdatedMessageID)); _telegramMessageReactionUpdatedMessageID = value; OnPropertyChanged(nameof(TelegramMessageReactionUpdatedMessageID)); }
    }

    [InverseProperty(nameof(User.MessageReactionUpdatedThisUserBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }

    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => _userId;
        set { OnPropertyChanging(nameof(UserID)); _userId = value; OnPropertyChanged(nameof(UserID)); }
    }
}