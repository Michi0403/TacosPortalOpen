//-----------------------------------------------------------------------
// <copyright file="TelegramGift.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramGift : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _giftId = string.Empty;
    private int? _remainingCount;
    private long _starCount;
    private TelegramSticker _sticker = null!;
    private Guid? _stickerId;
    private int? _totalCount;
    private long? _upgradeStarCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string GiftId
    {
        get => _giftId;
        set { OnPropertyChanging(nameof(GiftId)); _giftId = value; OnPropertyChanged(nameof(GiftId)); }
    }

    [InverseProperty(nameof(TelegramGiftInfo.Gift))]
    public virtual IList<TelegramGiftInfo>? GiftInfoThisGiftinfoBelongsTo { get; set; } = new ObservableCollection<TelegramGiftInfo>();

    public virtual int? RemainingCount
    {
        get => _remainingCount;
        set { OnPropertyChanging(nameof(RemainingCount)); _remainingCount = value; OnPropertyChanged(nameof(RemainingCount)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual long StarCount
    {
        get => _starCount;
        set { OnPropertyChanging(nameof(StarCount)); _starCount = value; OnPropertyChanged(nameof(StarCount)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramSticker Sticker
    {
        get => _sticker;
        set { OnPropertyChanging(nameof(Sticker)); _sticker = value; OnPropertyChanged(nameof(Sticker)); }
    }

    [ForeignKey("Sticker")]
    public virtual Guid? StickerID
    {
        get => _stickerId;
        set { OnPropertyChanging(nameof(StickerID)); _stickerId = value; OnPropertyChanged(nameof(StickerID)); }
    }

    public virtual int? TotalCount
    {
        get => _totalCount;
        set { OnPropertyChanging(nameof(TotalCount)); _totalCount = value; OnPropertyChanged(nameof(TotalCount)); }
    }

    public virtual long? UpgradeStarCount
    {
        get => _upgradeStarCount;
        set { OnPropertyChanging(nameof(UpgradeStarCount)); _upgradeStarCount = value; OnPropertyChanged(nameof(UpgradeStarCount)); }
    }
}
