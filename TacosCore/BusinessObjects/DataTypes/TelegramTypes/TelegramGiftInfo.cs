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
public partial class TelegramGiftInfo : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _canBeUpgraded;
    private long? _convertStarCount;
    private TelegramGift _gift = null!;

    private Guid? _giftId;
    private bool _isPrivate;
    private string _ownedGiftId = string.Empty;
    private long? _prepaidUpgradeStarCount;
    private string _text = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool CanBeUpgraded
    {
        get => _canBeUpgraded;
        set { OnPropertyChanging(nameof(CanBeUpgraded)); _canBeUpgraded = value; OnPropertyChanged(nameof(CanBeUpgraded)); }
    }

    public virtual long? ConvertStarCount
    {
        get => _convertStarCount;
        set { OnPropertyChanging(nameof(ConvertStarCount)); _convertStarCount = value; OnPropertyChanged(nameof(ConvertStarCount)); }
    }

    [InverseProperty(nameof(TelegramMessageEntity.GiftInfoThisMessageEntityBelongsTo))]
    public virtual IList<TelegramMessageEntity>? Entities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(Gift.GiftInfoThisGiftinfoBelongsTo))]
    public virtual TelegramGift Gift
    {
        get => _gift;
        set { OnPropertyChanging(nameof(Gift)); _gift = value; OnPropertyChanged(nameof(Gift)); }
    }

    [ForeignKey("Gift")]
    public virtual Guid? GiftID
    {
        get => _giftId;
        set { OnPropertyChanging(nameof(GiftID)); _giftId = value; OnPropertyChanged(nameof(GiftID)); }
    }

    public virtual bool IsPrivate
    {
        get => _isPrivate;
        set { OnPropertyChanging(nameof(IsPrivate)); _isPrivate = value; OnPropertyChanged(nameof(IsPrivate)); }
    }

    [InverseProperty(nameof(TelegramMessage.Gift))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisGiftinfoBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual string OwnedGiftId
    {
        get => _ownedGiftId;
        set { OnPropertyChanging(nameof(OwnedGiftId)); _ownedGiftId = value; OnPropertyChanged(nameof(OwnedGiftId)); }
    }

    public virtual long? PrepaidUpgradeStarCount
    {
        get => _prepaidUpgradeStarCount;
        set { OnPropertyChanging(nameof(PrepaidUpgradeStarCount)); _prepaidUpgradeStarCount = value; OnPropertyChanged(nameof(PrepaidUpgradeStarCount)); }
    }

    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }
}
