using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramMessage : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public TelegramMessage()
        {
            Entities = new ObservableCollection<TelegramMessageEntity>();
            Photo = new ObservableCollection<TelegramPhotoSize>();
            NewChatMembers = new ObservableCollection<TelegramUser>();
            NewChatPhoto = new ObservableCollection<TelegramPhotoSize>();
            CaptionEntities = new ObservableCollection<TelegramMessageEntity>();
            CallbackQueryThisMessageBelongsTo = new ObservableCollection<TelegramCallbackQuery>();
            GiveAwayGiveawayCompletedThisMessageBelongsTo = new ObservableCollection<TelegramGiveawayCompleted>();
        }

        private int? message_ID;



        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Required]
        [LongIntIDSanity]
        public virtual int? Message_ID
        {
            get => message_ID;
            set { OnPropertyChanging(nameof(Message_ID)); message_ID = value; OnPropertyChanged(nameof(Message_ID)); }
        }

        private int? messageThreadId;

        public virtual int? MessageThreadId
        {
            get => messageThreadId;
            set { OnPropertyChanging(nameof(MessageThreadId)); messageThreadId = value; OnPropertyChanged(nameof(MessageThreadId)); }
        }
        private TelegramChatFullInfo? chatFullInfoOfPinnedMessage;

        [InverseProperty(nameof(TelegramChatFullInfo.PinnedMessage))]

        public virtual TelegramChatFullInfo? ChatFullInfoOfPinnedMessage
        {
            get => chatFullInfoOfPinnedMessage;
            set { OnPropertyChanging(nameof(ChatFullInfoOfPinnedMessage)); chatFullInfoOfPinnedMessage = value; OnPropertyChanged(nameof(ChatFullInfoOfPinnedMessage)); }
        }
        private TelegramUser? from;

        [InverseProperty(nameof(TelegramUser.FromUser))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramUser? From
        {
            get => from;
            set { OnPropertyChanging(nameof(From)); from = value; OnPropertyChanged(nameof(From)); }
        }
        private TelegramUser? forwardFrom;

        [InverseProperty(nameof(TelegramUser.ForwardFromMessageUser))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramUser? ForwardFrom
        {
            get => forwardFrom;
            set { OnPropertyChanging(nameof(ForwardFrom)); forwardFrom = value; OnPropertyChanged(nameof(ForwardFrom)); }
        }



        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual int? ForwardFromMessageId { get; set; }




        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual string? ForwardSignature { get; set; }





        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual string? ForwardSenderName { get; set; }




        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual DateTime? ForwardDate { get; set; }
        private Guid? userFromID;
        [ForeignKey("From")]
        public virtual Guid? UserFromID
        {
            get => userFromID;
            set { OnPropertyChanging(nameof(UserFromID)); userFromID = value; OnPropertyChanged(nameof(UserFromID)); }
        }

        private TelegramChat? senderChat;

        [InverseProperty(nameof(TelegramChat.MessagesAsSender))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramChat? SenderChat
        {
            get => senderChat;
            set { OnPropertyChanging(nameof(SenderChat)); senderChat = value; OnPropertyChanged(nameof(SenderChat)); }
        }

        private Guid? senderChatID;
        [ForeignKey("SenderChat")]
        public virtual Guid? SenderChatID
        {
            get => senderChatID;
            set { OnPropertyChanging(nameof(SenderChatID)); senderChatID = value; OnPropertyChanged(nameof(SenderChatID)); }
        }

        private int? senderBoostCount;

        public virtual int? SenderBoostCount
        {
            get => senderBoostCount;
            set { OnPropertyChanging(nameof(SenderBoostCount)); senderBoostCount = value; OnPropertyChanged(nameof(SenderBoostCount)); }
        }

        private TelegramUser? senderBusinessBot;

        [InverseProperty(nameof(TelegramUser.UserSenderBusinessBot))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramUser? SenderBusinessBot
        {
            get => senderBusinessBot;
            set { OnPropertyChanging(nameof(SenderBusinessBot)); senderBusinessBot = value; OnPropertyChanged(nameof(SenderBusinessBot)); }
        }

        private Guid? senderBusinessBotID;
        [ForeignKey("SenderBusinessBot")]
        public virtual Guid? SenderBusinessBotID
        {
            get => senderBusinessBotID;
            set { OnPropertyChanging(nameof(SenderBusinessBotID)); senderBusinessBotID = value; OnPropertyChanged(nameof(SenderBusinessBotID)); }
        }

        private DateTime? date;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual DateTime? Date
        {
            get => date;
            set { OnPropertyChanging(nameof(Date)); date = value; OnPropertyChanged(nameof(Date)); }
        }

        private string? businessConnectionId;

        public virtual string? BusinessConnectionId
        {
            get => businessConnectionId;
            set { OnPropertyChanging(nameof(BusinessConnectionId)); businessConnectionId = value; OnPropertyChanged(nameof(BusinessConnectionId)); }
        }

        private TelegramChat? chat;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [InverseProperty(nameof(TelegramChat.MessagesAsChat))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramChat? Chat
        {
            get => chat;
            set { OnPropertyChanging(nameof(Chat)); chat = value; OnPropertyChanged(nameof(Chat)); }
        }
        private TelegramChat? forwardFromChat;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [InverseProperty(nameof(TelegramChat.MessagesForwardFromChat))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramChat? ForwardFromChat
        {
            get => forwardFromChat;
            set { OnPropertyChanging(nameof(ForwardFromChat)); forwardFromChat = value; OnPropertyChanged(nameof(ForwardFromChat)); }
        }

        private Guid? chatID;
        [ForeignKey("TelegramChat")]
        public virtual Guid? ChatID
        {
            get => chatID;
            set { OnPropertyChanging(nameof(ChatID)); chatID = value; OnPropertyChanged(nameof(ChatID)); }
        }

        private TelegramMessageOrigin? forwardOrigin;

        [InverseProperty(nameof(TelegramMessageOrigin.MessageOriginBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual TelegramMessageOrigin? ForwardOrigin
        {
            get => forwardOrigin;
            set { OnPropertyChanging(nameof(ForwardOrigin)); forwardOrigin = value; OnPropertyChanged(nameof(ForwardOrigin)); }
        }

        private Guid? forwardOriginID;
        [ForeignKey("ForwardOrigin")]
        public virtual Guid? ForwardOriginID
        {
            get => forwardOriginID;
            set { OnPropertyChanging(nameof(ForwardOriginID)); forwardOriginID = value; OnPropertyChanged(nameof(ForwardOriginID)); }
        }

        private bool? isTopicMessage;

        public virtual bool? IsTopicMessage
        {
            get => isTopicMessage;
            set { OnPropertyChanging(nameof(IsTopicMessage)); isTopicMessage = value; OnPropertyChanged(nameof(IsTopicMessage)); }
        }

        private bool? isAutomaticForward;

        public virtual bool? IsAutomaticForward
        {
            get => isAutomaticForward;
            set { OnPropertyChanging(nameof(IsAutomaticForward)); isAutomaticForward = value; OnPropertyChanged(nameof(IsAutomaticForward)); }
        }

        private TelegramMessage? replyToMessage;

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramMessage? ReplyToMessage
        {
            get => replyToMessage;
            set { OnPropertyChanging(nameof(ReplyToMessage)); replyToMessage = value; OnPropertyChanged(nameof(ReplyToMessage)); }
        }

        private Guid? replyToMessageID;
        [ForeignKey("ReplyToMessage")]
        public virtual Guid? ReplyToMessageID
        {
            get => replyToMessageID;
            set { OnPropertyChanging(nameof(ReplyToMessageID)); replyToMessageID = value; OnPropertyChanged(nameof(ReplyToMessageID)); }
        }

        private TelegramExternalReplyInfo? externalReply;

        [InverseProperty(nameof(TelegramExternalReplyInfo.MessageThisExternalReplyInfoBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramExternalReplyInfo? ExternalReply
        {
            get => externalReply;
            set { OnPropertyChanging(nameof(ExternalReply)); externalReply = value; OnPropertyChanged(nameof(ExternalReply)); }
        }

        private Guid? externalReplyID;
        [ForeignKey("ExternalReply")]
        public virtual Guid? ExternalReplyID
        {
            get => externalReplyID;
            set { OnPropertyChanging(nameof(ExternalReplyID)); externalReplyID = value; OnPropertyChanged(nameof(ExternalReplyID)); }
        }

        private TelegramTextQuote? quote;

        [InverseProperty(nameof(TelegramTextQuote.MessageThisTextquoteBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramTextQuote? Quote
        {
            get => quote;
            set { OnPropertyChanging(nameof(Quote)); quote = value; OnPropertyChanged(nameof(Quote)); }
        }

        private Guid? textQuoteID;
        [ForeignKey("TelegramTextQuote")]
        public virtual Guid? TextQuoteID
        {
            get => textQuoteID;
            set { OnPropertyChanging(nameof(TextQuoteID)); textQuoteID = value; OnPropertyChanged(nameof(TextQuoteID)); }
        }

        private TelegramStory? replyToStory;

        [InverseProperty(nameof(TelegramStory.MessagesStoryRepliedTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramStory? ReplyToStory
        {
            get => replyToStory;
            set { OnPropertyChanging(nameof(ReplyToStory)); replyToStory = value; OnPropertyChanged(nameof(ReplyToStory)); }
        }

        private Guid? replyToStoryID;
        [ForeignKey("ReplyToStory")]
        public virtual Guid? ReplyToStoryID
        {
            get => replyToStoryID;
            set { OnPropertyChanging(nameof(ReplyToStoryID)); replyToStoryID = value; OnPropertyChanged(nameof(ReplyToStoryID)); }
        }

        private TelegramUser? viaBot;

        [InverseProperty(nameof(TelegramUser.UserViaBotMessage))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramUser? ViaBot
        {
            get => viaBot;
            set { OnPropertyChanging(nameof(ViaBot)); viaBot = value; OnPropertyChanged(nameof(ViaBot)); }
        }

        private Guid? viaBotID;
        [ForeignKey("ViaBot")]
        public virtual Guid? ViaBotID
        {
            get => viaBotID;
            set { OnPropertyChanging(nameof(ViaBotID)); viaBotID = value; OnPropertyChanged(nameof(ViaBotID)); }
        }

        private DateTime? editDate;

        public virtual DateTime? EditDate
        {
            get => editDate;
            set { OnPropertyChanging(nameof(EditDate)); editDate = value; OnPropertyChanged(nameof(EditDate)); }
        }


        private bool? hasProtectedContent;

        public virtual bool? HasProtectedContent
        {
            get => hasProtectedContent;
            set { OnPropertyChanging(nameof(HasProtectedContent)); hasProtectedContent = value; OnPropertyChanged(nameof(HasProtectedContent)); }
        }

        private bool? isFromOffline;

        public virtual bool? IsFromOffline
        {
            get => isFromOffline;
            set { OnPropertyChanging(nameof(IsFromOffline)); isFromOffline = value; OnPropertyChanged(nameof(IsFromOffline)); }
        }

        private string? mediaGroupId;

        public virtual string? MediaGroupId
        {
            get => mediaGroupId;
            set { OnPropertyChanging(nameof(MediaGroupId)); mediaGroupId = value; OnPropertyChanged(nameof(MediaGroupId)); }
        }

        private string? authorSignature;

        public virtual string? AuthorSignature
        {
            get => authorSignature;
            set { OnPropertyChanging(nameof(AuthorSignature)); authorSignature = value; OnPropertyChanged(nameof(AuthorSignature)); }
        }

        private long? paidStarCount;

        public virtual long? PaidStarCount
        {
            get => paidStarCount;
            set { OnPropertyChanging(nameof(PaidStarCount)); paidStarCount = value; OnPropertyChanged(nameof(PaidStarCount)); }
        }

        private string? text;

        public virtual string? Text
        {
            get => text;
            set { OnPropertyChanging(nameof(Text)); text = value; OnPropertyChanged(nameof(Text)); }
        }

        [InverseProperty(nameof(TelegramMessageEntity.MessageThisMessageEntitiesBelongingTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual IList<TelegramMessageEntity>? Entities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

        private TelegramLinkPreviewOptions? linkPreviewOptions;

        [InverseProperty(nameof(TelegramLinkPreviewOptions.MessageThisLinkPreviewOptionBelongsTo))]
        [ForeignKey("LinkPreviewOptionsLinkPreviewOptions")]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramLinkPreviewOptions? LinkPreviewOptions
        {
            get => linkPreviewOptions;
            set { OnPropertyChanging(nameof(LinkPreviewOptions)); linkPreviewOptions = value; OnPropertyChanged(nameof(LinkPreviewOptions)); }
        }

        private string? effectId;

        public virtual string? EffectId
        {
            get => effectId;
            set { OnPropertyChanging(nameof(EffectId)); effectId = value; OnPropertyChanged(nameof(EffectId)); }
        }

        private TelegramAnimation? animation;

        [InverseProperty(nameof(TelegramAnimation.MessageAnimationBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramAnimation? Animation
        {
            get => animation;
            set { OnPropertyChanging(nameof(Animation)); animation = value; OnPropertyChanged(nameof(Animation)); }
        }

        private Guid? animationID;
        [ForeignKey("TelegramAnimation")]
        public virtual Guid? AnimationID
        {
            get => animationID;
            set { OnPropertyChanging(nameof(AnimationID)); animationID = value; OnPropertyChanged(nameof(AnimationID)); }
        }
        private TelegramAudio? audio;
        [InverseProperty(nameof(TelegramAudio.MessageThisAudioBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramAudio? Audio
        {
            get => audio;
            set { OnPropertyChanging(nameof(Audio)); audio = value; OnPropertyChanged(nameof(Audio)); }
        }

        private Guid? audioID;
        [ForeignKey("TelegramAudio")]
        public virtual Guid? AudioID
        {
            get => audioID;
            set { OnPropertyChanging(nameof(AudioID)); audioID = value; OnPropertyChanged(nameof(AudioID)); }
        }

        private TelegramDocument? document;
        [InverseProperty(nameof(TelegramDocument.MessageThisDocumentBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramDocument? Document
        {
            get => document;
            set { OnPropertyChanging(nameof(Document)); document = value; OnPropertyChanged(nameof(Document)); }
        }

        private Guid? documentID;
        [ForeignKey("TelegramDocument")]
        public virtual Guid? DocumentID
        {
            get => documentID;
            set { OnPropertyChanging(nameof(DocumentID)); documentID = value; OnPropertyChanged(nameof(DocumentID)); }
        }

        private TelegramPaidMediaInfo? paidMedia;
        [InverseProperty(nameof(TelegramPaidMediaInfo.MessageThisPaidMediaInfoBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramPaidMediaInfo? PaidMedia
        {
            get => paidMedia;
            set { OnPropertyChanging(nameof(PaidMedia)); paidMedia = value; OnPropertyChanged(nameof(PaidMedia)); }
        }

        private Guid? paidMediaID;
        [ForeignKey("TelegramPaidMedia")]
        public virtual Guid? PaidMediaID
        {
            get => paidMediaID;
            set { OnPropertyChanging(nameof(PaidMediaID)); paidMediaID = value; OnPropertyChanged(nameof(PaidMediaID)); }
        }

        [InverseProperty(nameof(TelegramPhotoSize.MessagesAsPhoto))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual IList<TelegramPhotoSize>? Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();

        private TelegramSticker? sticker;
        [InverseProperty(nameof(TelegramSticker.MessageThisStickerBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramSticker? Sticker
        {
            get => sticker;
            set { OnPropertyChanging(nameof(Sticker)); sticker = value; OnPropertyChanged(nameof(Sticker)); }
        }

        private Guid? stickerID;
        [ForeignKey("TelegramSticker")]
        public virtual Guid? StickerID
        {
            get => stickerID;
            set { OnPropertyChanging(nameof(StickerID)); stickerID = value; OnPropertyChanged(nameof(StickerID)); }
        }

        private TelegramStory? story;
        [InverseProperty(nameof(TelegramStory.MessageThisStoryBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramStory? Story
        {
            get => story;
            set { OnPropertyChanging(nameof(Story)); story = value; OnPropertyChanged(nameof(Story)); }
        }

        private Guid? storyID;
        [ForeignKey("TelegramStory")]
        public virtual Guid? StoryID
        {
            get => storyID;
            set { OnPropertyChanging(nameof(StoryID)); storyID = value; OnPropertyChanged(nameof(StoryID)); }
        }

        private TelegramVideo? video;
        [InverseProperty(nameof(TelegramVideo.MessageThisVideoBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramVideo? Video
        {
            get => video;
            set { OnPropertyChanging(nameof(Video)); video = value; OnPropertyChanged(nameof(Video)); }
        }

        private Guid? videoID;
        [ForeignKey("TelegramVideo")]
        public virtual Guid? VideoID
        {
            get => videoID;
            set { OnPropertyChanging(nameof(VideoID)); videoID = value; OnPropertyChanged(nameof(VideoID)); }
        }

        private TelegramVideoNote? videoNote;
        [InverseProperty(nameof(TelegramVideoNote.MessageThisVideoNoteBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramVideoNote? VideoNote
        {
            get => videoNote;
            set { OnPropertyChanging(nameof(VideoNote)); videoNote = value; OnPropertyChanged(nameof(VideoNote)); }
        }

        private Guid? videoNoteID;
        [ForeignKey("TelegramVideoNote")]
        public virtual Guid? VideoNoteID
        {
            get => videoNoteID;
            set { OnPropertyChanging(nameof(VideoNoteID)); videoNoteID = value; OnPropertyChanged(nameof(VideoNoteID)); }
        }

        private TelegramVoice? voice;
        [InverseProperty(nameof(TelegramVoice.MessageThisVoiceBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramVoice? Voice
        {
            get => voice;
            set { OnPropertyChanging(nameof(Voice)); voice = value; OnPropertyChanged(nameof(Voice)); }
        }

        private Guid? voiceID;
        [ForeignKey("TelegramVoice")]
        public virtual Guid? VoiceID
        {
            get => voiceID;
            set { OnPropertyChanging(nameof(VoiceID)); voiceID = value; OnPropertyChanged(nameof(VoiceID)); }
        }

        private string? caption;

        public virtual string? Caption
        {
            get => caption;
            set { OnPropertyChanging(nameof(Caption)); caption = value; OnPropertyChanged(nameof(Caption)); }
        }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        [InverseProperty(nameof(TelegramMessageEntity.MessageThisCaptionEntitiesBelongingTo))]

        public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; }


        private bool? showCaptionAboveMedia;

        public virtual bool? ShowCaptionAboveMedia
        {
            get => showCaptionAboveMedia;
            set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
        }

        private bool? hasMediaSpoiler;

        public virtual bool? HasMediaSpoiler
        {
            get => hasMediaSpoiler;
            set { OnPropertyChanging(nameof(HasMediaSpoiler)); hasMediaSpoiler = value; OnPropertyChanged(nameof(HasMediaSpoiler)); }
        }

        private TelegramContact? contact;
        [InverseProperty(nameof(TelegramContact.MessageThisContactBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramContact? Contact
        {
            get => contact;
            set { OnPropertyChanging(nameof(Contact)); contact = value; OnPropertyChanged(nameof(Contact)); }
        }

        private Guid? contactID;
        [ForeignKey("TelegramContact")]
        public virtual Guid? ContactID
        {
            get => contactID;
            set { OnPropertyChanging(nameof(ContactID)); contactID = value; OnPropertyChanged(nameof(ContactID)); }
        }

        private TelegramDice? dice;
        [InverseProperty(nameof(TelegramDice.MessageThisDiceBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]

        public virtual TelegramDice? Dice
        {
            get => dice;
            set { OnPropertyChanging(nameof(Dice)); dice = value; OnPropertyChanged(nameof(Dice)); }
        }

        private Guid? diceID;
        [ForeignKey("TelegramDice")]
        public virtual Guid? DiceID
        {
            get => diceID;
            set { OnPropertyChanging(nameof(DiceID)); diceID = value; OnPropertyChanged(nameof(DiceID)); }
        }
        private TelegramGame? game;
        [InverseProperty(nameof(TelegramGame.MessageThisGameBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGame? Game
        {
            get => game;
            set { OnPropertyChanging(nameof(Game)); game = value; OnPropertyChanged(nameof(Game)); }
        }

        private Guid? gameID;
        [ForeignKey("TelegramGame")]
        public virtual Guid? GameID
        {
            get => gameID;
            set { OnPropertyChanging(nameof(GameID)); gameID = value; OnPropertyChanged(nameof(GameID)); }
        }

        private TelegramPoll? poll;
        [InverseProperty(nameof(TelegramPoll.MessageThisPollBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramPoll? Poll
        {
            get => poll;
            set { OnPropertyChanging(nameof(Poll)); poll = value; OnPropertyChanged(nameof(Poll)); }
        }

        private Guid? telegramMessagePollID;
        [ForeignKey("TelegramPoll")]
        public virtual Guid? TelegramMessagePollID
        {
            get => telegramMessagePollID;
            set { OnPropertyChanging(nameof(TelegramMessagePollID)); telegramMessagePollID = value; OnPropertyChanged(nameof(TelegramMessagePollID)); }
        }

        private TelegramVenue? venue;
        [InverseProperty(nameof(TelegramVenue.MessageThisVenueBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramVenue? Venue
        {
            get => venue;
            set { OnPropertyChanging(nameof(Venue)); venue = value; OnPropertyChanged(nameof(Venue)); }
        }

        private Guid? venueID;
        [ForeignKey("TelegramVenue")]
        public virtual Guid? VenueID
        {
            get => venueID;
            set { OnPropertyChanging(nameof(VenueID)); venueID = value; OnPropertyChanged(nameof(VenueID)); }
        }

        private TelegramLocation? location;
        [InverseProperty(nameof(TelegramLocation.MessageThisLocationBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramLocation? Location
        {
            get => location;
            set { OnPropertyChanging(nameof(Location)); location = value; OnPropertyChanged(nameof(Location)); }
        }

        private Guid? locationID;
        [ForeignKey("TelegramLocation")]
        public virtual Guid? LocationID
        {
            get => locationID;
            set { OnPropertyChanging(nameof(LocationID)); locationID = value; OnPropertyChanged(nameof(LocationID)); }
        }

        [InverseProperty(nameof(TelegramUser.UserNewChatMembers))]
        public virtual IList<TelegramUser>? NewChatMembers { get; set; } = new ObservableCollection<TelegramUser>();

        private TelegramUser? leftChatMember;
        [DeleteBehavior(DeleteBehavior.Restrict)]
        [InverseProperty(nameof(TelegramUser.UserLeftChatMember))]
        public virtual TelegramUser? LeftChatMember
        {
            get => leftChatMember;
            set { OnPropertyChanging(nameof(LeftChatMember)); leftChatMember = value; OnPropertyChanged(nameof(LeftChatMember)); }
        }

        private Guid? leftChatMemberID;
        [ForeignKey("LeftChatMember")]
        public virtual Guid? LeftChatMemberID
        {
            get => leftChatMemberID;
            set { OnPropertyChanging(nameof(LeftChatMemberID)); leftChatMemberID = value; OnPropertyChanged(nameof(LeftChatMemberID)); }
        }

        private string? newChatTitle;
        public virtual string? NewChatTitle
        {
            get => newChatTitle;
            set { OnPropertyChanging(nameof(NewChatTitle)); newChatTitle = value; OnPropertyChanged(nameof(NewChatTitle)); }
        }

        [InverseProperty(nameof(TelegramPhotoSize.MessagesAsNewChatPhoto))]
        public virtual IList<TelegramPhotoSize>? NewChatPhoto { get; set; } = new ObservableCollection<TelegramPhotoSize>();

        private bool? _deleteChatPhoto;
        public virtual bool? DeleteChatPhoto
        {
            get => _deleteChatPhoto;
            set { OnPropertyChanging(nameof(DeleteChatPhoto)); _deleteChatPhoto = value; OnPropertyChanged(nameof(DeleteChatPhoto)); }
        }

        private bool? groupChatCreated;
        public virtual bool? GroupChatCreated
        {
            get => groupChatCreated;
            set { OnPropertyChanging(nameof(GroupChatCreated)); groupChatCreated = value; OnPropertyChanged(nameof(GroupChatCreated)); }
        }

        private bool? supergroupChatCreated;
        public virtual bool? SupergroupChatCreated
        {
            get => supergroupChatCreated;
            set { OnPropertyChanging(nameof(SupergroupChatCreated)); supergroupChatCreated = value; OnPropertyChanged(nameof(SupergroupChatCreated)); }
        }

        private bool? channelChatCreated;
        public virtual bool? ChannelChatCreated
        {
            get => channelChatCreated;
            set { OnPropertyChanging(nameof(ChannelChatCreated)); channelChatCreated = value; OnPropertyChanged(nameof(ChannelChatCreated)); }
        }

        private int? messageAutoDeleteTimerChanged;
        public virtual int? MessageAutoDeleteTimerChanged
        {
            get => messageAutoDeleteTimerChanged;
            set { OnPropertyChanging(nameof(MessageAutoDeleteTimerChanged)); messageAutoDeleteTimerChanged = value; OnPropertyChanged(nameof(MessageAutoDeleteTimerChanged)); }
        }

        private long? migrateToChatId;
        public virtual long? MigrateToChatId
        {
            get => migrateToChatId;
            set { OnPropertyChanging(nameof(MigrateToChatId)); migrateToChatId = value; OnPropertyChanged(nameof(MigrateToChatId)); }
        }

        private long? migrateFromChatId;
        public virtual long? MigrateFromChatId
        {
            get => migrateFromChatId;
            set { OnPropertyChanging(nameof(MigrateFromChatId)); migrateFromChatId = value; OnPropertyChanged(nameof(MigrateFromChatId)); }
        }

        private TelegramMessage? pinnedMessage;
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramMessage? PinnedMessage
        {
            get => pinnedMessage;
            set { OnPropertyChanging(nameof(PinnedMessage)); pinnedMessage = value; OnPropertyChanged(nameof(PinnedMessage)); }
        }

        private Guid? pinnedMessageID;
        [ForeignKey("PinnedMessage")]
        public virtual Guid? PinnedMessageID
        {
            get => pinnedMessageID;
            set { OnPropertyChanging(nameof(PinnedMessageID)); pinnedMessageID = value; OnPropertyChanged(nameof(PinnedMessageID)); }
        }

        private TelegramInvoice? invoice;
        [InverseProperty(nameof(TelegramInvoice.MessageThisInvoiceBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramInvoice? Invoice
        {
            get => invoice;
            set { OnPropertyChanging(nameof(Invoice)); invoice = value; OnPropertyChanged(nameof(Invoice)); }
        }

        private Guid? invoiceID;
        [ForeignKey("TelegramInvoice")]
        public virtual Guid? InvoiceID
        {
            get => invoiceID;
            set { OnPropertyChanging(nameof(InvoiceID)); invoiceID = value; OnPropertyChanged(nameof(InvoiceID)); }
        }

        private TelegramSuccessfulPayment? successfulPayment;
        [InverseProperty(nameof(TelegramSuccessfulPayment.MessageThisSuccessfulPaymentBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramSuccessfulPayment? SuccessfulPayment
        {
            get => successfulPayment;
            set { OnPropertyChanging(nameof(SuccessfulPayment)); successfulPayment = value; OnPropertyChanged(nameof(SuccessfulPayment)); }
        }

        private Guid? successfulPaymentID;
        [ForeignKey("TelegramSuccessfulPayment")]
        public virtual Guid? SuccessfulPaymentID
        {
            get => successfulPaymentID;
            set { OnPropertyChanging(nameof(SuccessfulPaymentID)); successfulPaymentID = value; OnPropertyChanged(nameof(SuccessfulPaymentID)); }
        }

        private TelegramRefundedPayment? refundedPayment;
        [InverseProperty(nameof(TelegramRefundedPayment.MessageThisRefundedPaymentBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramRefundedPayment? RefundedPayment
        {
            get => refundedPayment;
            set { OnPropertyChanging(nameof(RefundedPayment)); refundedPayment = value; OnPropertyChanged(nameof(RefundedPayment)); }
        }

        private Guid? refundedPaymentID;
        [ForeignKey("TelegramRefundedPayment")]
        public virtual Guid? RefundedPaymentID
        {
            get => refundedPaymentID;
            set { OnPropertyChanging(nameof(RefundedPaymentID)); refundedPaymentID = value; OnPropertyChanged(nameof(RefundedPaymentID)); }
        }
        private TelegramUsersShared? usersShared;
        [InverseProperty(nameof(TelegramUsersShared.MessageThisUsersSharedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramUsersShared? UsersShared
        {
            get => usersShared;
            set { OnPropertyChanging(nameof(UsersShared)); usersShared = value; OnPropertyChanged(nameof(UsersShared)); }
        }

        private Guid? usersSharedID;
        [ForeignKey("TelegramUsersShared")]
        public virtual Guid? UsersSharedID
        {
            get => usersSharedID;
            set { OnPropertyChanging(nameof(UsersSharedID)); usersSharedID = value; OnPropertyChanged(nameof(UsersSharedID)); }
        }

        private TelegramChatShared? chatShared;
        [InverseProperty(nameof(TelegramChatShared.MessageThisChatSharedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramChatShared? ChatShared
        {
            get => chatShared;
            set { OnPropertyChanging(nameof(ChatShared)); chatShared = value; OnPropertyChanged(nameof(ChatShared)); }
        }

        private Guid? chatSharedID;
        [ForeignKey("TelegramChatShared")]
        public virtual Guid? ChatSharedID
        {
            get => chatSharedID;
            set { OnPropertyChanging(nameof(ChatSharedID)); chatSharedID = value; OnPropertyChanged(nameof(ChatSharedID)); }
        }

        private TelegramGiftInfo? gift;
        [InverseProperty(nameof(TelegramGiftInfo.MessageThisGiftinfoBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGiftInfo? Gift
        {
            get => gift;
            set { OnPropertyChanging(nameof(Gift)); gift = value; OnPropertyChanged(nameof(Gift)); }
        }

        private Guid? giftID;
        [ForeignKey("TelegramGift")]
        public virtual Guid? GiftID
        {
            get => giftID;
            set { OnPropertyChanging(nameof(GiftID)); giftID = value; OnPropertyChanged(nameof(GiftID)); }
        }

        private TelegramUniqueGiftInfo? uniqueGift;
        [InverseProperty(nameof(TelegramUniqueGiftInfo.MessageUniqueGiftInfoBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramUniqueGiftInfo? UniqueGift
        {
            get => uniqueGift;
            set { OnPropertyChanging(nameof(UniqueGift)); uniqueGift = value; OnPropertyChanged(nameof(UniqueGift)); }
        }

        private Guid? uniqueGiftID;
        [ForeignKey("TelegramUniqueGift")]
        public virtual Guid? UniqueGiftID
        {
            get => uniqueGiftID;
            set { OnPropertyChanging(nameof(UniqueGiftID)); uniqueGiftID = value; OnPropertyChanged(nameof(UniqueGiftID)); }
        }

        private string? connectedWebsite;
        public virtual string? ConnectedWebsite
        {
            get => connectedWebsite;
            set { OnPropertyChanging(nameof(ConnectedWebsite)); connectedWebsite = value; OnPropertyChanged(nameof(ConnectedWebsite)); }
        }

        private TelegramWriteAccessAllowed? writeAccessAllowed;
        [InverseProperty(nameof(TelegramWriteAccessAllowed.MessageThisWriteAccessAllowedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramWriteAccessAllowed? WriteAccessAllowed
        {
            get => writeAccessAllowed;
            set { OnPropertyChanging(nameof(WriteAccessAllowed)); writeAccessAllowed = value; OnPropertyChanged(nameof(WriteAccessAllowed)); }
        }

        private Guid? writeAccessAllowedID;
        [ForeignKey("TelegramWriteAccessAllowed")]
        public virtual Guid? WriteAccessAllowedID
        {
            get => writeAccessAllowedID;
            set { OnPropertyChanging(nameof(WriteAccessAllowedID)); writeAccessAllowedID = value; OnPropertyChanged(nameof(WriteAccessAllowedID)); }
        }

        private TelegramPassportData? passportData;
        [InverseProperty(nameof(TelegramPassportData.MessageThisPassportDataBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramPassportData? PassportData
        {
            get => passportData;
            set { OnPropertyChanging(nameof(PassportData)); passportData = value; OnPropertyChanged(nameof(PassportData)); }
        }

        private Guid? passportDataID;
        [ForeignKey("TelegramPassportData")]
        public virtual Guid? PassportDataID
        {
            get => passportDataID;
            set { OnPropertyChanging(nameof(PassportDataID)); passportDataID = value; OnPropertyChanged(nameof(PassportDataID)); }
        }

        private TelegramProximityAlertTriggered? proximityAlertTriggered;
        [InverseProperty(nameof(TelegramProximityAlertTriggered.MessageProximityAlertTriggeredBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramProximityAlertTriggered? ProximityAlertTriggered
        {
            get => proximityAlertTriggered;
            set { OnPropertyChanging(nameof(ProximityAlertTriggered)); proximityAlertTriggered = value; OnPropertyChanged(nameof(ProximityAlertTriggered)); }
        }

        private Guid? proximityAlertTriggeredID;
        [ForeignKey("TelegramProximityAlertTriggered")]
        public virtual Guid? ProximityAlertTriggeredID
        {
            get => proximityAlertTriggeredID;
            set { OnPropertyChanging(nameof(ProximityAlertTriggeredID)); proximityAlertTriggeredID = value; OnPropertyChanged(nameof(ProximityAlertTriggeredID)); }
        }

        private int? boostCount;
        public virtual int? BoostCount
        {
            get => boostCount;
            set { OnPropertyChanging(nameof(BoostCount)); boostCount = value; OnPropertyChanged(nameof(BoostCount)); }
        }

        private TelegramChatBackground? chatBackgroundSet;
        [InverseProperty(nameof(TelegramChatBackground.MessageChatBackgroundSetBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramChatBackground? ChatBackgroundSet
        {
            get => chatBackgroundSet;
            set { OnPropertyChanging(nameof(ChatBackgroundSet)); chatBackgroundSet = value; OnPropertyChanged(nameof(ChatBackgroundSet)); }
        }

        private Guid? chatBackgroundSetID;
        [ForeignKey("ChatBackgroundSet")]
        public virtual Guid? ChatBackgroundSetID
        {
            get => chatBackgroundSetID;
            set { OnPropertyChanging(nameof(ChatBackgroundSetID)); chatBackgroundSetID = value; OnPropertyChanged(nameof(ChatBackgroundSetID)); }
        }

        private TelegramForumTopicCreated? forumTopicCreated;
        [InverseProperty(nameof(TelegramForumTopicCreated.MessageThisForumTopicCreatedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramForumTopicCreated? ForumTopicCreated
        {
            get => forumTopicCreated;
            set { OnPropertyChanging(nameof(ForumTopicCreated)); forumTopicCreated = value; OnPropertyChanged(nameof(ForumTopicCreated)); }
        }

        private Guid? forumTopicCreatedID;
        [ForeignKey("TelegramForumTopicCreated")]
        public virtual Guid? ForumTopicCreatedID
        {
            get => forumTopicCreatedID;
            set { OnPropertyChanging(nameof(ForumTopicCreatedID)); forumTopicCreatedID = value; OnPropertyChanged(nameof(ForumTopicCreatedID)); }
        }

        private TelegramForumTopicEdited? forumTopicEdited;
        [InverseProperty(nameof(TelegramForumTopicEdited.MessageThisForumTopicEditedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramForumTopicEdited? ForumTopicEdited
        {
            get => forumTopicEdited;
            set { OnPropertyChanging(nameof(ForumTopicEdited)); forumTopicEdited = value; OnPropertyChanged(nameof(ForumTopicEdited)); }
        }

        private Guid? forumTopicEditedID;
        [ForeignKey("TelegramForumTopicEdited")]
        public virtual Guid? ForumTopicEditedID
        {
            get => forumTopicEditedID;
            set { OnPropertyChanging(nameof(ForumTopicEditedID)); forumTopicEditedID = value; OnPropertyChanged(nameof(ForumTopicEditedID)); }
        }

        private bool? forumTopicClosed;
        public virtual bool? ForumTopicClosed
        {
            get => forumTopicClosed;
            set { OnPropertyChanging(nameof(ForumTopicClosed)); forumTopicClosed = value; OnPropertyChanged(nameof(ForumTopicClosed)); }
        }

        private bool? forumTopicReopened;
        public virtual bool? ForumTopicReopened
        {
            get => forumTopicReopened;
            set { OnPropertyChanging(nameof(ForumTopicReopened)); forumTopicReopened = value; OnPropertyChanged(nameof(ForumTopicReopened)); }
        }

        private bool? generalForumTopicHidden;
        public virtual bool? GeneralForumTopicHidden
        {
            get => generalForumTopicHidden;
            set { OnPropertyChanging(nameof(GeneralForumTopicHidden)); generalForumTopicHidden = value; OnPropertyChanged(nameof(GeneralForumTopicHidden)); }
        }

        private bool? generalForumTopicUnhidden;
        public virtual bool? GeneralForumTopicUnhidden
        {
            get => generalForumTopicUnhidden;
            set { OnPropertyChanging(nameof(GeneralForumTopicUnhidden)); generalForumTopicUnhidden = value; OnPropertyChanged(nameof(GeneralForumTopicUnhidden)); }
        }

        private TelegramGiveawayCreated? giveawayCreated;
        [InverseProperty(nameof(TelegramGiveawayCreated.MessageThisGiveawayCreatedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGiveawayCreated? GiveawayCreated
        {
            get => giveawayCreated;
            set { OnPropertyChanging(nameof(GiveawayCreated)); giveawayCreated = value; OnPropertyChanged(nameof(GiveawayCreated)); }
        }

        private Guid? giveawayCreatedID;
        [ForeignKey("TelegramGiveawayCreated")]
        public virtual Guid? GiveawayCreatedID
        {
            get => giveawayCreatedID;
            set { OnPropertyChanging(nameof(GiveawayCreatedID)); giveawayCreatedID = value; OnPropertyChanged(nameof(GiveawayCreatedID)); }
        }

        private TelegramGiveaway? giveaway;
        [InverseProperty(nameof(TelegramGiveaway.MessageGiveawayBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGiveaway? Giveaway
        {
            get => giveaway;
            set { OnPropertyChanging(nameof(Giveaway)); giveaway = value; OnPropertyChanged(nameof(Giveaway)); }
        }

        private Guid? giveawayID;
        [ForeignKey("TelegramGiveaway")]
        public virtual Guid? GiveawayID
        {
            get => giveawayID;
            set { OnPropertyChanging(nameof(GiveawayID)); giveawayID = value; OnPropertyChanged(nameof(GiveawayID)); }
        }
        private TelegramGiveawayWinners? giveawayWinners;
        [InverseProperty(nameof(TelegramGiveawayWinners.MessageThisGiveawayWinnersBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGiveawayWinners? GiveawayWinners
        {
            get => giveawayWinners;
            set { OnPropertyChanging(nameof(GiveawayWinners)); giveawayWinners = value; OnPropertyChanged(nameof(GiveawayWinners)); }
        }

        private Guid? giveawayWinnersID;
        [ForeignKey("TelegramGiveawayWinners")]
        public virtual Guid? GiveawayWinnersID
        {
            get => giveawayWinnersID;
            set { OnPropertyChanging(nameof(GiveawayWinnersID)); giveawayWinnersID = value; OnPropertyChanged(nameof(GiveawayWinnersID)); }
        }

        private TelegramGiveawayCompleted? giveawayCompleted;
        [InverseProperty(nameof(TelegramGiveawayCompleted.MessageThisGiveawayCompletedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramGiveawayCompleted? GiveawayCompleted
        {
            get => giveawayCompleted;
            set { OnPropertyChanging(nameof(GiveawayCompleted)); giveawayCompleted = value; OnPropertyChanged(nameof(GiveawayCompleted)); }
        }

        private Guid? giveawayCompletedID;
        [ForeignKey("TelegramGiveawayCompleted")]
        public virtual Guid? GiveawayCompletedID
        {
            get => giveawayCompletedID;
            set { OnPropertyChanging(nameof(GiveawayCompletedID)); giveawayCompletedID = value; OnPropertyChanged(nameof(GiveawayCompletedID)); }
        }

        private long? paidMessagePriceChanged;
        public virtual long? PaidMessagePriceChanged
        {
            get => paidMessagePriceChanged;
            set { OnPropertyChanging(nameof(PaidMessagePriceChanged)); paidMessagePriceChanged = value; OnPropertyChanged(nameof(PaidMessagePriceChanged)); }
        }

        private TelegramVideoChatScheduled? videoChatScheduled;
        [InverseProperty(nameof(TelegramVideoChatScheduled.MessageThisVideoChatScheduledBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramVideoChatScheduled? VideoChatScheduled
        {
            get => videoChatScheduled;
            set { OnPropertyChanging(nameof(VideoChatScheduled)); videoChatScheduled = value; OnPropertyChanged(nameof(VideoChatScheduled)); }
        }

        private Guid? videoChatScheduledID;
        [ForeignKey("TelegramVideoChatScheduled")]
        public virtual Guid? VideoChatScheduledID
        {
            get => videoChatScheduledID;
            set { OnPropertyChanging(nameof(VideoChatScheduledID)); videoChatScheduledID = value; OnPropertyChanged(nameof(VideoChatScheduledID)); }
        }

        private bool? videoChatStarted;
        public virtual bool? VideoChatStarted
        {
            get => videoChatStarted;
            set { OnPropertyChanging(nameof(VideoChatStarted)); videoChatStarted = value; OnPropertyChanged(nameof(VideoChatStarted)); }
        }

        private int? videoChatEnded;
        public virtual int? VideoChatEnded
        {
            get => videoChatEnded;
            set { OnPropertyChanging(nameof(VideoChatEnded)); videoChatEnded = value; OnPropertyChanged(nameof(VideoChatEnded)); }
        }

        private TelegramVideoChatParticipantsInvited? videoChatParticipantsInvited;
        [InverseProperty(nameof(TelegramVideoChatParticipantsInvited.MessageThisVideoChatParticipantsInvitedBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramVideoChatParticipantsInvited? VideoChatParticipantsInvited
        {
            get => videoChatParticipantsInvited;
            set { OnPropertyChanging(nameof(VideoChatParticipantsInvited)); videoChatParticipantsInvited = value; OnPropertyChanged(nameof(VideoChatParticipantsInvited)); }
        }

        private Guid? videoChatParticipantsInvitedID;
        [ForeignKey("TelegramVideoChatParticipantsInvited")]
        public virtual Guid? VideoChatParticipantsInvitedID
        {
            get => videoChatParticipantsInvitedID;
            set { OnPropertyChanging(nameof(VideoChatParticipantsInvitedID)); videoChatParticipantsInvitedID = value; OnPropertyChanged(nameof(VideoChatParticipantsInvitedID)); }
        }

        private TelegramWebAppData? webAppData;
        [InverseProperty(nameof(TelegramWebAppData.MessageThisWebAppDataBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramWebAppData? WebAppData
        {
            get => webAppData;
            set { OnPropertyChanging(nameof(WebAppData)); webAppData = value; OnPropertyChanged(nameof(WebAppData)); }
        }

        private Guid? webAppDataID;
        [ForeignKey("TelegramWebAppData")]
        public virtual Guid? WebAppDataID
        {
            get => webAppDataID;
            set { OnPropertyChanging(nameof(WebAppDataID)); webAppDataID = value; OnPropertyChanged(nameof(WebAppDataID)); }
        }

        private TelegramInlineKeyboardMarkup? replyMarkup;
        [InverseProperty(nameof(TelegramInlineKeyboardMarkup.MessageThisInlineKeyboardMarkupBelongsTo))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual TelegramInlineKeyboardMarkup? ReplyMarkup
        {
            get => replyMarkup;
            set { OnPropertyChanging(nameof(ReplyMarkup)); replyMarkup = value; OnPropertyChanged(nameof(ReplyMarkup)); }
        }

        private Guid? replyMarkupID;
        [ForeignKey("TelegramReplyMarkup")]
        public virtual Guid? ReplyMarkupID
        {
            get => replyMarkupID;
            set { OnPropertyChanging(nameof(ReplyMarkupID)); replyMarkupID = value; OnPropertyChanged(nameof(ReplyMarkupID)); }
        }

        [InverseProperty(nameof(TelegramGiveawayCompleted.GiveawayMessage))]
        [JsonIgnore]
        public virtual IList<TelegramGiveawayCompleted>? GiveAwayGiveawayCompletedThisMessageBelongsTo { get; set; } = new ObservableCollection<TelegramGiveawayCompleted>();

        [InverseProperty(nameof(TelegramCallbackQuery.Message))]
        [JsonIgnore]
        public virtual IList<TelegramCallbackQuery>? CallbackQueryThisMessageBelongsTo { get; set; } = new ObservableCollection<TelegramCallbackQuery>();


    }
}