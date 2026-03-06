//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessOpeningHours.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramBusinessOpeningHours : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private IList<TelegramBusinessOpeningHoursInterval> _openingHours = new ObservableCollection<TelegramBusinessOpeningHoursInterval>();

    private string _timeZoneName = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramBusinessOpeningHoursInterval> OpeningHours
    {
        get => _openingHours;
        set
        {
            OnPropertyChanging(nameof(OpeningHours));
            _openingHours = value;
            OnPropertyChanged(nameof(OpeningHours));
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string TimeZoneName
    {
        get => _timeZoneName;
        set
        {
            OnPropertyChanging(nameof(TimeZoneName));
            _timeZoneName = value;
            OnPropertyChanged(nameof(TimeZoneName));
        }
    }
}
