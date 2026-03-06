//-----------------------------------------------------------------------
// <copyright file="TelegramDice.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramDice : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _emoji = string.Empty;
    private int _value;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Emoji
    {
        get => _emoji;
        set { OnPropertyChanging(nameof(Emoji)); _emoji = value; OnPropertyChanged(nameof(Emoji)); }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Dice))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisDiceBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.Dice))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisDiceBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();





    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Value
    {
        get => _value;
        set { OnPropertyChanging(nameof(Value)); _value = value; OnPropertyChanged(nameof(Value)); }
    }
}
