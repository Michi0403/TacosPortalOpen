//-----------------------------------------------------------------------
// <copyright file="TelegramUniqueGiftInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUniqueGiftInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUniqueGift _gift;

    private Guid? _giftId;
    private string _origin = string.Empty;
    private string _ownedGiftId = string.Empty;
    private long? _transferStarCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramUniqueGift Gift
    {
        get => _gift;
        set
        {
            if (_gift != value)
            {
                OnPropertyChanging(nameof(Gift));
                _gift = value;
                OnPropertyChanged(nameof(Gift));
            }
        }
    }


    [ForeignKey("Gift")]
    public virtual Guid? GiftID
    {
        get => _giftId;
        set
        {
            if (_giftId != value)
            {
                OnPropertyChanging(nameof(GiftID));
                _giftId = value;
                OnPropertyChanged(nameof(GiftID));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.UniqueGift))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageUniqueGiftInfoBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Origin
    {
        get => _origin;
        set
        {
            if (_origin != value)
            {
                OnPropertyChanging(nameof(Origin));
                _origin = value;
                OnPropertyChanged(nameof(Origin));
            }
        }
    }




    public virtual string OwnedGiftId
    {
        get => _ownedGiftId;
        set
        {
            if (_ownedGiftId != value)
            {
                OnPropertyChanging(nameof(OwnedGiftId));
                _ownedGiftId = value;
                OnPropertyChanged(nameof(OwnedGiftId));
            }
        }
    }




    public virtual long? TransferStarCount
    {
        get => _transferStarCount;
        set
        {
            if (_transferStarCount != value)
            {
                OnPropertyChanging(nameof(TransferStarCount));
                _transferStarCount = value;
                OnPropertyChanged(nameof(TransferStarCount));
            }
        }
    }
}