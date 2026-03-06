//-----------------------------------------------------------------------
// <copyright file="TelegramStarTransaction.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramStarTransaction : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _amount;
    private DateTime _date;
    private int? _nanostarAmount;
    private TelegramTransactionPartner _receiver = null!;
    private TelegramTransactionPartner _source = null!;

    private string _starTransactionId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Amount
    {
        get => _amount;
        set { OnPropertyChanging(nameof(Amount)); _amount = value; OnPropertyChanged(nameof(Amount)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }

    public virtual int? NanostarAmount
    {
        get => _nanostarAmount;
        set { OnPropertyChanging(nameof(NanostarAmount)); _nanostarAmount = value; OnPropertyChanged(nameof(NanostarAmount)); }
    }

    public required virtual TelegramTransactionPartner Receiver
    {
        get => _receiver;
        set { OnPropertyChanging(nameof(Receiver)); _receiver = value; OnPropertyChanged(nameof(Receiver)); }
    }

    [ForeignKey("Receiver")]
    public virtual Guid? ReceiverID { get; set; }

    public required virtual TelegramTransactionPartner Source
    {
        get => _source;
        set { OnPropertyChanging(nameof(Source)); _source = value; OnPropertyChanged(nameof(Source)); }
    }

    [ForeignKey("Source")]
    public virtual Guid? SourceID { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string StarTransactionId
    {
        get => _starTransactionId;
        set { OnPropertyChanging(nameof(StarTransactionId)); _starTransactionId = value; OnPropertyChanged(nameof(StarTransactionId)); }
    }
}
