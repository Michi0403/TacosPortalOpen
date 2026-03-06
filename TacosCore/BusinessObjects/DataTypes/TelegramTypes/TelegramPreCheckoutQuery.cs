//-----------------------------------------------------------------------
// <copyright file="TelegramPreCheckoutQuery.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPreCheckoutQuery : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _currency = string.Empty;
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private string _invoicePayload = string.Empty;
    private TelegramOrderInfo _orderInfo;
    private Guid? _orderInfoID;

    private string _preCheckoutQueryId = string.Empty;
    private string _shippingOptionId = string.Empty;
    private int _totalAmount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Currency
    {
        get => _currency;
        set { OnPropertyChanging(nameof(Currency)); _currency = value; OnPropertyChanged(nameof(Currency)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.PreCheckoutQueryThisUserBelongsTo))]
    public virtual TelegramUser From
    {
        get => _from;
        set { OnPropertyChanging(nameof(From)); _from = value; OnPropertyChanged(nameof(From)); }
    }

    [ForeignKey("From")]
    public virtual Guid? FromID
    {
        get => _fromID;
        set { OnPropertyChanging(nameof(FromID)); _fromID = value; OnPropertyChanged(nameof(FromID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string InvoicePayload
    {
        get => _invoicePayload;
        set { OnPropertyChanging(nameof(InvoicePayload)); _invoicePayload = value; OnPropertyChanged(nameof(InvoicePayload)); }
    }

    [InverseProperty(nameof(OrderInfo.PreCheckoutQueryThisOrderInfoBelongsTo))]
    public virtual TelegramOrderInfo OrderInfo
    {
        get => _orderInfo;
        set { OnPropertyChanging(nameof(OrderInfo)); _orderInfo = value; OnPropertyChanged(nameof(OrderInfo)); }
    }

    [ForeignKey("OrderInfo")]
    public virtual Guid? OrderInfoID
    {
        get => _orderInfoID;
        set { OnPropertyChanging(nameof(OrderInfoID)); _orderInfoID = value; OnPropertyChanged(nameof(OrderInfoID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string PreCheckoutQueryId
    {
        get => _preCheckoutQueryId;
        set { OnPropertyChanging(nameof(PreCheckoutQueryId)); _preCheckoutQueryId = value; OnPropertyChanged(nameof(PreCheckoutQueryId)); }
    }

    public virtual string ShippingOptionId
    {
        get => _shippingOptionId;
        set { OnPropertyChanging(nameof(ShippingOptionId)); _shippingOptionId = value; OnPropertyChanged(nameof(ShippingOptionId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TotalAmount
    {
        get => _totalAmount;
        set { OnPropertyChanging(nameof(TotalAmount)); _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
    }
}
