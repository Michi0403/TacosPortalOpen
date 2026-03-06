
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramOwnedGift : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _isSaved;

    private string _ownedGiftId = string.Empty;
    private DateTime _sendDate;
    private TelegramUser _senderUser;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool IsSaved
    {
        get => _isSaved;
        set { OnPropertyChanging(nameof(IsSaved)); _isSaved = value; OnPropertyChanged(nameof(IsSaved)); }
    }
    [Required]
    public virtual string OwnedGiftId
    {
        get => _ownedGiftId;
        set { OnPropertyChanging(nameof(OwnedGiftId)); _ownedGiftId = value; OnPropertyChanged(nameof(OwnedGiftId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime SendDate
    {
        get => _sendDate;
        set { OnPropertyChanging(nameof(SendDate)); _sendDate = value; OnPropertyChanged(nameof(SendDate)); }
    }

    public virtual TelegramUser SenderUser
    {
        get => _senderUser;
        set { OnPropertyChanging(nameof(SenderUser)); _senderUser = value; OnPropertyChanged(nameof(SenderUser)); }
    }

    [ForeignKey("SenderUser")]
    public virtual Guid? SenderUserID { get; set; }
    public abstract OwnedGiftType Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramOwnedGiftRegular : TelegramOwnedGift
{
    private bool _canBeUpgraded;
    private int? _convertStarCount;

    private TelegramGift _gift;
    private bool _isPrivate;
    private int? _prepaidUpgradeStarCount;
    private string _text = string.Empty;
    private bool _wasRefunded;

    public virtual bool CanBeUpgraded
    {
        get => _canBeUpgraded;
        set { OnPropertyChanging(nameof(CanBeUpgraded)); _canBeUpgraded = value; OnPropertyChanged(nameof(CanBeUpgraded)); }
    }

    public virtual int? ConvertStarCount
    {
        get => _convertStarCount;
        set { OnPropertyChanging(nameof(ConvertStarCount)); _convertStarCount = value; OnPropertyChanged(nameof(ConvertStarCount)); }
    }

    public virtual IList<TelegramMessageEntity>? Entities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramGift Gift
    {
        get => _gift;
        set { OnPropertyChanging(nameof(Gift)); _gift = value; OnPropertyChanged(nameof(Gift)); }
    }

    [ForeignKey("Gift")]
    public virtual Guid? GiftID { get; set; }

    public virtual bool IsPrivate
    {
        get => _isPrivate;
        set { OnPropertyChanging(nameof(IsPrivate)); _isPrivate = value; OnPropertyChanged(nameof(IsPrivate)); }
    }

    public virtual int? PrepaidUpgradeStarCount
    {
        get => _prepaidUpgradeStarCount;
        set { OnPropertyChanging(nameof(PrepaidUpgradeStarCount)); _prepaidUpgradeStarCount = value; OnPropertyChanged(nameof(PrepaidUpgradeStarCount)); }
    }

    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }
    public override OwnedGiftType Type => OwnedGiftType.Regular;

    public virtual bool WasRefunded
    {
        get => _wasRefunded;
        set { OnPropertyChanging(nameof(WasRefunded)); _wasRefunded = value; OnPropertyChanged(nameof(WasRefunded)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramOwnedGiftUnique : TelegramOwnedGift
{
    private bool _canBeTransferred;

    private TelegramUniqueGift _gift = null!;
    private int? _transferStarCount;

    public virtual bool CanBeTransferred
    {
        get => _canBeTransferred;
        set { OnPropertyChanging(nameof(CanBeTransferred)); _canBeTransferred = value; OnPropertyChanged(nameof(CanBeTransferred)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramUniqueGift Gift
    {
        get => _gift;
        set { OnPropertyChanging(nameof(Gift)); _gift = value; OnPropertyChanged(nameof(Gift)); }
    }

    [ForeignKey("Gift")]
    public virtual Guid? GiftID { get; set; }

    public virtual int? TransferStarCount
    {
        get => _transferStarCount;
        set { OnPropertyChanging(nameof(TransferStarCount)); _transferStarCount = value; OnPropertyChanged(nameof(TransferStarCount)); }
    }
    public override OwnedGiftType Type => OwnedGiftType.Unique;
}