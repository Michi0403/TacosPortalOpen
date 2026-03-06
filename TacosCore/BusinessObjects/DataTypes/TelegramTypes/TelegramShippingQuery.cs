//-----------------------------------------------------------------------
// <copyright file="TelegramShippingQuery.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramShippingQuery : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private string _invoicePayload = string.Empty;
    private TelegramShippingAddress _shippingAddress = null!;
    private Guid? _shippingAddressID;

    private string _shippingQueryId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ShippingQueryThisUserBelongsTo))]
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

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(ShippingAddress.ShippingQueryThisShippingAddressBelongsTo))]
    public virtual TelegramShippingAddress ShippingAddress
    {
        get => _shippingAddress;
        set { OnPropertyChanging(nameof(ShippingAddress)); _shippingAddress = value; OnPropertyChanged(nameof(ShippingAddress)); }
    }




    [ForeignKey("ShippingAddress")]
    public virtual Guid? ShippingAddressID
    {
        get => _shippingAddressID;
        set { OnPropertyChanging(nameof(ShippingAddressID)); _shippingAddressID = value; OnPropertyChanged(nameof(ShippingAddressID)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string ShippingQueryId
    {
        get => _shippingQueryId;
        set { OnPropertyChanging(nameof(ShippingQueryId)); _shippingQueryId = value; OnPropertyChanged(nameof(ShippingQueryId)); }
    }
}