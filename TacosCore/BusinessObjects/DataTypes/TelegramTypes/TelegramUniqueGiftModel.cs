//-----------------------------------------------------------------------
// <copyright file="TelegramUniqueGiftModel.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUniqueGiftModel : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _name = string.Empty;
    private int _rarityPerMille;
    private TelegramSticker _sticker = null!;
    private Guid? _stickerId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


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

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(Sticker.UniqueGiftModelThisStickerBelongsTo))]
    public virtual TelegramSticker Sticker
    {
        get => _sticker;
        set
        {
            if (_sticker != value)
            {
                OnPropertyChanging(nameof(Sticker));
                _sticker = value;
                OnPropertyChanged(nameof(Sticker));
            }
        }
    }


    [ForeignKey("Sticker")]
    public virtual Guid? StickerID
    {
        get => _stickerId;
        set
        {
            if (_stickerId != value)
            {
                OnPropertyChanging(nameof(StickerID));
                _stickerId = value;
                OnPropertyChanged(nameof(StickerID));
            }
        }
    }

    [InverseProperty(nameof(TelegramUniqueGift.Model))]
    public virtual IList<TelegramUniqueGift>? UniqueGiftThisUniqueGiftModelBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGift>();
}