//-----------------------------------------------------------------------
// <copyright file="TelegramChatBoostSource.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

public abstract partial class TelegramChatBoostSource : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramChatBoostRemoved.Source))]
    public virtual IList<TelegramChatBoostRemoved>? ChatBoostRemovedThisChatBoostSourceBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostRemoved>();

    [InverseProperty(nameof(TelegramChatBoost.Source))]
    public virtual IList<TelegramChatBoost>? ChatBoostThisChatBoostSourceBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoost>();
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramChatBoostSourcePremium : TelegramChatBoostSource
{
    private TelegramUser _user = null!;
    private Guid? _userID;

    public virtual ChatBoostSourceType Source => ChatBoostSourceType.Premium;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(User.ChatBoostSourcePremiumThisUserBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }

    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => _userID;
        set { OnPropertyChanging(nameof(UserID)); _userID = value; OnPropertyChanged(nameof(UserID)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramChatBoostSourceGiftCode : TelegramChatBoostSource
{
    private TelegramUser _user = null!;
    private Guid? _userID;

    public virtual ChatBoostSourceType Source => ChatBoostSourceType.GiftCode;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(User.ChatBoostSourceGiftCodeThisUserBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }

    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => _userID;
        set { OnPropertyChanging(nameof(UserID)); _userID = value; OnPropertyChanged(nameof(UserID)); }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramChatBoostSourceGiveaway : TelegramChatBoostSource
{
    private int _giveawayMessageId;
    private bool _isUnclaimed;
    private long? _prizeStarCount;
    private TelegramUser? _user;
    private Guid? _userID;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int GiveawayMessageId
    {
        get => _giveawayMessageId;
        set { OnPropertyChanging(nameof(GiveawayMessageId)); _giveawayMessageId = value; OnPropertyChanged(nameof(GiveawayMessageId)); }
    }

    public virtual bool IsUnclaimed
    {
        get => _isUnclaimed;
        set { OnPropertyChanging(nameof(IsUnclaimed)); _isUnclaimed = value; OnPropertyChanged(nameof(IsUnclaimed)); }
    }

    public virtual long? PrizeStarCount
    {
        get => _prizeStarCount;
        set { OnPropertyChanging(nameof(PrizeStarCount)); _prizeStarCount = value; OnPropertyChanged(nameof(PrizeStarCount)); }
    }

    public virtual ChatBoostSourceType Source => ChatBoostSourceType.Giveaway;

    [InverseProperty(nameof(User.ChatBoostSourceGiveawayThisUserBelongsTo))]
    public virtual TelegramUser? User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }

    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => _userID;
        set { OnPropertyChanging(nameof(UserID)); _userID = value; OnPropertyChanged(nameof(UserID)); }
    }
}
