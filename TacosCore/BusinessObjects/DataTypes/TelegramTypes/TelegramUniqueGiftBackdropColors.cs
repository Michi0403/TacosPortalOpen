//-----------------------------------------------------------------------
// <copyright file="TelegramUniqueGiftBackdropColors.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUniqueGiftBackdropColors : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _centerColor;
    private int _edgeColor;
    private int _symbolColor;
    private int _textColor;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int CenterColor
    {
        get => _centerColor;
        set
        {
            if (_centerColor != value)
            {
                OnPropertyChanging(nameof(CenterColor));
                _centerColor = value;
                OnPropertyChanged(nameof(CenterColor));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int EdgeColor
    {
        get => _edgeColor;
        set
        {
            if (_edgeColor != value)
            {
                OnPropertyChanging(nameof(EdgeColor));
                _edgeColor = value;
                OnPropertyChanged(nameof(EdgeColor));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int SymbolColor
    {
        get => _symbolColor;
        set
        {
            if (_symbolColor != value)
            {
                OnPropertyChanging(nameof(SymbolColor));
                _symbolColor = value;
                OnPropertyChanged(nameof(SymbolColor));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TextColor
    {
        get => _textColor;
        set
        {
            if (_textColor != value)
            {
                OnPropertyChanging(nameof(TextColor));
                _textColor = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
    }

    [InverseProperty(nameof(TelegramUniqueGiftBackdrop.Colors))]
    public virtual IList<TelegramUniqueGiftBackdrop>? UniqueGiftBackdropThisUniqueGiftBackdropColorsBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGiftBackdrop>();
}