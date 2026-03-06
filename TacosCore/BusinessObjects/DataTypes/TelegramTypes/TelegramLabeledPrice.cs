//-----------------------------------------------------------------------
// <copyright file="TelegramLabeledPrice.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramLabeledPrice : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _amount;

    private string _label = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                OnPropertyChanging(nameof(Amount));
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Label
    {
        get => _label;
        set
        {
            if (_label != value)
            {
                OnPropertyChanging(nameof(Label));
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }
    }
}
