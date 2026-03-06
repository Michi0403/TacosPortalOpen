//-----------------------------------------------------------------------
// <copyright file="TelegramOrderInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramOrderInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _email = string.Empty;

    private string _name = string.Empty;
    private string _phoneNumber = string.Empty;
    private TelegramShippingAddress _shippingAddress = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual string Email
    {
        get => _email;
        set { OnPropertyChanging(nameof(Email)); _email = value; OnPropertyChanged(nameof(Email)); }
    }


    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }


    public virtual string PhoneNumber
    {
        get => _phoneNumber;
        set { OnPropertyChanging(nameof(PhoneNumber)); _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
    }

    [InverseProperty(nameof(TelegramPreCheckoutQuery.OrderInfo))]
    public virtual IList<TelegramPreCheckoutQuery>? PreCheckoutQueryThisOrderInfoBelongsTo { get; set; } = new ObservableCollection<TelegramPreCheckoutQuery>();


    [InverseProperty(nameof(ShippingAddress.OrderInfoThisShippingAddressBelongsTo))]
    public virtual TelegramShippingAddress ShippingAddress
    {
        get => _shippingAddress;
        set { OnPropertyChanging(nameof(ShippingAddress)); _shippingAddress = value; OnPropertyChanged(nameof(ShippingAddress)); }
    }

    [InverseProperty(nameof(TelegramSuccessfulPayment.OrderInfo))]
    public virtual IList<TelegramSuccessfulPayment>? SuccessfulPaymentThisOrderInfoBelongsTo { get; set; } = new ObservableCollection<TelegramSuccessfulPayment>();
}
