//-----------------------------------------------------------------------
// <copyright file="TelegramPaidMediaPurchased.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPaidMediaPurchased : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUser _from = null!;

    private Guid? _fromID;
    private string _paidMediaPayload = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramUser From
    {
        get => _from;
        set { OnPropertyChanging(nameof(From)); _from = value; OnPropertyChanged(nameof(From)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.PaidMediaPurchasedThisUserBelongsTo))]
    [ForeignKey("From")]
    public virtual Guid? FromID
    {
        get => _fromID;
        set { OnPropertyChanging(nameof(FromID)); _fromID = value; OnPropertyChanged(nameof(FromID)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PaidMediaPayload
    {
        get => _paidMediaPayload;
        set { OnPropertyChanging(nameof(PaidMediaPayload)); _paidMediaPayload = value; OnPropertyChanged(nameof(PaidMediaPayload)); }
    }
}
