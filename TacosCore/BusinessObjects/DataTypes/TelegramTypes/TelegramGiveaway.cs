//-----------------------------------------------------------------------
// <copyright file="TelegramGiveaway.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramGiveaway : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _hasPublicWinners;
    private bool _onlyNewMembers;
    private int? _premiumSubscriptionMonthCount;
    private string _prizeDescription = string.Empty;
    private long? _prizeStarCount;
    private int _winnerCount;

    private DateTime _winnersSelectionDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.GiveawayThisChatsBelongsTo))]
    public virtual IList<TelegramChat>? Chats { get; set; } = new ObservableCollection<TelegramChat>();


    public virtual IList<string>? CountryCodes { get; set; } = new ObservableCollection<string>();

    [InverseProperty(nameof(TelegramExternalReplyInfo.Giveaway))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisGiveawayBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


    public virtual bool HasPublicWinners
    {
        get => _hasPublicWinners;
        set
        {
            OnPropertyChanging(nameof(HasPublicWinners));
            _hasPublicWinners = value;
            OnPropertyChanged(nameof(HasPublicWinners));
        }
    }

    [InverseProperty(nameof(TelegramMessage.Giveaway))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageGiveawayBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


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
