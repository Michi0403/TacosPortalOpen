//-----------------------------------------------------------------------
// <copyright file="TelegramStarAmount.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramStarAmount : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _amount;
    private int? _nanostarAmount;

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





    public virtual int? NanostarAmount
    {
        get => _nanostarAmount;
        set { OnPropertyChanging(nameof(NanostarAmount)); _nanostarAmount = value; OnPropertyChanged(nameof(NanostarAmount)); }
    }
}
