//-----------------------------------------------------------------------
// <copyright file="TelegramSuccessfulPayment.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramSuccessfulPayment : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _currency = string.Empty;
    private string _invoicePayload = string.Empty;
    private bool _isFirstRecurring;
    private bool _isRecurring;
    private TelegramOrderInfo _orderInfo = null!;
    private Guid? _orderInfoId;
    private string _providerPaymentChargeId = string.Empty;
    private string _shippingOptionId = string.Empty;
    private DateTime? _subscriptionExpirationDate;
    private string _telegramPaymentChargeId = string.Empty;
    private long _totalAmount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Currency
    {
        get => _currency;
        set { OnPropertyChanging(nameof(Currency)); _currency = value; OnPropertyChanged(nameof(Currency)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string InvoicePayload
    {
        get => _invoicePayload;
        set { OnPropertyChanging(nameof(InvoicePayload)); _invoicePayload = value; OnPropertyChanged(nameof(InvoicePayload)); }
    }

    public virtual bool IsFirstRecurring
    {
        get => _isFirstRecurring;
        set { OnPropertyChanging(nameof(IsFirstRecurring)); _isFirstRecurring = value; OnPropertyChanged(nameof(IsFirstRecurring)); }
    }

    public virtual bool IsRecurring
    {
        get => _isRecurring;
        set { OnPropertyChanging(nameof(IsRecurring)); _isRecurring = value; OnPropertyChanged(nameof(IsRecurring)); }
    }

    [InverseProperty(nameof(TelegramMessage.SuccessfulPayment))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisSuccessfulPaymentBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual TelegramOrderInfo OrderInfo
    {
        get => _orderInfo;
        set { OnPropertyChanging(nameof(OrderInfo)); _orderInfo = value; OnPropertyChanged(nameof(OrderInfo)); }
    }

    [ForeignKey("OrderInfo")]
    public virtual Guid? OrderInfoID
    {
        get => _orderInfoId;
        set { OnPropertyChanging(nameof(OrderInfoID)); _orderInfoId = value; OnPropertyChanged(nameof(OrderInfoID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ProviderPaymentChargeId
    {
        get => _providerPaymentChargeId;
        set { OnPropertyChanging(nameof(ProviderPaymentChargeId)); _providerPaymentChargeId = value; OnPropertyChanged(nameof(ProviderPaymentChargeId)); }
    }

    public virtual string ShippingOptionId
    {
        get => _shippingOptionId;
        set { OnPropertyChanging(nameof(ShippingOptionId)); _shippingOptionId = value; OnPropertyChanged(nameof(ShippingOptionId)); }
    }

    public virtual DateTime? SubscriptionExpirationDate
    {
        get => _subscriptionExpirationDate;
        set { OnPropertyChanging(nameof(SubscriptionExpirationDate)); _subscriptionExpirationDate = value; OnPropertyChanged(nameof(SubscriptionExpirationDate)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string TelegramPaymentChargeId
    {
        get => _telegramPaymentChargeId;
        set { OnPropertyChanging(nameof(TelegramPaymentChargeId)); _telegramPaymentChargeId = value; OnPropertyChanged(nameof(TelegramPaymentChargeId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual long TotalAmount
    {
        get => _totalAmount;
        set { OnPropertyChanging(nameof(TotalAmount)); _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
    }
}