//-----------------------------------------------------------------------
// <copyright file="TelegramChatFullInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatFullInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChat? _chat = null!;
    private Guid? _chatID;

    private int accentColorId;
    private TelegramAcceptedGiftTypes? acceptedGiftTypes;
    private Guid? acceptedGiftTypesID;

    private IList<TelegramReactionType>? availableReactions = new ObservableCollection<TelegramReactionType>();
    private string backgroundCustomEmojiId = string.Empty;
    private string bio = string.Empty;
    private TelegramBirthdate? birthdate;
    private Guid? birthdateID;
    private TelegramBusinessIntro? businessIntro;
    private Guid? businessIntroID;
    private TelegramBusinessLocation? businessLocation;
    private Guid? businessLocationID;
    private TelegramBusinessOpeningHours? businessOpeningHours;
    private Guid? businessOpeningHoursID;
    private bool canSendPaidMedia;
    private bool canSetStickerSet;
    private string customEmojiStickerSetName = string.Empty;
    private DateOnly dateCreated = DateOnly.FromDateTime(DateTime.Now);
    private string description = string.Empty;
    private string emojiStatusCustomEmojiId = string.Empty;
    private DateTime? emojiStatusExpirationDate;
    private bool hasAggressiveAntiSpamEnabled;
    private bool hasHiddenMembers;
    private bool hasPrivateForwards;
    private bool hasProtectedContent;
    private bool hasRestrictedVoiceAndVideoMessages;
    private bool hasVisibleHistory;
    private string inviteLink = string.Empty;
    private bool joinByRequest;
    private bool joinToSendMessages;
    private long? linkedChatId;
    private TelegramChatLocation? location;
    private Guid? locationID;
    private int maxReactionCount;
    private int? messageAutoDeleteTime;
    private TelegramChatPermissions? permissions;
    private Guid? permissionsID;
    private TelegramChat? personalChat;
    private Guid? personalChatID;
    private TelegramChatPhoto? photo;
    private Guid? photoID;
    private TelegramMessage? pinnedMessage;
    private Guid? pinnedMessageID;
    private int? profileAccentColorId;
    private string profileBackgroundCustomEmojiId = string.Empty;
    private int? slowModeDelay;
    private string stickerSetName = string.Empty;
    private int? unrestrictBoostCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int AccentColorId
    {
        get => accentColorId;
        set { OnPropertyChanging(nameof(AccentColorId)); accentColorId = value; OnPropertyChanged(nameof(AccentColorId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramAcceptedGiftTypes? AcceptedGiftTypes
    {
        get => acceptedGiftTypes;
        set { OnPropertyChanging(nameof(AcceptedGiftTypes)); acceptedGiftTypes = value; OnPropertyChanged(nameof(AcceptedGiftTypes)); }
    }

    [ForeignKey("AcceptedGiftTypes")]
    public virtual Guid? AcceptedGiftTypesID
    {
        get => acceptedGiftTypesID;
        set { OnPropertyChanging(nameof(AcceptedGiftTypesID)); acceptedGiftTypesID = value; OnPropertyChanged(nameof(AcceptedGiftTypesID)); }
    }

    public virtual IList<string> ActiveUsernames { get; set; } = new ObservableCollection<string>();

    public virtual IList<TelegramReactionType>? AvailableReactions
    {
        get => availableReactions;
        set { OnPropertyChanging(nameof(AvailableReactions)); availableReactions = value; OnPropertyChanged(nameof(AvailableReactions)); }
    }

    public virtual string BackgroundCustomEmojiId
    {
        get => backgroundCustomEmojiId;
        set { OnPropertyChanging(nameof(BackgroundCustomEmojiId)); backgroundCustomEmojiId = value; OnPropertyChanged(nameof(BackgroundCustomEmojiId)); }
    }

    public virtual string Bio
    {
        get => bio;
        set { OnPropertyChanging(nameof(Bio)); bio = value; OnPropertyChanged(nameof(Bio)); }
    }

    public virtual TelegramBirthdate? Birthdate
    {
        get => birthdate;
        set { OnPropertyChanging(nameof(Birthdate)); birthdate = value; OnPropertyChanged(nameof(Birthdate)); }
    }

    [ForeignKey("Birthdate")]
    public virtual Guid? BirthdateID
    {
        get => birthdateID;
        set { OnPropertyChanging(nameof(BirthdateID)); birthdateID = value; OnPropertyChanged(nameof(BirthdateID)); }
    }

    public virtual TelegramBusinessIntro? BusinessIntro
    {
        get => businessIntro;
        set { OnPropertyChanging(nameof(BusinessIntro)); businessIntro = value; OnPropertyChanged(nameof(BusinessIntro)); }
    }

    [ForeignKey("BusinessIntro")]
    public virtual Guid? BusinessIntroID
    {
        get => businessIntroID;
        set { OnPropertyChanging(nameof(BusinessIntroID)); businessIntroID = value; OnPropertyChanged(nameof(BusinessIntroID)); }
    }

    public virtual TelegramBusinessLocation? BusinessLocation
    {
        get => businessLocation;
        set { OnPropertyChanging(nameof(BusinessLocation)); businessLocation = value; OnPropertyChanged(nameof(BusinessLocation)); }
    }

    [ForeignKey("BusinessLocation")]
    public virtual Guid? BusinessLocationID
    {
        get => businessLocationID;
        set { OnPropertyChanging(nameof(BusinessLocationID)); businessLocationID = value; OnPropertyChanged(nameof(BusinessLocationID)); }
    }

    public virtual TelegramBusinessOpeningHours? BusinessOpeningHours
    {
        get => businessOpeningHours;
        set { OnPropertyChanging(nameof(BusinessOpeningHours)); businessOpeningHours = value; OnPropertyChanged(nameof(BusinessOpeningHours)); }
    }

    [ForeignKey("BusinessOpeningHours")]
    public virtual Guid? BusinessOpeningHoursID
    {
        get => businessOpeningHoursID;
        set { OnPropertyChanging(nameof(BusinessOpeningHoursID)); businessOpeningHoursID = value; OnPropertyChanged(nameof(BusinessOpeningHoursID)); }
    }

    public virtual bool CanSendPaidMedia
    {
        get => canSendPaidMedia;
        set { OnPropertyChanging(nameof(CanSendPaidMedia)); canSendPaidMedia = value; OnPropertyChanged(nameof(CanSendPaidMedia)); }
    }

    public virtual bool CanSetStickerSet
    {
        get => canSetStickerSet;
        set { OnPropertyChanging(nameof(CanSetStickerSet)); canSetStickerSet = value; OnPropertyChanged(nameof(CanSetStickerSet)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(Chat.ChatFullInfoThisChatBelongsTo))]
    public virtual TelegramChat? Chat
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

    public virtual string CustomEmojiStickerSetName
    {
        get => customEmojiStickerSetName;
        set { OnPropertyChanging(nameof(CustomEmojiStickerSetName)); customEmojiStickerSetName = value; OnPropertyChanged(nameof(CustomEmojiStickerSetName)); }
    }
    public virtual DateOnly DateCreated
    {
        get => dateCreated;
        set { OnPropertyChanging(nameof(DateCreated)); dateCreated = value; OnPropertyChanged(nameof(DateCreated)); }
    }

    public virtual string Description
    {
        get => description;
        set { OnPropertyChanging(nameof(Description)); description = value; OnPropertyChanged(nameof(Description)); }
    }

    public virtual string EmojiStatusCustomEmojiId
    {
        get => emojiStatusCustomEmojiId;
        set { OnPropertyChanging(nameof(EmojiStatusCustomEmojiId)); emojiStatusCustomEmojiId = value; OnPropertyChanged(nameof(EmojiStatusCustomEmojiId)); }
    }

    public virtual DateTime? EmojiStatusExpirationDate
    {
        get => emojiStatusExpirationDate;
        set { OnPropertyChanging(nameof(EmojiStatusExpirationDate)); emojiStatusExpirationDate = value; OnPropertyChanged(nameof(EmojiStatusExpirationDate)); }
    }

    public virtual bool HasAggressiveAntiSpamEnabled
    {
        get => hasAggressiveAntiSpamEnabled;
        set { OnPropertyChanging(nameof(HasAggressiveAntiSpamEnabled)); hasAggressiveAntiSpamEnabled = value; OnPropertyChanged(nameof(HasAggressiveAntiSpamEnabled)); }
    }

    public virtual bool HasHiddenMembers
    {
        get => hasHiddenMembers;
        set { OnPropertyChanging(nameof(HasHiddenMembers)); hasHiddenMembers = value; OnPropertyChanged(nameof(HasHiddenMembers)); }
    }

    public virtual bool HasPrivateForwards
    {
        get => hasPrivateForwards;
        set { OnPropertyChanging(nameof(HasPrivateForwards)); hasPrivateForwards = value; OnPropertyChanged(nameof(HasPrivateForwards)); }
    }

    public virtual bool HasProtectedContent
    {
        get => hasProtectedContent;
        set { OnPropertyChanging(nameof(HasProtectedContent)); hasProtectedContent = value; OnPropertyChanged(nameof(HasProtectedContent)); }
    }

    public virtual bool HasRestrictedVoiceAndVideoMessages
    {
        get => hasRestrictedVoiceAndVideoMessages;
        set { OnPropertyChanging(nameof(HasRestrictedVoiceAndVideoMessages)); hasRestrictedVoiceAndVideoMessages = value; OnPropertyChanged(nameof(HasRestrictedVoiceAndVideoMessages)); }
    }

    public virtual bool HasVisibleHistory
    {
        get => hasVisibleHistory;
        set { OnPropertyChanging(nameof(HasVisibleHistory)); hasVisibleHistory = value; OnPropertyChanged(nameof(HasVisibleHistory)); }
    }

    public virtual string InviteLink
    {
        get => inviteLink;
        set { OnPropertyChanging(nameof(InviteLink)); inviteLink = value; OnPropertyChanged(nameof(InviteLink)); }
    }

    public virtual bool JoinByRequest
    {
        get => joinByRequest;
        set { OnPropertyChanging(nameof(JoinByRequest)); joinByRequest = value; OnPropertyChanged(nameof(JoinByRequest)); }
    }

    public virtual bool JoinToSendMessages
    {
        get => joinToSendMessages;
        set { OnPropertyChanging(nameof(JoinToSendMessages)); joinToSendMessages = value; OnPropertyChanged(nameof(JoinToSendMessages)); }
    }

    public virtual long? LinkedChatId
    {
        get => linkedChatId;
        set { OnPropertyChanging(nameof(LinkedChatId)); linkedChatId = value; OnPropertyChanged(nameof(LinkedChatId)); }
    }

    public virtual TelegramChatLocation? Location
    {
        get => location;
        set { OnPropertyChanging(nameof(Location)); location = value; OnPropertyChanged(nameof(Location)); }
    }

    [ForeignKey("Location")]
    public virtual Guid? LocationID
    {
        get => locationID;
        set { OnPropertyChanging(nameof(LocationID)); locationID = value; OnPropertyChanged(nameof(LocationID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int MaxReactionCount
    {
        get => maxReactionCount;
        set { OnPropertyChanging(nameof(MaxReactionCount)); maxReactionCount = value; OnPropertyChanged(nameof(MaxReactionCount)); }
    }

    public virtual int? MessageAutoDeleteTime
    {
        get => messageAutoDeleteTime;
        set { OnPropertyChanging(nameof(MessageAutoDeleteTime)); messageAutoDeleteTime = value; OnPropertyChanged(nameof(MessageAutoDeleteTime)); }
    }

    public virtual TelegramChatPermissions? Permissions
    {
        get => permissions;
        set { OnPropertyChanging(nameof(Permissions)); permissions = value; OnPropertyChanged(nameof(Permissions)); }
    }

    [ForeignKey("Permissions")]
    public virtual Guid? PermissionsID
    {
        get => permissionsID;
        set { OnPropertyChanging(nameof(PermissionsID)); permissionsID = value; OnPropertyChanged(nameof(PermissionsID)); }
    }

    public virtual TelegramChat? PersonalChat
    {
        get => personalChat;
        set { OnPropertyChanging(nameof(PersonalChat)); personalChat = value; OnPropertyChanged(nameof(PersonalChat)); }
    }

    [ForeignKey("PersonalChat")]
    public virtual Guid? PersonalChatID
    {
        get => personalChatID;
        set { OnPropertyChanging(nameof(PersonalChatID)); personalChatID = value; OnPropertyChanged(nameof(PersonalChatID)); }
    }

    public virtual TelegramChatPhoto? Photo
    {
        get => photo;
        set { OnPropertyChanging(nameof(Photo)); photo = value; OnPropertyChanged(nameof(Photo)); }
    }

    [ForeignKey("Photo")]
    public virtual Guid? PhotoID
    {
        get => photoID;
        set { OnPropertyChanging(nameof(PhotoID)); photoID = value; OnPropertyChanged(nameof(PhotoID)); }
    }
    [InverseProperty(nameof(TelegramMessage.ChatFullInfoOfPinnedMessage))]
    public virtual TelegramMessage? PinnedMessage
    {
        get => pinnedMessage;
        set { OnPropertyChanging(nameof(PinnedMessage)); pinnedMessage = value; OnPropertyChanged(nameof(PinnedMessage)); }
    }

    [ForeignKey("PinnedMessage")]
    public virtual Guid? PinnedMessageID
    {
        get => pinnedMessageID;
        set { OnPropertyChanging(nameof(PinnedMessageID)); pinnedMessageID = value; OnPropertyChanged(nameof(PinnedMessageID)); }
    }
    public virtual int? ProfileAccentColorId
    {
        get => profileAccentColorId;
        set { OnPropertyChanging(nameof(ProfileAccentColorId)); profileAccentColorId = value; OnPropertyChanged(nameof(ProfileAccentColorId)); }
    }

    public virtual string ProfileBackgroundCustomEmojiId
    {
        get => profileBackgroundCustomEmojiId;
        set { OnPropertyChanging(nameof(ProfileBackgroundCustomEmojiId)); profileBackgroundCustomEmojiId = value; OnPropertyChanged(nameof(ProfileBackgroundCustomEmojiId)); }
    }

    public virtual int? SlowModeDelay
    {
        get => slowModeDelay;
        set { OnPropertyChanging(nameof(SlowModeDelay)); slowModeDelay = value; OnPropertyChanged(nameof(SlowModeDelay)); }
    }

    public virtual string StickerSetName
    {
        get => stickerSetName;
        set { OnPropertyChanging(nameof(StickerSetName)); stickerSetName = value; OnPropertyChanged(nameof(StickerSetName)); }
    }

    public virtual int? UnrestrictBoostCount
    {
        get => unrestrictBoostCount;
        set { OnPropertyChanging(nameof(UnrestrictBoostCount)); unrestrictBoostCount = value; OnPropertyChanged(nameof(UnrestrictBoostCount)); }
    }
}