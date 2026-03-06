//-----------------------------------------------------------------------
// <copyright file="TelegramInvoice.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramInvoice : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _currency = string.Empty;
    private string _description = string.Empty;
    private string _startParameter = string.Empty;

    private string _title = string.Empty;
    private long _totalAmount;

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
    public virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Invoice))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisInvoiceBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.Invoice))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisInvoiceBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string StartParameter
    {
        get => _startParameter;
        set { OnPropertyChanging(nameof(StartParameter)); _startParameter = value; OnPropertyChanged(nameof(StartParameter)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual long TotalAmount
    {
        get => _totalAmount;
        set { OnPropertyChanging(nameof(TotalAmount)); _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
    }
}
