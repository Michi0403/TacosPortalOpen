//-----------------------------------------------------------------------
// <copyright file="TelegramMaskPosition.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramMaskPosition : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private MaskPositionPoint _point;
    private double _scale;
    private double _xShift;
    private double _yShift;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual MaskPositionPoint Point
    {
        get => _point;
        set
        {
            if (_point != value)
            {
                OnPropertyChanging(nameof(Point));
                _point = value;
                OnPropertyChanged(nameof(Point));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Scale
    {
        get => _scale;
        set
        {
            if (Math.Abs(_scale - value) > double.Epsilon)
            {
                OnPropertyChanging(nameof(Scale));
                _scale = value;
                OnPropertyChanged(nameof(Scale));
            }
        }
    }

    [InverseProperty(nameof(TelegramSticker.MaskPosition))]
    public virtual IList<TelegramSticker>? StickerThisMaskPositionBelongsTo { get; set; } = new ObservableCollection<TelegramSticker>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double XShift
    {
        get => _xShift;
        set
        {
            if (Math.Abs(_xShift - value) > double.Epsilon)
            {
                OnPropertyChanging(nameof(XShift));
                _xShift = value;
                OnPropertyChanged(nameof(XShift));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double YShift
    {
        get => _yShift;
        set
        {
            if (Math.Abs(_yShift - value) > double.Epsilon)
            {
                OnPropertyChanging(nameof(YShift));
                _yShift = value;
                OnPropertyChanged(nameof(YShift));
            }
        }
    }
}
