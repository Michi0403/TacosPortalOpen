//-----------------------------------------------------------------------
// <copyright file="TelegramProximityAlertTriggered.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramProximityAlertTriggered : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _distance;

    private TelegramUser _traveler = null!;
    private TelegramUser _watcher = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Distance
    {
        get => _distance;
        set
        {
            OnPropertyChanging(nameof(Distance));
            _distance = value;
            OnPropertyChanged(nameof(Distance));
        }
    }

    [InverseProperty(nameof(TelegramMessage.ProximityAlertTriggered))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageProximityAlertTriggeredBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ProximityAlertTriggeredThisTravelerUserBelongsTo))]
    public virtual TelegramUser Traveler
    {
        get => _traveler;
        set
        {
            OnPropertyChanging(nameof(Traveler));
            _traveler = value;
            OnPropertyChanged(nameof(Traveler));
        }
    }

    [ForeignKey(nameof(Traveler))]
    public virtual Guid? TravelerID { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ProximityAlertTriggeredThisWatcherUserBelongsTo))]
    public virtual TelegramUser Watcher
    {
        get => _watcher;
        set
        {
            OnPropertyChanging(nameof(Watcher));
            _watcher = value;
            OnPropertyChanged(nameof(Watcher));
        }
    }

    [ForeignKey(nameof(Watcher))]
    public virtual Guid? WatcherID { get; set; }
}