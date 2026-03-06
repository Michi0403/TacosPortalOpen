//-----------------------------------------------------------------------
// <copyright file="TelegramMessageEntity.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageEntity : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _customEmojiId = string.Empty;
    private string _language = string.Empty;
    private int _length;
    private int _offset;

    private MessageEntityType _type;
    private string _url = string.Empty;
    private TelegramUser _user = null!;
    private Guid? _userId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string CustomEmojiId
    {
        get => _customEmojiId;
        set { OnPropertyChanging(nameof(CustomEmojiId)); _customEmojiId = value; OnPropertyChanged(nameof(CustomEmojiId)); }
    }

    [InverseProperty(nameof(TelegramGame.TextEntities))]
    [JsonIgnore]
    public virtual IList<TelegramGame>? GameThisMessageEntityBelongsTo { get; set; } = new ObservableCollection<TelegramGame>();

    [InverseProperty(nameof(TelegramGiftInfo.Entities))]
    [JsonIgnore]
    public virtual IList<TelegramGiftInfo>? GiftInfoThisMessageEntityBelongsTo { get; set; } = new ObservableCollection<TelegramGiftInfo>();

    public virtual string Language
    {
        get => _language;
        set { OnPropertyChanging(nameof(Language)); _language = value; OnPropertyChanged(nameof(Language)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Length
    {
        get => _length;
        set { OnPropertyChanging(nameof(Length)); _length = value; OnPropertyChanged(nameof(Length)); }
    }

    [InverseProperty(nameof(TelegramMessage.CaptionEntities))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisCaptionEntitiesBelongingTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [InverseProperty(nameof(TelegramMessage.Entities))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisMessageEntitiesBelongingTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Offset
    {
        get => _offset;
        set { OnPropertyChanging(nameof(Offset)); _offset = value; OnPropertyChanged(nameof(Offset)); }
    }

    [InverseProperty(nameof(TelegramPoll.ExplanationEntities))]
    [JsonIgnore]
    public virtual IList<TelegramPoll>? PollExplanationEntitiesToThisPoll { get; set; } = new ObservableCollection<TelegramPoll>();

    [InverseProperty(nameof(TelegramPoll.QuestionEntities))]
    [JsonIgnore]
    public virtual IList<TelegramPoll>? PollQuestionEntitiesToThisPoll { get; set; } = new ObservableCollection<TelegramPoll>();

    [InverseProperty(nameof(TelegramTextQuote.Entities))]
    [JsonIgnore]
    public virtual IList<TelegramTextQuote>? TextQuoteThisMessageEntityBelongsTo { get; set; } = new ObservableCollection<TelegramTextQuote>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual MessageEntityType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }

    public virtual string Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }

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
