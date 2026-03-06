
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramSticker : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private StickerType _type;
    private int _width;
    private int _height;
    private bool _isAnimated;
    private bool _isVideo;
    private TelegramPhotoSize _thumbnail = null!;
    private string _emoji = string.Empty;
    private string _setName = string.Empty;
    private TelegramTGFile _premiumAnimation = null!;
    private TelegramMaskPosition _maskPosition = null!;
    private string _customEmojiId = string.Empty;
    private bool _needsRepainting;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual StickerType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public virtual bool IsAnimated
    {
        get => _isAnimated;
        set { OnPropertyChanging(nameof(IsAnimated)); _isAnimated = value; OnPropertyChanged(nameof(IsAnimated)); }
    }

    public virtual bool IsVideo
    {
        get => _isVideo;
        set { OnPropertyChanging(nameof(IsVideo)); _isVideo = value; OnPropertyChanged(nameof(IsVideo)); }
    }

    [ForeignKey("Thumbnail")]
    public virtual Guid? ThumbnailID { get; set; }

    [InverseProperty(nameof(TelegramPhotoSize.Stickers))]
    public virtual TelegramPhotoSize Thumbnail
    {
        get => _thumbnail;
        set { OnPropertyChanging(nameof(Thumbnail)); _thumbnail = value; OnPropertyChanged(nameof(Thumbnail)); }
    }

    public virtual string Emoji
    {
        get => _emoji;
        set { OnPropertyChanging(nameof(Emoji)); _emoji = value; OnPropertyChanged(nameof(Emoji)); }
    }

    public virtual string SetName
    {
        get => _setName;
        set { OnPropertyChanging(nameof(SetName)); _setName = value; OnPropertyChanged(nameof(SetName)); }
    }

    [ForeignKey("PremiumAnimation")]
    public virtual Guid? PremiumAnimationID { get; set; }

    [InverseProperty(nameof(TelegramTGFile.StickerThisTGFileBelongsTo))]
    public virtual TelegramTGFile PremiumAnimation
    {
        get => _premiumAnimation;
        set { OnPropertyChanging(nameof(PremiumAnimation)); _premiumAnimation = value; OnPropertyChanged(nameof(PremiumAnimation)); }
    }

    [ForeignKey("MaskPosition")]
    public virtual Guid? MaskPositionID { get; set; }

    [InverseProperty(nameof(MaskPosition.StickerThisMaskPositionBelongsTo))]
    public virtual TelegramMaskPosition MaskPosition
    {
        get => _maskPosition;
        set { OnPropertyChanging(nameof(MaskPosition)); _maskPosition = value; OnPropertyChanged(nameof(MaskPosition)); }
    }

    public virtual string CustomEmojiId
    {
        get => _customEmojiId;
        set { OnPropertyChanging(nameof(CustomEmojiId)); _customEmojiId = value; OnPropertyChanged(nameof(CustomEmojiId)); }
    }

    public virtual bool NeedsRepainting
    {
        get => _needsRepainting;
        set { OnPropertyChanging(nameof(NeedsRepainting)); _needsRepainting = value; OnPropertyChanged(nameof(NeedsRepainting)); }
    }

    [InverseProperty(nameof(TelegramMessage.Sticker))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisStickerBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramExternalReplyInfo.Sticker))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisStickerBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramGift.Sticker))]
    [JsonIgnore]
    public virtual IList<TelegramGift>? GiftThisStickerBelongsTo { get; set; } = new ObservableCollection<TelegramGift>();

    [InverseProperty(nameof(TelegramUniqueGiftModel.Sticker))]
    [JsonIgnore]
    public virtual IList<TelegramUniqueGiftModel>? UniqueGiftModelThisStickerBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGiftModel>();

    [InverseProperty(nameof(TelegramUniqueGiftSymbol.Sticker))]
    [JsonIgnore]
    public virtual IList<TelegramUniqueGiftSymbol>? UniqueGiftSymbolThisStickerBelongsTo { get; set; } = new ObservableCollection<TelegramUniqueGiftSymbol>();
}