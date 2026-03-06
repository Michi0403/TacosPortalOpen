//-----------------------------------------------------------------------
// <copyright file="TelegramGiveawayWinners.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramGiveawayWinners : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int? _additionalChatCount;
    private TelegramChat _chat = null!;

    private Guid? _chatID;
    private int _giveawayMessageId;
    private bool _onlyNewMembers;
    private int? _premiumSubscriptionMonthCount;
    private string _prizeDescription = string.Empty;
    private long? _prizeStarCount;
    private int? _unclaimedPrizeCount;
    private bool _wasRefunded;
    private int _winnerCount;
    private DateTime _winnersSelectionDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual int? AdditionalChatCount
    {
        get => _additionalChatCount;
        set
        {
            OnPropertyChanging(nameof(AdditionalChatCount));
            _additionalChatCount = value;
            OnPropertyChanged(nameof(AdditionalChatCount));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.GiveawayGiveawayWinnersThisChatBelongsTo))]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set
        {
            OnPropertyChanging(nameof(Chat));
            _chat = value;
            OnPropertyChanged(nameof(Chat));
        }
    }

    [ForeignKey(nameof(Chat))]
    public virtual Guid? ChatID
    {
        get => _chatID;
        set
        {
            OnPropertyChanging(nameof(ChatID));
            _chatID = value;
            OnPropertyChanged(nameof(ChatID));
        }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.GiveawayWinners))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisGiveawayWinnersBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual int GiveawayMessageId
    {
        get => _giveawayMessageId;
        set
        {
            OnPropertyChanging(nameof(GiveawayMessageId));
            _giveawayMessageId = value;
            OnPropertyChanged(nameof(GiveawayMessageId));
        }
    }

    [InverseProperty(nameof(TelegramMessage.GiveawayWinners))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisGiveawayWinnersBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual bool OnlyNewMembers
    {
        get => _onlyNewMembers;
        set
        {
            OnPropertyChanging(nameof(OnlyNewMembers));
            _onlyNewMembers = value;
            OnPropertyChanged(nameof(OnlyNewMembers));
        }
    }

    public virtual int? PremiumSubscriptionMonthCount
    {
        get => _premiumSubscriptionMonthCount;
        set
        {
            OnPropertyChanging(nameof(PremiumSubscriptionMonthCount));
            _premiumSubscriptionMonthCount = value;
            OnPropertyChanged(nameof(PremiumSubscriptionMonthCount));
        }
    }

    public virtual string PrizeDescription
    {
        get => _prizeDescription;
        set
        {
            OnPropertyChanging(nameof(PrizeDescription));
            _prizeDescription = value;
            OnPropertyChanged(nameof(PrizeDescription));
        }
    }

    public virtual long? PrizeStarCount
    {
        get => _prizeStarCount;
        set
        {
            OnPropertyChanging(nameof(PrizeStarCount));
            _prizeStarCount = value;
            OnPropertyChanged(nameof(PrizeStarCount));
        }
    }

    public virtual int? UnclaimedPrizeCount
    {
        get => _unclaimedPrizeCount;
        set
        {
            OnPropertyChanging(nameof(UnclaimedPrizeCount));
            _unclaimedPrizeCount = value;
            OnPropertyChanged(nameof(UnclaimedPrizeCount));
        }
    }

    public virtual bool WasRefunded
    {
        get => _wasRefunded;
        set
        {
            OnPropertyChanging(nameof(WasRefunded));
            _wasRefunded = value;
            OnPropertyChanged(nameof(WasRefunded));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int WinnerCount
    {
        get => _winnerCount;
        set
        {
            OnPropertyChanging(nameof(WinnerCount));
            _winnerCount = value;
            OnPropertyChanged(nameof(WinnerCount));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.GiveawayWinnersThisWinnerUsersBelongsTo))]
    public virtual IList<TelegramUser>? Winners { get; set; } = new ObservableCollection<TelegramUser>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime WinnersSelectionDate
    {
        get => _winnersSelectionDate;
        set
        {
            OnPropertyChanging(nameof(WinnersSelectionDate));
            _winnersSelectionDate = value;
            OnPropertyChanged(nameof(WinnersSelectionDate));
        }
    }
}
