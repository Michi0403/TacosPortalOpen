//-----------------------------------------------------------------------
// <copyright file="TelegramRevenueWithdrawalState.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Payments;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[DefaultClassOptions]
[Authorize]
public abstract partial class TelegramRevenueWithdrawalState : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public abstract RevenueWithdrawalStateType Type { get; }
}


[DefaultClassOptions]
[Authorize]
public partial class TelegramRevenueWithdrawalStateSucceeded : TelegramRevenueWithdrawalState
{
    private DateTime _date;
    private string _url = string.Empty;


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }

    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Succeeded;


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }
}

[DefaultClassOptions]
[Authorize]
public partial class TelegramRevenueWithdrawalStatePending : TelegramRevenueWithdrawalState
{
    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Pending;
}

[DefaultClassOptions]
[Authorize]
public partial class TelegramRevenueWithdrawalStateFailed : TelegramRevenueWithdrawalState
{
    public override RevenueWithdrawalStateType Type => RevenueWithdrawalStateType.Failed;
}