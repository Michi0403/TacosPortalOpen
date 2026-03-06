//-----------------------------------------------------------------------
// <copyright file="TelegramWebAppData.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramWebAppData : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _buttonText = string.Empty;

    private string _data = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ButtonText
    {
        get => _buttonText;
        set
        {
            if (_buttonText != value)
            {
                OnPropertyChanging(nameof(ButtonText));
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Data
    {
        get => _data;
        set
        {
            if (_data != value)
            {
                OnPropertyChanging(nameof(Data));
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.WebAppData))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisWebAppDataBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();
}