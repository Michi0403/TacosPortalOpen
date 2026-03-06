//-----------------------------------------------------------------------
// <copyright file="TelegramUser.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
[Index(nameof(UserId), IsUnique = true)]
public partial class TelegramUser : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _addedToAttachmentMenu;
    private bool _canConnectToBusiness;
    private bool _canJoinGroups;
    private bool _canReadAllGroupMessages;
    private string _firstName = string.Empty;
    private bool _hasMainWebApp;
    private bool _isBot;
    private bool _isPremium;
    private string _languageCode = string.Empty;
    private string _lastName = string.Empty;
    private bool _supportsInlineQueries;


    private long _userId;
    private string _username = string.Empty;
    private ApplicationUser? applicationUserThisTelegramUserBelongsTo;
    private Guid? applicationUserThisTelegramUserBelongsToID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool AddedToAttachmentMenu
    {
        get => _addedToAttachmentMenu;
        set
        {
            if (_addedToAttachmentMenu != value)
            {
                OnPropertyChanging(nameof(AddedToAttachmentMenu));
                _addedToAttachmentMenu = value;
                OnPropertyChanged(nameof(AddedToAttachmentMenu));
            }
        }
    }

    [InverseProperty(nameof(ApplicationUser.TelegramUserThisApplicationUserBelongsTo))]
    public virtual ApplicationUser? ApplicationUserThisTelegramUserBelongsTo
    {
        get => applicationUserThisTelegramUserBelongsTo;
        set { OnPropertyChanging(nameof(ApplicationUserThisTelegramUserBelongsTo)); applicationUserThisTelegramUserBelongsTo = value; OnPropertyChanged(nameof(ApplicationUserThisTelegramUserBelongsTo)); }
    }
    [ForeignKey("ApplicationUserThisTelegramUserBelongsTo")]
    public virtual Guid? ApplicationUserThisTelegramUserBelongsToID
    {
        get => applicationUserThisTelegramUserBelongsToID;
        set { OnPropertyChanging(nameof(ApplicationUserThisTelegramUserBelongsToID)); applicationUserThisTelegramUserBelongsToID = value; OnPropertyChanged(nameof(ApplicationUserThisTelegramUserBelongsToID)); }
    }
    [InverseProperty(nameof(TelegramBusinessConnection.User))]
    [JsonIgnore]
    public virtual IList<TelegramBusinessConnection>? BusinessConnectionThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramBusinessConnection>();
    [InverseProperty(nameof(TelegramCallbackQuery.From))]
    [JsonIgnore]
    public virtual IList<TelegramCallbackQuery>? CallbackQueryThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramCallbackQuery>();

    public virtual bool CanConnectToBusiness
    {
        get => _canConnectToBusiness;
        set
        {
            if (_canConnectToBusiness != value)
            {
                OnPropertyChanging(nameof(CanConnectToBusiness));
                _canConnectToBusiness = value;
                OnPropertyChanged(nameof(CanConnectToBusiness));
            }
        }
    }

    public virtual bool CanJoinGroups
    {
        get => _canJoinGroups;
        set
        {
            if (_canJoinGroups != value)
            {
                OnPropertyChanging(nameof(CanJoinGroups));
                _canJoinGroups = value;
                OnPropertyChanged(nameof(CanJoinGroups));
            }
        }
    }

    public virtual bool CanReadAllGroupMessages
    {
        get => _canReadAllGroupMessages;
        set
        {
            if (_canReadAllGroupMessages != value)
            {
                OnPropertyChanging(nameof(CanReadAllGroupMessages));
                _canReadAllGroupMessages = value;
                OnPropertyChanged(nameof(CanReadAllGroupMessages));
            }
        }
    }
    [InverseProperty(nameof(TelegramChatBoostSourceGiftCode.User))]
    [JsonIgnore]
    public virtual IList<TelegramChatBoostSourceGiftCode>? ChatBoostSourceGiftCodeThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostSourceGiftCode>();
    [InverseProperty(nameof(TelegramChatBoostSourceGiveaway.User))]
    [JsonIgnore]
    public virtual IList<TelegramChatBoostSourceGiveaway>? ChatBoostSourceGiveawayThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostSourceGiveaway>();
    [InverseProperty(nameof(TelegramChatBoostSourcePremium.User))]
    [JsonIgnore]
    public virtual IList<TelegramChatBoostSourcePremium>? ChatBoostSourcePremiumThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostSourcePremium>();
    [InverseProperty(nameof(TelegramChatInviteLink.Creator))]
    [JsonIgnore]
    public virtual IList<TelegramChatInviteLink>? ChatInviteLinkThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatInviteLink>();
    [InverseProperty(nameof(TelegramChatJoinRequest.From))]
    [JsonIgnore]
    public virtual IList<TelegramChatJoinRequest>? ChatJoinRequestThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatJoinRequest>();
    [InverseProperty(nameof(TelegramChatMember.User))]
    [JsonIgnore]
    public virtual IList<TelegramChatMember>? ChatMemberThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatMember>();
    [InverseProperty(nameof(TelegramChatMemberUpdated.From))]
    [JsonIgnore]
    public virtual IList<TelegramChatMemberUpdated>? ChatMemberUpdatedThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatMemberUpdated>();
    [InverseProperty(nameof(TelegramChosenInlineResult.From))]
    [JsonIgnore]
    public virtual IList<TelegramChosenInlineResult>? ChosenInlineResultThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChosenInlineResult>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FirstName
    {
        get => _firstName;
        set
        {
            if (_firstName != value)
            {
                OnPropertyChanging(nameof(FirstName));
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.ForwardFrom))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? ForwardFromMessageUser { get; set; } = new ObservableCollection<TelegramMessage>();
    [InverseProperty(nameof(TelegramMessage.From))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? FromUser { get; set; } = new ObservableCollection<TelegramMessage>();
    [InverseProperty(nameof(TelegramGiveawayWinners.Winners))]
    [JsonIgnore]
    public virtual IList<TelegramGiveawayWinners>? GiveawayWinnersThisWinnerUsersBelongsTo { get; set; } = new ObservableCollection<TelegramGiveawayWinners>();

    public virtual bool HasMainWebApp
    {
        get => _hasMainWebApp;
        set
        {
            if (_hasMainWebApp != value)
            {
                OnPropertyChanging(nameof(HasMainWebApp));
                _hasMainWebApp = value;
                OnPropertyChanged(nameof(HasMainWebApp));
            }
        }
    }
    [InverseProperty(nameof(TelegramInlineQuery.From))]
    [JsonIgnore]
    public virtual IList<TelegramInlineQuery>? InlineQueryThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramInlineQuery>();

    public virtual bool IsBot
    {
        get => _isBot;
        set
        {
            if (_isBot != value)
            {
                OnPropertyChanging(nameof(IsBot));
                _isBot = value;
                OnPropertyChanged(nameof(IsBot));
            }
        }
    }

    public virtual bool IsPremium
    {
        get => _isPremium;
        set
        {
            if (_isPremium != value)
            {
                OnPropertyChanging(nameof(IsPremium));
                _isPremium = value;
                OnPropertyChanged(nameof(IsPremium));
            }
        }
    }

    public virtual string LanguageCode
    {
        get => _languageCode;
        set
        {
            if (_languageCode != value)
            {
                OnPropertyChanging(nameof(LanguageCode));
                _languageCode = value;
                OnPropertyChanged(nameof(LanguageCode));
            }
        }
    }

    public virtual string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                OnPropertyChanging(nameof(LastName));
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
    }
    [InverseProperty(nameof(TelegramMessageReactionUpdated.User))]
    [JsonIgnore]
    public virtual IList<TelegramMessageReactionUpdated>? MessageReactionUpdatedThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramMessageReactionUpdated>();
    [InverseProperty(nameof(TelegramPaidMediaPurchased.From))]
    [JsonIgnore]
    public virtual IList<TelegramPaidMediaPurchased>? PaidMediaPurchasedThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramPaidMediaPurchased>();
    [InverseProperty(nameof(TelegramPollAnswer.User))]
    [JsonIgnore]
    public virtual IList<TelegramPollAnswer>? PollAnswerThisVoterUsersBelongsTo { get; set; } = new ObservableCollection<TelegramPollAnswer>();
    [InverseProperty(nameof(TelegramPreCheckoutQuery.From))]

    public virtual IList<TelegramPreCheckoutQuery>? PreCheckoutQueryThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramPreCheckoutQuery>();
    [InverseProperty(nameof(TelegramProximityAlertTriggered.Traveler))]
    [JsonIgnore]
    public virtual IList<TelegramProximityAlertTriggered>? ProximityAlertTriggeredThisTravelerUserBelongsTo { get; set; } = new ObservableCollection<TelegramProximityAlertTriggered>();
    [InverseProperty(nameof(TelegramProximityAlertTriggered.Watcher))]
    [JsonIgnore]
    public virtual IList<TelegramProximityAlertTriggered>? ProximityAlertTriggeredThisWatcherUserBelongsTo { get; set; } = new ObservableCollection<TelegramProximityAlertTriggered>();
    [InverseProperty(nameof(TelegramShippingQuery.From))]
    [JsonIgnore]
    public virtual IList<TelegramShippingQuery>? ShippingQueryThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramShippingQuery>();

    public virtual bool SupportsInlineQueries
    {
        get => _supportsInlineQueries;
        set
        {
            if (_supportsInlineQueries != value)
            {
                OnPropertyChanging(nameof(SupportsInlineQueries));
                _supportsInlineQueries = value;
                OnPropertyChanged(nameof(SupportsInlineQueries));
            }
        }
    }

    [InverseProperty(nameof(TelegramChatBotRightsUser.BotUser))]
    [JsonIgnore]
    public virtual IList<TelegramChatBotRightsUser>? TelegramChatBotRightsUserThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramChatBotRightsUser>();
    [InverseProperty(nameof(TelegramUserChat.User))]
    [JsonIgnore]
    public virtual IList<TelegramUserChat>? UserChatsThisUserBelongsTo { get; set; } = new ObservableCollection<TelegramUserChat>();

    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [LongIntIDSanity]
    public virtual long UserId
    {
        get => _userId;
        set
        {
            if (_userId != value)
            {
                OnPropertyChanging(nameof(UserId));
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
    }
    [InverseProperty(nameof(TelegramMessage.LeftChatMember))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? UserLeftChatMember { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual string Username
    {
        get => _username;
        set
        {
            if (_username != value)
            {
                OnPropertyChanging(nameof(Username));
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
    }
    [InverseProperty(nameof(TelegramMessage.NewChatMembers))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? UserNewChatMembers { get; set; } = new ObservableCollection<TelegramMessage>();
    [InverseProperty(nameof(TelegramMessage.SenderBusinessBot))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? UserSenderBusinessBot { get; set; } = new ObservableCollection<TelegramMessage>();
    [InverseProperty(nameof(TelegramMessage.ViaBot))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? UserViaBotMessage { get; set; } = new ObservableCollection<TelegramMessage>();
}