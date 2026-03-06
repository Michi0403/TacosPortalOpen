//-----------------------------------------------------------------------
// <copyright file="TelegramUniqueGift.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUniqueGift : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUniqueGiftBackdrop _backdrop = null!;

    private string _baseName = string.Empty;
    private TelegramUniqueGiftModel _model = null!;
    private Guid? _modelId;
    private string _name = string.Empty;
    private int _number;
    private TelegramUniqueGiftSymbol _symbol = null!;
    private Guid? _symbolId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUniqueGiftBackdrop.UniqueGiftThisUniqueGiftBackdropBelongsTo))]
    public virtual TelegramUniqueGiftBackdrop Backdrop
    {
        get => _backdrop;
        set
        {
            if (_backdrop != value)
            {
                OnPropertyChanging(nameof(Backdrop));
                _backdrop = value;
                OnPropertyChanged(nameof(Backdrop));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string BaseName
    {
        get => _baseName;
        set
        {
            if (_baseName != value)
            {
                OnPropertyChanging(nameof(BaseName));
                _baseName = value;
                OnPropertyChanged(nameof(BaseName));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUniqueGiftModel.UniqueGiftThisUniqueGiftModelBelongsTo))]
    public virtual TelegramUniqueGiftModel Model
    {
        get => _model;
        set
        {
            if (_model != value)
            {
                OnPropertyChanging(nameof(Model));
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
    }

    [ForeignKey("Model")]
    public virtual Guid? ModelID
    {
        get => _modelId;
        set
        {
            if (_modelId != value)
            {
                OnPropertyChanging(nameof(ModelID));
                _modelId = value;
                OnPropertyChanged(nameof(ModelID));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                OnPropertyChanging(nameof(Name));
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Number
    {
        get => _number;
        set
        {
            if (_number != value)
            {
                OnPropertyChanging(nameof(Number));
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUniqueGiftSymbol.UniqueGiftThisUniqueGiftSymbolBelongsTo))]
    public virtual TelegramUniqueGiftSymbol Symbol
    {
        get => _symbol;
        set
        {
            if (_symbol != value)
            {
                OnPropertyChanging(nameof(Symbol));
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }
    }

    [ForeignKey("Symbol")]
    public virtual Guid? SymbolID
    {
        get => _symbolId;
        set
        {
            if (_symbolId != value)
            {
                OnPropertyChanging(nameof(SymbolID));
                _symbolId = value;
                OnPropertyChanged(nameof(SymbolID));
            }
        }
    }

    [InverseProperty(nameof(TelegramUniqueGiftInfo.Gift))]
    public virtual IList<TelegramUniqueGiftInfo>? UniqueGiftInfoThisUniqueGiftBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGiftInfo>();
}