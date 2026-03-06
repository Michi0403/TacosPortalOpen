//-----------------------------------------------------------------------
// <copyright file="TelegramBotShortDescription.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramBotShortDescription : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _shortDescription = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ShortDescription
    {
        get => _shortDescription;
        set
        {
            OnPropertyChanging(nameof(ShortDescription));
            _shortDescription = value;
            OnPropertyChanged(nameof(ShortDescription));
        }
    }
}
