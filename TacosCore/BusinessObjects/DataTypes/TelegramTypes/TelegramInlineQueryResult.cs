//-----------------------------------------------------------------------
// <copyright file="TelegramInlineQueryResult.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

public abstract partial class TelegramInlineQueryResult : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _inlineQueryResultId = string.Empty;
    private TelegramInlineKeyboardMarkup _replyMarkup = null!;
    private Guid? _replyMarkupId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string InlineQueryResultId
    {
        get => _inlineQueryResultId;
        set { OnPropertyChanging(nameof(InlineQueryResultId)); _inlineQueryResultId = value; OnPropertyChanged(nameof(InlineQueryResultId)); }
    }

    public virtual TelegramInlineKeyboardMarkup ReplyMarkup
    {
        get => _replyMarkup;
        set { OnPropertyChanging(nameof(ReplyMarkup)); _replyMarkup = value; OnPropertyChanged(nameof(ReplyMarkup)); }
    }

    [ForeignKey("ReplyMarkup")]
    public virtual Guid? ReplyMarkupID
    {
        get => _replyMarkupId;
        set { OnPropertyChanging(nameof(ReplyMarkupID)); _replyMarkupId = value; OnPropertyChanged(nameof(ReplyMarkupID)); }
    }
    public abstract InlineQueryResultType Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultArticle : TelegramInlineQueryResult
{
    private string _description = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentId;
    private int? _thumbnailHeight;
    private string _thumbnailUrl = string.Empty;
    private int? _thumbnailWidth;

    private string _title = string.Empty;
    private string _url = string.Empty;

    public virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set { OnPropertyChanging(nameof(InputMessageContent)); _inputMessageContent = value; OnPropertyChanged(nameof(InputMessageContent)); }
    }

    [ForeignKey("InputMessageContent")]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentId;
        set { OnPropertyChanging(nameof(InputMessageContentID)); _inputMessageContentId = value; OnPropertyChanged(nameof(InputMessageContentID)); }
    }

    public virtual int? ThumbnailHeight
    {
        get => _thumbnailHeight;
        set { OnPropertyChanging(nameof(ThumbnailHeight)); _thumbnailHeight = value; OnPropertyChanged(nameof(ThumbnailHeight)); }
    }

    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set { OnPropertyChanging(nameof(ThumbnailUrl)); _thumbnailUrl = value; OnPropertyChanged(nameof(ThumbnailUrl)); }
    }

    public virtual int? ThumbnailWidth
    {
        get => _thumbnailWidth;
        set { OnPropertyChanging(nameof(ThumbnailWidth)); _thumbnailWidth = value; OnPropertyChanged(nameof(ThumbnailWidth)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Article;

    public virtual string Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultPhoto : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private string _description = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private int? _photoHeight;

    private string _photoUrl = string.Empty;
    private int? _photoWidth;
    private bool _showCaptionAboveMedia;
    private string _thumbnailUrl = string.Empty;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set { OnPropertyChanging(nameof(Caption)); _caption = value; OnPropertyChanged(nameof(Caption)); }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set { OnPropertyChanging(nameof(InputMessageContent)); _inputMessageContent = value; OnPropertyChanged(nameof(InputMessageContent)); }
    }

    [ForeignKey("InputMessageContent")]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set { OnPropertyChanging(nameof(InputMessageContentID)); _inputMessageContentID = value; OnPropertyChanged(nameof(InputMessageContentID)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }

    public virtual int? PhotoHeight
    {
        get => _photoHeight;
        set { OnPropertyChanging(nameof(PhotoHeight)); _photoHeight = value; OnPropertyChanged(nameof(PhotoHeight)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PhotoUrl
    {
        get => _photoUrl;
        set { OnPropertyChanging(nameof(PhotoUrl)); _photoUrl = value; OnPropertyChanged(nameof(PhotoUrl)); }
    }

    public virtual int? PhotoWidth
    {
        get => _photoWidth;
        set { OnPropertyChanging(nameof(PhotoWidth)); _photoWidth = value; OnPropertyChanged(nameof(PhotoWidth)); }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set { OnPropertyChanging(nameof(ThumbnailUrl)); _thumbnailUrl = value; OnPropertyChanged(nameof(ThumbnailUrl)); }
    }

    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Photo;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultMpeg4Gif : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private int? _mpeg4Duration;
    private int? _mpeg4Height;

    private string _mpeg4Url = string.Empty;
    private int? _mpeg4Width;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _thumbnailMimeType = string.Empty;
    private string _thumbnailUrl = string.Empty;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual int? Mpeg4Duration
    {
        get => _mpeg4Duration;
        set
        {
            OnPropertyChanging(nameof(Mpeg4Duration));
            _mpeg4Duration = value;
            OnPropertyChanged(nameof(Mpeg4Duration));
        }
    }

    public virtual int? Mpeg4Height
    {
        get => _mpeg4Height;
        set
        {
            OnPropertyChanging(nameof(Mpeg4Height));
            _mpeg4Height = value;
            OnPropertyChanged(nameof(Mpeg4Height));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Mpeg4Url
    {
        get => _mpeg4Url;
        set
        {
            OnPropertyChanging(nameof(Mpeg4Url));
            _mpeg4Url = value;
            OnPropertyChanged(nameof(Mpeg4Url));
        }
    }

    public virtual int? Mpeg4Width
    {
        get => _mpeg4Width;
        set
        {
            OnPropertyChanging(nameof(Mpeg4Width));
            _mpeg4Width = value;
            OnPropertyChanged(nameof(Mpeg4Width));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set
        {
            OnPropertyChanging(nameof(ShowCaptionAboveMedia));
            _showCaptionAboveMedia = value;
            OnPropertyChanged(nameof(ShowCaptionAboveMedia));
        }
    }

    public virtual string ThumbnailMimeType
    {
        get => _thumbnailMimeType;
        set
        {
            OnPropertyChanging(nameof(ThumbnailMimeType));
            _thumbnailMimeType = value;
            OnPropertyChanged(nameof(ThumbnailMimeType));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set
        {
            OnPropertyChanging(nameof(ThumbnailUrl));
            _thumbnailUrl = value;
            OnPropertyChanged(nameof(ThumbnailUrl));
        }
    }

    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Mpeg4Gif;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultGif : TelegramInlineQueryResult, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _caption = string.Empty;
    private IList<TelegramMessageEntity>? _captionEntities = new ObservableCollection<TelegramMessageEntity>();
    private int? _gifDuration;
    private int? _gifHeight;
    private string _gifUrl = string.Empty;
    private int? _gifWidth;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _thumbnailMimeType = string.Empty;
    private string _thumbnailUrl = string.Empty;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string Caption
    {
        get => _caption;
        set { OnPropertyChanging(nameof(Caption)); _caption = value; OnPropertyChanged(nameof(Caption)); }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities
    {
        get => _captionEntities;
    }

    public virtual int? GifDuration
    {
        get => _gifDuration;
        set { OnPropertyChanging(nameof(GifDuration)); _gifDuration = value; OnPropertyChanged(nameof(GifDuration)); }
    }

    public virtual int? GifHeight
    {
        get => _gifHeight;
        set { OnPropertyChanging(nameof(GifHeight)); _gifHeight = value; OnPropertyChanged(nameof(GifHeight)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string GifUrl
    {
        get => _gifUrl;
        set { OnPropertyChanging(nameof(GifUrl)); _gifUrl = value; OnPropertyChanged(nameof(GifUrl)); }
    }

    public virtual int? GifWidth
    {
        get => _gifWidth;
        set { OnPropertyChanging(nameof(GifWidth)); _gifWidth = value; OnPropertyChanged(nameof(GifWidth)); }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set { OnPropertyChanging(nameof(InputMessageContent)); _inputMessageContent = value; OnPropertyChanged(nameof(InputMessageContent)); }
    }

    [ForeignKey("InputMessageContent")]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set { OnPropertyChanging(nameof(InputMessageContentID)); _inputMessageContentID = value; OnPropertyChanged(nameof(InputMessageContentID)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
    }

    public virtual string ThumbnailMimeType
    {
        get => _thumbnailMimeType;
        set { OnPropertyChanging(nameof(ThumbnailMimeType)); _thumbnailMimeType = value; OnPropertyChanged(nameof(ThumbnailMimeType)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set { OnPropertyChanging(nameof(ThumbnailUrl)); _thumbnailUrl = value; OnPropertyChanged(nameof(ThumbnailUrl)); }
    }

    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    public override InlineQueryResultType Type => InlineQueryResultType.Gif;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultVoice : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private string _title = string.Empty;
    private int? _voiceDuration;

    private string _voiceUrl = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Voice;

    public virtual int? VoiceDuration
    {
        get => _voiceDuration;
        set
        {
            OnPropertyChanging(nameof(VoiceDuration));
            _voiceDuration = value;
            OnPropertyChanged(nameof(VoiceDuration));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string VoiceUrl
    {
        get => _voiceUrl;
        set
        {
            OnPropertyChanging(nameof(VoiceUrl));
            _voiceUrl = value;
            OnPropertyChanged(nameof(VoiceUrl));
        }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultAudio : TelegramInlineQueryResult, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int? _audioDuration;
    private string _audioUrl = string.Empty;
    private string _caption = string.Empty;
    private IList<TelegramMessageEntity>? _captionEntities = new ObservableCollection<TelegramMessageEntity>();
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private string _performer = string.Empty;
    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual int? AudioDuration
    {
        get => _audioDuration;
        set { OnPropertyChanging(nameof(AudioDuration)); _audioDuration = value; OnPropertyChanged(nameof(AudioDuration)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string AudioUrl
    {
        get => _audioUrl;
        set { OnPropertyChanging(nameof(AudioUrl)); _audioUrl = value; OnPropertyChanged(nameof(AudioUrl)); }
    }

    public virtual string Caption
    {
        get => _caption;
        set { OnPropertyChanging(nameof(Caption)); _caption = value; OnPropertyChanged(nameof(Caption)); }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities => _captionEntities;

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set { OnPropertyChanging(nameof(InputMessageContent)); _inputMessageContent = value; OnPropertyChanged(nameof(InputMessageContent)); }
    }

    [ForeignKey("InputMessageContent")]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set { OnPropertyChanging(nameof(InputMessageContentID)); _inputMessageContentID = value; OnPropertyChanged(nameof(InputMessageContentID)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }

    public virtual string Performer
    {
        get => _performer;
        set { OnPropertyChanging(nameof(Performer)); _performer = value; OnPropertyChanged(nameof(Performer)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    public override InlineQueryResultType Type => InlineQueryResultType.Audio;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultDocument : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private string _description = string.Empty;

    private string _documentUrl = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private string _mimeType = string.Empty;
    private ParseMode _parseMode;
    private int? _thumbnailHeight;
    private string _thumbnailUrl = string.Empty;
    private int? _thumbnailWidth;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual string Description
    {
        get => _description;
        set
        {
            OnPropertyChanging(nameof(Description));
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string DocumentUrl
    {
        get => _documentUrl;
        set
        {
            OnPropertyChanging(nameof(DocumentUrl));
            _documentUrl = value;
            OnPropertyChanged(nameof(DocumentUrl));
        }
    }

    public virtual TelegramInputMessageContent InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string MimeType
    {
        get => _mimeType;
        set
        {
            OnPropertyChanging(nameof(MimeType));
            _mimeType = value;
            OnPropertyChanged(nameof(MimeType));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    public virtual int? ThumbnailHeight
    {
        get => _thumbnailHeight;
        set
        {
            OnPropertyChanging(nameof(ThumbnailHeight));
            _thumbnailHeight = value;
            OnPropertyChanged(nameof(ThumbnailHeight));
        }
    }

    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set
        {
            OnPropertyChanging(nameof(ThumbnailUrl));
            _thumbnailUrl = value;
            OnPropertyChanged(nameof(ThumbnailUrl));
        }
    }

    public virtual int? ThumbnailWidth
    {
        get => _thumbnailWidth;
        set
        {
            OnPropertyChanging(nameof(ThumbnailWidth));
            _thumbnailWidth = value;
            OnPropertyChanged(nameof(ThumbnailWidth));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Document;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultLocation : TelegramInlineQueryResult
{
    private int? _heading;
    private double? _horizontalAccuracy;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;

    private double _latitude;
    private int? _livePeriod;
    private double _longitude;
    private int? _proximityAlertRadius;
    private int? _thumbnailHeight;
    private string _thumbnailUrl = string.Empty;
    private int? _thumbnailWidth;
    private string _title = string.Empty;

    public virtual int? Heading
    {
        get => _heading;
        set
        {
            OnPropertyChanging(nameof(Heading));
            _heading = value;
            OnPropertyChanged(nameof(Heading));
        }
    }

    public virtual double? HorizontalAccuracy
    {
        get => _horizontalAccuracy;
        set
        {
            OnPropertyChanging(nameof(HorizontalAccuracy));
            _horizontalAccuracy = value;
            OnPropertyChanged(nameof(HorizontalAccuracy));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Latitude
    {
        get => _latitude;
        set
        {
            OnPropertyChanging(nameof(Latitude));
            _latitude = value;
            OnPropertyChanged(nameof(Latitude));
        }
    }

    public virtual int? LivePeriod
    {
        get => _livePeriod;
        set
        {
            OnPropertyChanging(nameof(LivePeriod));
            _livePeriod = value;
            OnPropertyChanged(nameof(LivePeriod));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Longitude
    {
        get => _longitude;
        set
        {
            OnPropertyChanging(nameof(Longitude));
            _longitude = value;
            OnPropertyChanged(nameof(Longitude));
        }
    }

    public virtual int? ProximityAlertRadius
    {
        get => _proximityAlertRadius;
        set
        {
            OnPropertyChanging(nameof(ProximityAlertRadius));
            _proximityAlertRadius = value;
            OnPropertyChanged(nameof(ProximityAlertRadius));
        }
    }

    public virtual int? ThumbnailHeight
    {
        get => _thumbnailHeight;
        set
        {
            OnPropertyChanging(nameof(ThumbnailHeight));
            _thumbnailHeight = value;
            OnPropertyChanged(nameof(ThumbnailHeight));
        }
    }

    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set
        {
            OnPropertyChanging(nameof(ThumbnailUrl));
            _thumbnailUrl = value;
            OnPropertyChanged(nameof(ThumbnailUrl));
        }
    }

    public virtual int? ThumbnailWidth
    {
        get => _thumbnailWidth;
        set
        {
            OnPropertyChanging(nameof(ThumbnailWidth));
            _thumbnailWidth = value;
            OnPropertyChanged(nameof(ThumbnailWidth));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Location;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultVenue : TelegramInlineQueryResult
{
    private string _address = string.Empty;
    private string _foursquareId = string.Empty;
    private string _foursquareType = string.Empty;
    private string _googlePlaceId = string.Empty;
    private string _googlePlaceType = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;

    private double _latitude;
    private double _longitude;
    private int? _thumbnailHeight;
    private string _thumbnailUrl = string.Empty;
    private int? _thumbnailWidth;
    private string _title = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Address
    {
        get => _address;
        set
        {
            OnPropertyChanging(nameof(Address));
            _address = value;
            OnPropertyChanged(nameof(Address));
        }
    }

    public virtual string FoursquareId
    {
        get => _foursquareId;
        set
        {
            OnPropertyChanging(nameof(FoursquareId));
            _foursquareId = value;
            OnPropertyChanged(nameof(FoursquareId));
        }
    }

    public virtual string FoursquareType
    {
        get => _foursquareType;
        set
        {
            OnPropertyChanging(nameof(FoursquareType));
            _foursquareType = value;
            OnPropertyChanged(nameof(FoursquareType));
        }
    }

    public virtual string GooglePlaceId
    {
        get => _googlePlaceId;
        set
        {
            OnPropertyChanging(nameof(GooglePlaceId));
            _googlePlaceId = value;
            OnPropertyChanged(nameof(GooglePlaceId));
        }
    }

    public virtual string GooglePlaceType
    {
        get => _googlePlaceType;
        set
        {
            OnPropertyChanging(nameof(GooglePlaceType));
            _googlePlaceType = value;
            OnPropertyChanged(nameof(GooglePlaceType));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Latitude
    {
        get => _latitude;
        set
        {
            OnPropertyChanging(nameof(Latitude));
            _latitude = value;
            OnPropertyChanged(nameof(Latitude));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual double Longitude
    {
        get => _longitude;
        set
        {
            OnPropertyChanging(nameof(Longitude));
            _longitude = value;
            OnPropertyChanged(nameof(Longitude));
        }
    }

    public virtual int? ThumbnailHeight
    {
        get => _thumbnailHeight;
        set
        {
            OnPropertyChanging(nameof(ThumbnailHeight));
            _thumbnailHeight = value;
            OnPropertyChanged(nameof(ThumbnailHeight));
        }
    }

    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set
        {
            OnPropertyChanging(nameof(ThumbnailUrl));
            _thumbnailUrl = value;
            OnPropertyChanged(nameof(ThumbnailUrl));
        }
    }

    public virtual int? ThumbnailWidth
    {
        get => _thumbnailWidth;
        set
        {
            OnPropertyChanging(nameof(ThumbnailWidth));
            _thumbnailWidth = value;
            OnPropertyChanged(nameof(ThumbnailWidth));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Venue;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultContact : TelegramInlineQueryResult
{
    private string _firstName = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private string _lastName = string.Empty;

    private string _phoneNumber = string.Empty;
    private int? _thumbnailHeight;
    private string _thumbnailUrl = string.Empty;
    private int? _thumbnailWidth;
    private string _vcard = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FirstName
    {
        get => _firstName;
        set
        {
            OnPropertyChanging(nameof(FirstName));
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual string LastName
    {
        get => _lastName;
        set
        {
            OnPropertyChanging(nameof(LastName));
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            OnPropertyChanging(nameof(PhoneNumber));
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }

    public virtual int? ThumbnailHeight
    {
        get => _thumbnailHeight;
        set
        {
            OnPropertyChanging(nameof(ThumbnailHeight));
            _thumbnailHeight = value;
            OnPropertyChanged(nameof(ThumbnailHeight));
        }
    }

    public virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set
        {
            OnPropertyChanging(nameof(ThumbnailUrl));
            _thumbnailUrl = value;
            OnPropertyChanged(nameof(ThumbnailUrl));
        }
    }

    public virtual int? ThumbnailWidth
    {
        get => _thumbnailWidth;
        set
        {
            OnPropertyChanging(nameof(ThumbnailWidth));
            _thumbnailWidth = value;
            OnPropertyChanged(nameof(ThumbnailWidth));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Contact;

    public virtual string Vcard
    {
        get => _vcard;
        set
        {
            OnPropertyChanging(nameof(Vcard));
            _vcard = value;
            OnPropertyChanged(nameof(Vcard));
        }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultGame : TelegramInlineQueryResult
{

    private string _gameShortName = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string GameShortName
    {
        get => _gameShortName;
        set
        {
            OnPropertyChanging(nameof(GameShortName));
            _gameShortName = value;
            OnPropertyChanged(nameof(GameShortName));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Game;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedPhoto : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private string _description = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;

    private string _photoFileId = string.Empty;
    private bool _showCaptionAboveMedia;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual string Description
    {
        get => _description;
        set
        {
            OnPropertyChanging(nameof(Description));
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PhotoFileId
    {
        get => _photoFileId;
        set
        {
            OnPropertyChanging(nameof(PhotoFileId));
            _photoFileId = value;
            OnPropertyChanged(nameof(PhotoFileId));
        }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set
        {
            OnPropertyChanging(nameof(ShowCaptionAboveMedia));
            _showCaptionAboveMedia = value;
            OnPropertyChanged(nameof(ShowCaptionAboveMedia));
        }
    }

    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Photo;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedGif : TelegramInlineQueryResult
{
    private string _caption = string.Empty;

    private string _gifFileId = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string GifFileId
    {
        get => _gifFileId;
        set
        {
            OnPropertyChanging(nameof(GifFileId));
            _gifFileId = value;
            OnPropertyChanged(nameof(GifFileId));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set
        {
            OnPropertyChanging(nameof(ShowCaptionAboveMedia));
            _showCaptionAboveMedia = value;
            OnPropertyChanged(nameof(ShowCaptionAboveMedia));
        }
    }

    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Gif;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedMpeg4Gif : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;

    private string _mpeg4FileId = string.Empty;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Mpeg4FileId
    {
        get => _mpeg4FileId;
        set
        {
            OnPropertyChanging(nameof(Mpeg4FileId));
            _mpeg4FileId = value;
            OnPropertyChanged(nameof(Mpeg4FileId));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set
        {
            OnPropertyChanging(nameof(ShowCaptionAboveMedia));
            _showCaptionAboveMedia = value;
            OnPropertyChanged(nameof(ShowCaptionAboveMedia));
        }
    }

    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Mpeg4Gif;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedSticker : TelegramInlineQueryResult
{
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;

    private string _stickerFileId = string.Empty;

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string StickerFileId
    {
        get => _stickerFileId;
        set
        {
            OnPropertyChanging(nameof(StickerFileId));
            _stickerFileId = value;
            OnPropertyChanged(nameof(StickerFileId));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Sticker;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedDocument : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private string _description = string.Empty;

    private string _documentFileId = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private string _title = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual string Description
    {
        get => _description;
        set
        {
            OnPropertyChanging(nameof(Description));
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string DocumentFileId
    {
        get => _documentFileId;
        set
        {
            OnPropertyChanging(nameof(DocumentFileId));
            _documentFileId = value;
            OnPropertyChanged(nameof(DocumentFileId));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Document;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedVideo : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private string _description = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _title = string.Empty;

    private string _videoFileId = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    public virtual string Description
    {
        get => _description;
        set
        {
            OnPropertyChanging(nameof(Description));
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set
        {
            OnPropertyChanging(nameof(ShowCaptionAboveMedia));
            _showCaptionAboveMedia = value;
            OnPropertyChanged(nameof(ShowCaptionAboveMedia));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string VideoFileId
    {
        get => _videoFileId;
        set
        {
            OnPropertyChanging(nameof(VideoFileId));
            _videoFileId = value;
            OnPropertyChanged(nameof(VideoFileId));
        }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedVoice : TelegramInlineQueryResult
{
    private string _caption = string.Empty;
    private IList<TelegramMessageEntity>? _captionEntities = new ObservableCollection<TelegramMessageEntity>();
    private TelegramInputMessageContent? _inputMessageContent = null!;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;
    private string _title = string.Empty;

    private string _voiceFileId = string.Empty;

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities
    {
        get => _captionEntities;
        set
        {
            OnPropertyChanging(nameof(CaptionEntities));
            _captionEntities = value;
            OnPropertyChanged(nameof(CaptionEntities));
        }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Voice;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string VoiceFileId
    {
        get => _voiceFileId;
        set
        {
            OnPropertyChanging(nameof(VoiceFileId));
            _voiceFileId = value;
            OnPropertyChanged(nameof(VoiceFileId));
        }
    }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultCachedAudio : TelegramInlineQueryResult
{

    private string _audioFileId = string.Empty;
    private string _caption = string.Empty;
    private IList<TelegramMessageEntity>? _captionEntities = new ObservableCollection<TelegramMessageEntity>();
    private TelegramInputMessageContent? _inputMessageContent = null!;
    private Guid? _inputMessageContentID;
    private ParseMode _parseMode;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string AudioFileId
    {
        get => _audioFileId;
        set
        {
            OnPropertyChanging(nameof(AudioFileId));
            _audioFileId = value;
            OnPropertyChanged(nameof(AudioFileId));
        }
    }

    public virtual string Caption
    {
        get => _caption;
        set
        {
            OnPropertyChanging(nameof(Caption));
            _caption = value;
            OnPropertyChanged(nameof(Caption));
        }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities
    {
        get => _captionEntities;
        set
        {
            OnPropertyChanging(nameof(CaptionEntities));
            _captionEntities = value;
            OnPropertyChanged(nameof(CaptionEntities));
        }
    }

    public virtual TelegramInputMessageContent InputMessageContent
    {
        get => _inputMessageContent;
        set
        {
            OnPropertyChanging(nameof(InputMessageContent));
            _inputMessageContent = value;
            OnPropertyChanged(nameof(InputMessageContent));
        }
    }

    [ForeignKey(nameof(InputMessageContent))]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set
        {
            OnPropertyChanging(nameof(InputMessageContentID));
            _inputMessageContentID = value;
            OnPropertyChanged(nameof(InputMessageContentID));
        }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set
        {
            OnPropertyChanging(nameof(ParseMode));
            _parseMode = value;
            OnPropertyChanged(nameof(ParseMode));
        }
    }
    public override InlineQueryResultType Type => InlineQueryResultType.Audio;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQueryResultVideo : TelegramInlineQueryResult, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _caption = string.Empty;
    private IList<TelegramMessageEntity>? _captionEntities = new ObservableCollection<TelegramMessageEntity>();
    private string _description = string.Empty;
    private TelegramInputMessageContent? _inputMessageContent;
    private Guid? _inputMessageContentID;
    private string _mimeType = string.Empty;
    private ParseMode _parseMode;
    private bool _showCaptionAboveMedia;
    private string _thumbnailUrl = string.Empty;
    private string _title = string.Empty;
    private int? _videoDuration;
    private int? _videoHeight;
    private string _videoUrl = string.Empty;
    private int? _videoWidth;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string Caption
    {
        get => _caption;
        set { OnPropertyChanging(nameof(Caption)); _caption = value; OnPropertyChanged(nameof(Caption)); }
    }

    public virtual IList<TelegramMessageEntity>? CaptionEntities
    {
        get => _captionEntities;
    }

    public virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    public virtual TelegramInputMessageContent? InputMessageContent
    {
        get => _inputMessageContent;
        set { OnPropertyChanging(nameof(InputMessageContent)); _inputMessageContent = value; OnPropertyChanged(nameof(InputMessageContent)); }
    }

    [ForeignKey("InputMessageContent")]
    public virtual Guid? InputMessageContentID
    {
        get => _inputMessageContentID;
        set { OnPropertyChanging(nameof(InputMessageContentID)); _inputMessageContentID = value; OnPropertyChanged(nameof(InputMessageContentID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string MimeType
    {
        get => _mimeType;
        set { OnPropertyChanging(nameof(MimeType)); _mimeType = value; OnPropertyChanged(nameof(MimeType)); }
    }

    public virtual ParseMode ParseMode
    {
        get => _parseMode;
        set { OnPropertyChanging(nameof(ParseMode)); _parseMode = value; OnPropertyChanged(nameof(ParseMode)); }
    }

    public virtual bool ShowCaptionAboveMedia
    {
        get => _showCaptionAboveMedia;
        set { OnPropertyChanging(nameof(ShowCaptionAboveMedia)); _showCaptionAboveMedia = value; OnPropertyChanged(nameof(ShowCaptionAboveMedia)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string ThumbnailUrl
    {
        get => _thumbnailUrl;
        set { OnPropertyChanging(nameof(ThumbnailUrl)); _thumbnailUrl = value; OnPropertyChanged(nameof(ThumbnailUrl)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    public override InlineQueryResultType Type => InlineQueryResultType.Video;

    public virtual int? VideoDuration
    {
        get => _videoDuration;
        set { OnPropertyChanging(nameof(VideoDuration)); _videoDuration = value; OnPropertyChanged(nameof(VideoDuration)); }
    }

    public virtual int? VideoHeight
    {
        get => _videoHeight;
        set { OnPropertyChanging(nameof(VideoHeight)); _videoHeight = value; OnPropertyChanged(nameof(VideoHeight)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual string VideoUrl
    {
        get => _videoUrl;
        set { OnPropertyChanging(nameof(VideoUrl)); _videoUrl = value; OnPropertyChanged(nameof(VideoUrl)); }
    }

    public virtual int? VideoWidth
    {
        get => _videoWidth;
        set { OnPropertyChanging(nameof(VideoWidth)); _videoWidth = value; OnPropertyChanged(nameof(VideoWidth)); }
    }
}
