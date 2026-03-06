//-----------------------------------------------------------------------
// <copyright file="TelegramChat.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.Attributes;
using Telegram.Bot.Types.Enums;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
[Index(nameof(ChatId), IsUnique = true)]
public partial class TelegramChat : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private long _chatId;
    private string? _firstName;
    private bool? _isForum;
    private string? _lastName;
    private string? _title;
    private ChatType _type;
    private string? _username;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramBusinessMessagesDeleted.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramBusinessMessagesDeleted>? BusinessMessagesDeletedThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramBusinessMessagesDeleted>();
    [InverseProperty(nameof(TelegramChatBoostRemoved.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramChatBoostRemoved>? ChatBoostRemovedThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostRemoved>();


    [InverseProperty(nameof(TelegramChatBoostUpdated.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramChatBoostUpdated>? ChatBoostUpdatedThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostUpdated>();
    [InverseProperty(nameof(TelegramChatFullInfo.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramChatFullInfo>? ChatFullInfoThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatFullInfo>();
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    [LongIntIDSanity]
    public virtual long ChatId
    {
        get => _chatId;
        set { OnPropertyChanging(nameof(ChatId)); _chatId = value; OnPropertyChanged(nameof(ChatId)); }
    }

    [InverseProperty(nameof(TelegramChatJoinRequest.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramChatJoinRequest>? ChatJoinRequestThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatJoinRequest>();

    [InverseProperty(nameof(TelegramChatMemberUpdated.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramChatMemberUpdated>? ChatMemberUpdatedThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatMemberUpdated>();
    public virtual bool? ChatToIgnore
    {
        get => _isForum;
        set { OnPropertyChanging(nameof(ChatToIgnore)); _isForum = value; OnPropertyChanged(nameof(ChatToIgnore)); }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    public virtual string? FirstName
    {
        get => _firstName;
        set { OnPropertyChanging(nameof(FirstName)); _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }

    [InverseProperty(nameof(TelegramGiveawayWinners.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramGiveawayWinners>? GiveawayGiveawayWinnersThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramGiveawayWinners>();

    [InverseProperty(nameof(TelegramGiveaway.Chats))]
    [JsonIgnore]
    public virtual IList<TelegramGiveaway>? GiveawayThisChatsBelongsTo { get; set; } = new ObservableCollection<TelegramGiveaway>();

    public virtual bool? IsForum
    {
        get => _isForum;
        set { OnPropertyChanging(nameof(IsForum)); _isForum = value; OnPropertyChanged(nameof(IsForum)); }
    }

    public virtual string? LastName
    {
        get => _lastName;
        set { OnPropertyChanging(nameof(LastName)); _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    [InverseProperty(nameof(TelegramMessageReactionCountUpdated.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramMessageReactionCountUpdated>? MessageReactionCountUpdatedThisActorelongsTo { get; set; } = new ObservableCollection<TelegramMessageReactionCountUpdated>();

    [InverseProperty(nameof(TelegramMessageReactionUpdated.ActorChat))]
    [JsonIgnore]
    public virtual IList<TelegramMessageReactionUpdated>? MessageReactionUpdatedThisActorChatBelongsTo { get; set; } = new ObservableCollection<TelegramMessageReactionUpdated>();

    [InverseProperty(nameof(TelegramMessageReactionUpdated.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramMessageReactionUpdated>? MessageReactionUpdatedThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramMessageReactionUpdated>();

    [InverseProperty(nameof(TelegramMessage.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesAsChat { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramMessage.SenderChat))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesAsSender { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramMessage.ForwardFromChat))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesForwardFromChat { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramPollAnswer.VoterChat))]
    [JsonIgnore]
    public virtual IList<TelegramPollAnswer>? PollAnswerThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramPollAnswer>();

    [InverseProperty(nameof(TelegramStory.Chat))]
    [JsonIgnore]
    public virtual IList<TelegramStory>? StoryThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramStory>();
    [InverseProperty(nameof(TacoTeamChat.TelegramChat))]
    public virtual IList<TacoTeamChat>? TacoTeamChatThisChatBelongsTo { get; set; } = new ObservableCollection<TacoTeamChat>();

    [InverseProperty(nameof(TelegramChatBotRightsUser.Chat))]
    public virtual IList<TelegramChatBotRightsUser>? TelegramChatBotRightsUserThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramChatBotRightsUser>();

    [InverseProperty(nameof(TelegramUserChat.ChatThisUserChatBelongsTo))]
    public virtual IList<TelegramUserChat>? TelegramUserChatsThisChatBelongsTo { get; set; } = new ObservableCollection<TelegramUserChat>();

    public virtual string? Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual ChatType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }

    public virtual string? Username
    {
        get => _username;
        set { OnPropertyChanging(nameof(Username)); _username = value; OnPropertyChanged(nameof(Username)); }
    }
}
