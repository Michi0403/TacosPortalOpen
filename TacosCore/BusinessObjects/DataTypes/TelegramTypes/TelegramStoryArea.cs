//-----------------------------------------------------------------------
// <copyright file="TelegramStoryArea.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramStoryArea : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramStoryAreaPosition _position = null!;
    private TelegramStoryAreaType _type = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramStoryAreaPosition Position
    {
        get => _position;
        set { OnPropertyChanging(nameof(Position)); _position = value; OnPropertyChanged(nameof(Position)); }
    }

    [ForeignKey("Position")]
    public virtual Guid? PositionID { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramStoryAreaType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }

    [ForeignKey("Type")]
    public virtual Guid? TypeID { get; set; }
}