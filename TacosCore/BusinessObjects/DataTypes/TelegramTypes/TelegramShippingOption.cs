//-----------------------------------------------------------------------
// <copyright file="TelegramShippingOption.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramShippingOption : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _telegramShippingOptionsId = string.Empty;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramLabeledPrice>? Prices { get; set; } = new ObservableCollection<TelegramLabeledPrice>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string TelegramShippingOptionsId
    {
        get => _telegramShippingOptionsId;
        set { OnPropertyChanging(nameof(TelegramShippingOptionsId)); _telegramShippingOptionsId = value; OnPropertyChanged(nameof(TelegramShippingOptionsId)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
}
