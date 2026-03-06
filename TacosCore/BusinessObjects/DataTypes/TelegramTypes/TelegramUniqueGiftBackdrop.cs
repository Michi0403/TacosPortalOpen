//-----------------------------------------------------------------------
// <copyright file="TelegramUniqueGiftBackdrop.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUniqueGiftBackdrop : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUniqueGiftBackdropColors _colors = null!;
    private Guid? _colorsId;

    private string _name = string.Empty;
    private int _rarityPerMille;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUniqueGiftBackdropColors.UniqueGiftBackdropThisUniqueGiftBackdropColorsBelongsTo))]
    public virtual TelegramUniqueGiftBackdropColors Colors
    {
        get => _colors;
        set
        {
            if (_colors != value)
            {
                OnPropertyChanging(nameof(Colors));
                _colors = value;
                OnPropertyChanged(nameof(Colors));
            }
        }
    }


    [ForeignKey("Colors")]
    public virtual Guid? ColorsID
    {
        get => _colorsId;
        set
        {
            if (_colorsId != value)
            {
                OnPropertyChanging(nameof(ColorsID));
                _colorsId = value;
                OnPropertyChanged(nameof(ColorsID));
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
    public virtual int RarityPerMille
    {
        get => _rarityPerMille;
        set
        {
            if (_rarityPerMille != value)
            {
                OnPropertyChanging(nameof(RarityPerMille));
                _rarityPerMille = value;
                OnPropertyChanged(nameof(RarityPerMille));
            }
        }
    }

    [InverseProperty(nameof(TelegramUniqueGift.Backdrop))]
    public virtual IList<TelegramUniqueGift>? UniqueGiftThisUniqueGiftBackdropBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGift>();
}