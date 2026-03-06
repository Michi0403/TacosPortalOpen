//-----------------------------------------------------------------------
// <copyright file="TelegramChatLocation.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatLocation : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _address = string.Empty;
    private TelegramLocation _location = null!;

    private Guid? _locationID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Address
    {
        get => _address;
        set { OnPropertyChanging(nameof(Address)); _address = value; OnPropertyChanged(nameof(Address)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramLocation Location
    {
        get => _location;
        set { OnPropertyChanging(nameof(Location)); _location = value; OnPropertyChanged(nameof(Location)); }
    }

    [ForeignKey("Location")]
    public virtual Guid? LocationID
    {
        get => _locationID;
        set { OnPropertyChanging(nameof(LocationID)); _locationID = value; OnPropertyChanged(nameof(LocationID)); }
    }
}
