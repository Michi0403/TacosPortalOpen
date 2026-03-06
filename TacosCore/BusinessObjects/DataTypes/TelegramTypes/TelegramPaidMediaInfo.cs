//-----------------------------------------------------------------------
// <copyright file="TelegramPaidMediaInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramPaidMediaInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private long _starCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramExternalReplyInfo.PaidMedia))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisPaidMediaInfoBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.PaidMedia))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisPaidMediaInfoBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramPaidMedia>? PaidMedia { get; set; } = new ObservableCollection<TelegramPaidMedia>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual long StarCount
    {
        get => _starCount;
        set { OnPropertyChanging(nameof(StarCount)); _starCount = value; OnPropertyChanged(nameof(StarCount)); }
    }
}