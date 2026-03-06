//-----------------------------------------------------------------------
// <copyright file="TelegramGiveawayCompleted.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramGiveawayCompleted : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramMessage _giveawayMessage = null!;
    private Guid? _giveawayMessageID;
    private bool _isStarGiveaway;
    private int? _unclaimedPrizeCount;

    private int _winnerCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [InverseProperty(nameof(TelegramMessage.GiveAwayGiveawayCompletedThisMessageBelongsTo))]
    public virtual TelegramMessage GiveawayMessage
    {
        get => _giveawayMessage;
        set
        {
            OnPropertyChanging(nameof(GiveawayMessage));
            _giveawayMessage = value;
            OnPropertyChanged(nameof(GiveawayMessage));
        }
    }

    [ForeignKey(nameof(GiveawayMessage))]
    public virtual Guid? GiveawayMessageID
    {
        get => _giveawayMessageID;
        set
        {
            OnPropertyChanging(nameof(GiveawayMessageID));
            _giveawayMessageID = value;
            OnPropertyChanged(nameof(GiveawayMessageID));
        }
    }


    public virtual bool IsStarGiveaway
    {
        get => _isStarGiveaway;
        set
        {
            OnPropertyChanging(nameof(IsStarGiveaway));
            _isStarGiveaway = value;
            OnPropertyChanged(nameof(IsStarGiveaway));
        }
    }

    [InverseProperty(nameof(TelegramMessage.GiveawayCompleted))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisGiveawayCompletedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


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
}
