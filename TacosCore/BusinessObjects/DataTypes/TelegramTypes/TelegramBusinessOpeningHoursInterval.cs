//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessOpeningHoursInterval.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramBusinessOpeningHoursInterval : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _closingMinute;

    private int _openingMinute;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int ClosingMinute
    {
        get => _closingMinute;
        set
        {
            OnPropertyChanging(nameof(ClosingMinute));
            _closingMinute = value;
            OnPropertyChanged(nameof(ClosingMinute));
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int OpeningMinute
    {
        get => _openingMinute;
        set
        {
            OnPropertyChanging(nameof(OpeningMinute));
            _openingMinute = value;
            OnPropertyChanged(nameof(OpeningMinute));
        }
    }
}
