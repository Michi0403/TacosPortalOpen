//-----------------------------------------------------------------------
// <copyright file="TelegramChatBoost.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatBoost : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private DateTime _addDate;

    private string _boostId = string.Empty;
    private DateTime _expirationDate;
    private TelegramChatBoostSource? _source;
    private Guid? _sourceID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime AddDate
    {
        get => _addDate;
        set { OnPropertyChanging(nameof(AddDate)); _addDate = value; OnPropertyChanged(nameof(AddDate)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string BoostId
    {
        get => _boostId;
        set { OnPropertyChanging(nameof(BoostId)); _boostId = value; OnPropertyChanged(nameof(BoostId)); }
    }

    [InverseProperty(nameof(TelegramChatBoostUpdated.Boost))]
    public virtual IList<TelegramChatBoostUpdated>? ChatBoostUpdatedThisChatBoostBelongsTo { get; set; } = new ObservableCollection<TelegramChatBoostUpdated>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime ExpirationDate
    {
        get => _expirationDate;
        set { OnPropertyChanging(nameof(ExpirationDate)); _expirationDate = value; OnPropertyChanged(nameof(ExpirationDate)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChatBoostSource.ChatBoostThisChatBoostSourceBelongsTo))]
    public virtual TelegramChatBoostSource? Source
    {
        get => _source;
        set { OnPropertyChanging(nameof(Source)); _source = value; OnPropertyChanged(nameof(Source)); }
    }

    [ForeignKey("Source")]
    public virtual Guid? SourceID
    {
        get => _sourceID;
        set { OnPropertyChanging(nameof(SourceID)); _sourceID = value; OnPropertyChanged(nameof(SourceID)); }
    }
}
