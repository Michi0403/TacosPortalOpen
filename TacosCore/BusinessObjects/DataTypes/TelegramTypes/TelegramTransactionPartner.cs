//-----------------------------------------------------------------------
// <copyright file="TelegramTransactionPartner.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Payments;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramTransactionPartner : BaseObject { public abstract TelegramTransactionPartnerType Type { get; } }

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerUser : TelegramTransactionPartner, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramAffiliateInfo _affiliate = null!;
    private Guid? _affiliateId;
    private TelegramGift _gift = null!;
    private Guid? _giftId;
    private string _invoicePayload = string.Empty;
    private string _paidMediaPayload = string.Empty;
    private int? _premiumSubscriptionDuration;
    private int? _subscriptionPeriod;

    private TransactionPartnerUserTransactionType _transactionType;
    private TelegramUser _user = null!;
    private Guid? _userId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramAffiliateInfo Affiliate
    {
        get => _affiliate;
        set { OnPropertyChanging(nameof(Affiliate)); _affiliate = value; OnPropertyChanged(nameof(Affiliate)); }
    }

    [ForeignKey("Affiliate")]
    public virtual Guid? AffiliateID
    {
        get => _affiliateId;
        set { OnPropertyChanging(nameof(AffiliateID)); _affiliateId = value; OnPropertyChanged(nameof(AffiliateID)); }
    }

    public virtual TelegramGift Gift
    {
        get => _gift;
        set { OnPropertyChanging(nameof(Gift)); _gift = value; OnPropertyChanged(nameof(Gift)); }
    }

    [ForeignKey("Gift")]
    public virtual Guid? GiftID
    {
        get => _giftId;
        set { OnPropertyChanging(nameof(GiftID)); _giftId = value; OnPropertyChanged(nameof(GiftID)); }
    }

    public virtual string InvoicePayload
    {
        get => _invoicePayload;
        set { OnPropertyChanging(nameof(InvoicePayload)); _invoicePayload = value; OnPropertyChanged(nameof(InvoicePayload)); }
    }

    public virtual IList<TelegramPaidMedia>? PaidMedia { get; set; } = new ObservableCollection<TelegramPaidMedia>();

    public virtual string PaidMediaPayload
    {
        get => _paidMediaPayload;
        set { OnPropertyChanging(nameof(PaidMediaPayload)); _paidMediaPayload = value; OnPropertyChanged(nameof(PaidMediaPayload)); }
    }

    public virtual int? PremiumSubscriptionDuration
    {
        get => _premiumSubscriptionDuration;
        set { OnPropertyChanging(nameof(PremiumSubscriptionDuration)); _premiumSubscriptionDuration = value; OnPropertyChanged(nameof(PremiumSubscriptionDuration)); }
    }

    public virtual int? SubscriptionPeriod
    {
        get => _subscriptionPeriod;
        set { OnPropertyChanging(nameof(SubscriptionPeriod)); _subscriptionPeriod = value; OnPropertyChanged(nameof(SubscriptionPeriod)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TransactionPartnerUserTransactionType TransactionType
    {
        get => _transactionType;
        set { OnPropertyChanging(nameof(TransactionType)); _transactionType = value; OnPropertyChanged(nameof(TransactionType)); }
    }

    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.User;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
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

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerChat : TelegramTransactionPartner, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChat _chat = null!;

    private Guid? _chatId;
    private TelegramGift _gift = null!;
    private Guid? _giftId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => _chatId;
        set { OnPropertyChanging(nameof(ChatID)); _chatId = value; OnPropertyChanged(nameof(ChatID)); }
    }

    public virtual TelegramGift Gift
    {
        get => _gift;
        set { OnPropertyChanging(nameof(Gift)); _gift = value; OnPropertyChanged(nameof(Gift)); }
    }

    [ForeignKey("Gift")]
    public virtual Guid? GiftID
    {
        get => _giftId;
        set { OnPropertyChanging(nameof(GiftID)); _giftId = value; OnPropertyChanged(nameof(GiftID)); }
    }

    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.Chat;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerAffiliateProgram : TelegramTransactionPartner, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _commissionPerMille;
    private TelegramUser _sponsorUser = null!;

    private Guid? _sponsorUserId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int CommissionPerMille
    {
        get => _commissionPerMille;
        set { OnPropertyChanging(nameof(CommissionPerMille)); _commissionPerMille = value; OnPropertyChanged(nameof(CommissionPerMille)); }
    }

    public virtual TelegramUser SponsorUser
    {
        get => _sponsorUser;
        set { OnPropertyChanging(nameof(SponsorUser)); _sponsorUser = value; OnPropertyChanged(nameof(SponsorUser)); }
    }

    [ForeignKey("SponsorUser")]
    public virtual Guid? SponsorUserID
    {
        get => _sponsorUserId;
        set { OnPropertyChanging(nameof(SponsorUserID)); _sponsorUserId = value; OnPropertyChanged(nameof(SponsorUserID)); }
    }

    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.AffiliateProgram;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerFragment : TelegramTransactionPartner, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramRevenueWithdrawalState _withdrawalState = null!;

    private Guid? _withdrawalStateId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.Fragment;

    public required virtual TelegramRevenueWithdrawalState WithdrawalState
    {
        get => _withdrawalState;
        set { OnPropertyChanging(nameof(WithdrawalState)); _withdrawalState = value; OnPropertyChanged(nameof(WithdrawalState)); }
    }

    [ForeignKey("WithdrawalState")]
    public virtual Guid? WithdrawalStateID
    {
        get => _withdrawalStateId;
        set { OnPropertyChanging(nameof(WithdrawalStateID)); _withdrawalStateId = value; OnPropertyChanged(nameof(WithdrawalStateID)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerTelegramAds : TelegramTransactionPartner
{
    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.TelegramAds;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerTelegramApi : TelegramTransactionPartner, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _requestCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int RequestCount
    {
        get => _requestCount;
        set { OnPropertyChanging(nameof(RequestCount)); _requestCount = value; OnPropertyChanged(nameof(RequestCount)); }
    }

    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.TelegramApi;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramTransactionPartnerOther : TelegramTransactionPartner
{
    public override TelegramTransactionPartnerType Type => TelegramTransactionPartnerType.Other;
}
