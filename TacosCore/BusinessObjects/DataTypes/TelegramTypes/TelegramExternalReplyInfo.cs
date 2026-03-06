//-----------------------------------------------------------------------
// <copyright file="TelegramExternalReplyInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

[DefaultClassOptions]
[Authorize]
public partial class TelegramExternalReplyInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramAnimation _animation = null!;
    private TelegramAudio _audio = null!;
    private TelegramChat _chat = null!;
    private TelegramContact _contact = null!;
    private TelegramDice _dice = null!;
    private TelegramDocument _document = null!;
    private TelegramGame _game = null!;
    private TelegramGiveaway _giveaway = null!;
    private TelegramGiveawayWinners _giveawayWinners = null!;
    private bool _hasMediaSpoiler;
    private TelegramInvoice _invoice = null!;
    private TelegramLinkPreviewOptions _linkPreviewOptions = null!;
    private TelegramLocation _location = null!;

    private TelegramMessageOrigin _origin = null!;
    private TelegramPaidMediaInfo _paidMedia = null!;
    private TelegramPoll _poll = null!;
    private TelegramSticker _sticker = null!;
    private TelegramStory _story = null!;
    private int? _uniqueMessageId;
    private TelegramVenue _venue = null!;
    private TelegramVideo _video = null!;
    private TelegramVideoNote _videoNote = null!;
    private TelegramVoice _voice = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramAnimation.ExternalReplyThisAnimationBelongsTo))]
    public virtual TelegramAnimation Animation { get => _animation; set { OnPropertyChanging(nameof(Animation)); _animation = value; OnPropertyChanged(nameof(Animation)); } }



    [ForeignKey("Animation")]
    public virtual Guid? AnimationID { get; set; }
    [InverseProperty(nameof(TelegramAudio.ExternalReplyThisAudioBelongsTo))]
    public virtual TelegramAudio Audio { get => _audio; set { OnPropertyChanging(nameof(Audio)); _audio = value; OnPropertyChanged(nameof(Audio)); } }

    [ForeignKey("Audio")]
    public virtual Guid? AudioID { get; set; }

    [InverseProperty(nameof(TelegramChat.ExternalReplyThisChatBelongsTo))]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID { get; set; }
    [InverseProperty(nameof(TelegramContact.ExternalReplyThisContactBelongsTo))]
    public virtual TelegramContact Contact { get => _contact; set { OnPropertyChanging(nameof(Contact)); _contact = value; OnPropertyChanged(nameof(Contact)); } }

    [ForeignKey("Contact")]
    public virtual Guid? ContactID { get; set; }
    [InverseProperty(nameof(TelegramDice.ExternalReplyThisDiceBelongsTo))]
    public virtual TelegramDice Dice { get => _dice; set { OnPropertyChanging(nameof(Dice)); _dice = value; OnPropertyChanged(nameof(Dice)); } }

    [ForeignKey("Dice")]
    public virtual Guid? DiceID { get; set; }
    [InverseProperty(nameof(TelegramDocument.ExternalReplyThisDocumentBelongsTo))]
    public virtual TelegramDocument Document { get => _document; set { OnPropertyChanging(nameof(Document)); _document = value; OnPropertyChanged(nameof(Document)); } }

    [ForeignKey("Document")]
    public virtual Guid? DocumentID { get; set; }
    [InverseProperty(nameof(TelegramGame.ExternalReplyThisGameBelongsTo))]
    public virtual TelegramGame Game { get => _game; set { OnPropertyChanging(nameof(Game)); _game = value; OnPropertyChanged(nameof(Game)); } }

    [ForeignKey("Game")]
    public virtual Guid? GameID { get; set; }
    [InverseProperty(nameof(TelegramGiveaway.ExternalReplyThisGiveawayBelongsTo))]
    public virtual TelegramGiveaway Giveaway { get => _giveaway; set { OnPropertyChanging(nameof(Giveaway)); _giveaway = value; OnPropertyChanged(nameof(Giveaway)); } }

    [ForeignKey("Giveaway")]
    public virtual Guid? GiveawayID { get; set; }
    [InverseProperty(nameof(TelegramGiveawayWinners.ExternalReplyThisGiveawayWinnersBelongsTo))]
    public virtual TelegramGiveawayWinners GiveawayWinners { get => _giveawayWinners; set { OnPropertyChanging(nameof(GiveawayWinners)); _giveawayWinners = value; OnPropertyChanged(nameof(GiveawayWinners)); } }

    [ForeignKey("GiveawayWinners")]
    public virtual Guid? GiveawayWinnersID { get; set; }

    public virtual bool HasMediaSpoiler
    {
        get => _hasMediaSpoiler;
        set { OnPropertyChanging(nameof(HasMediaSpoiler)); _hasMediaSpoiler = value; OnPropertyChanged(nameof(HasMediaSpoiler)); }
    }
    [InverseProperty(nameof(TelegramInvoice.ExternalReplyThisInvoiceBelongsTo))]
    public virtual TelegramInvoice Invoice { get => _invoice; set { OnPropertyChanging(nameof(Invoice)); _invoice = value; OnPropertyChanged(nameof(Invoice)); } }

    [ForeignKey("Invoice")]
    public virtual Guid? InvoiceID { get; set; }

    [InverseProperty(nameof(TelegramLinkPreviewOptions.ExternalReplyThisLinkPreviewOptionsBelongsTo))]
    public virtual TelegramLinkPreviewOptions LinkPreviewOptions
    {
        get => _linkPreviewOptions;
        set { OnPropertyChanging(nameof(LinkPreviewOptions)); _linkPreviewOptions = value; OnPropertyChanged(nameof(LinkPreviewOptions)); }
    }

    [ForeignKey("LinkPreviewOptions")]
    public virtual Guid? LinkPreviewOptionsID { get; set; }
    [InverseProperty(nameof(TelegramLocation.ExternalReplyThisLocationBelongsTo))]
    public virtual TelegramLocation Location { get => _location; set { OnPropertyChanging(nameof(Location)); _location = value; OnPropertyChanged(nameof(Location)); } }

    [ForeignKey("Location")]
    public virtual Guid? LocationID { get; set; }

    [InverseProperty(nameof(TelegramMessage.ExternalReply))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisExternalReplyInfoBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramMessageOrigin.ExternalReplyThisMessageOriginBelongsTo))]
    public required virtual TelegramMessageOrigin? Origin
    {
        get => _origin;
        set { OnPropertyChanging(nameof(Origin)); _origin = value; OnPropertyChanged(nameof(Origin)); }
    }
    [InverseProperty(nameof(TelegramPaidMediaInfo.ExternalReplyThisPaidMediaInfoBelongsTo))]
    public virtual TelegramPaidMediaInfo PaidMedia { get => _paidMedia; set { OnPropertyChanging(nameof(PaidMedia)); _paidMedia = value; OnPropertyChanged(nameof(PaidMedia)); } }

    [ForeignKey("PaidMedia")]
    public virtual Guid? PaidMediaID { get; set; }

    [InverseProperty(nameof(TelegramPhotoSize.ExternalReplies))]
    public virtual IList<TelegramPhotoSize> Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();
    [InverseProperty(nameof(TelegramPoll.ExternalReplyThisPollBelongsTo))]
    public virtual TelegramPoll Poll { get => _poll; set { OnPropertyChanging(nameof(Poll)); _poll = value; OnPropertyChanged(nameof(Poll)); } }

    [ForeignKey("Poll")]
    public virtual Guid? PollID { get; set; }
    [InverseProperty(nameof(TelegramSticker.ExternalReplyThisStickerBelongsTo))]
    public virtual TelegramSticker Sticker { get => _sticker; set { OnPropertyChanging(nameof(Sticker)); _sticker = value; OnPropertyChanged(nameof(Sticker)); } }

    [ForeignKey("Sticker")]
    public virtual Guid? StickerID { get; set; }
    [InverseProperty(nameof(TelegramStory.ExternalReplyThisStoryBelongsTo))]
    public virtual TelegramStory Story { get => _story; set { OnPropertyChanging(nameof(Story)); _story = value; OnPropertyChanged(nameof(Story)); } }

    [ForeignKey("Story")]
    public virtual Guid? StoryID { get; set; }

    public virtual int? UniqueMessageId
    {
        get => _uniqueMessageId;
        set { OnPropertyChanging(nameof(UniqueMessageId)); _uniqueMessageId = value; OnPropertyChanged(nameof(UniqueMessageId)); }
    }
    [InverseProperty(nameof(TelegramVenue.ExternalReplyThisVenueBelongsTo))]
    public virtual TelegramVenue Venue { get => _venue; set { OnPropertyChanging(nameof(Venue)); _venue = value; OnPropertyChanged(nameof(Venue)); } }

    [ForeignKey("Venue")]
    public virtual Guid? VenueID { get; set; }
    [InverseProperty(nameof(TelegramVideo.ExternalReplyThisVideoBelongsTo))]
    public virtual TelegramVideo Video { get => _video; set { OnPropertyChanging(nameof(Video)); _video = value; OnPropertyChanged(nameof(Video)); } }

    [ForeignKey("Video")]
    public virtual Guid? VideoID { get; set; }
    [InverseProperty(nameof(TelegramVideoNote.ExternalReplyThisVideoNoteBelongsTo))]
    public virtual TelegramVideoNote VideoNote { get => _videoNote; set { OnPropertyChanging(nameof(VideoNote)); _videoNote = value; OnPropertyChanged(nameof(VideoNote)); } }

    [ForeignKey("VideoNote")]
    public virtual Guid? VideoNoteID { get; set; }
    [InverseProperty(nameof(TelegramVoice.ExternalReplyThisVoiceBelongsTo))]
    public virtual TelegramVoice Voice { get => _voice; set { OnPropertyChanging(nameof(Voice)); _voice = value; OnPropertyChanged(nameof(Voice)); } }

    [ForeignKey("Voice")]
    public virtual Guid? VoiceID { get; set; }
}
