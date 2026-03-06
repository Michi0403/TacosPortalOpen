//-----------------------------------------------------------------------
// <copyright file="TelegramStoryAreaPosition.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramStoryAreaPosition : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private double _cornerRadiusPercentage;
    private double _heightPercentage;
    private double _rotationAngle;
    private double _widthPercentage;

    private double _xPercentage;
    private double _yPercentage;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double CornerRadiusPercentage
    {
        get => _cornerRadiusPercentage;
        set { OnPropertyChanging(nameof(CornerRadiusPercentage)); _cornerRadiusPercentage = value; OnPropertyChanged(nameof(CornerRadiusPercentage)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double HeightPercentage
    {
        get => _heightPercentage;
        set { OnPropertyChanging(nameof(HeightPercentage)); _heightPercentage = value; OnPropertyChanged(nameof(HeightPercentage)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double RotationAngle
    {
        get => _rotationAngle;
        set { OnPropertyChanging(nameof(RotationAngle)); _rotationAngle = value; OnPropertyChanged(nameof(RotationAngle)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double WidthPercentage
    {
        get => _widthPercentage;
        set { OnPropertyChanging(nameof(WidthPercentage)); _widthPercentage = value; OnPropertyChanged(nameof(WidthPercentage)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double XPercentage
    {
        get => _xPercentage;
        set { OnPropertyChanging(nameof(XPercentage)); _xPercentage = value; OnPropertyChanged(nameof(XPercentage)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double YPercentage
    {
        get => _yPercentage;
        set { OnPropertyChanging(nameof(YPercentage)); _yPercentage = value; OnPropertyChanged(nameof(YPercentage)); }
    }
}
