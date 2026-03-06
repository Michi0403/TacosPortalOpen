
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramInputStoryContent : BaseObject
{
    public abstract InputStoryContentType Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputStoryContentPhoto : TelegramInputStoryContent, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramInputFile _photo = null!;

    private Guid? _photoId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public required virtual TelegramInputFile Photo
    {
        get => _photo;
        set { OnPropertyChanging(nameof(Photo)); _photo = value; OnPropertyChanged(nameof(Photo)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("Photo")]
    public virtual Guid? PhotoID
    {
        get => _photoId;
        set { OnPropertyChanging(nameof(PhotoID)); _photoId = value; OnPropertyChanged(nameof(PhotoID)); }
    }

    public override InputStoryContentType Type => InputStoryContentType.Photo;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInputStoryContentVideo : TelegramInputStoryContent, INotifyPropertyChanging, INotifyPropertyChanged
{
    private double? _coverFrameTimestamp;
    private double _duration;
    private bool _isAnimation;
    private TelegramInputFile _video = null!;

    private Guid? _videoId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual double? CoverFrameTimestamp
    {
        get => _coverFrameTimestamp;
        set { OnPropertyChanging(nameof(CoverFrameTimestamp)); _coverFrameTimestamp = value; OnPropertyChanged(nameof(CoverFrameTimestamp)); }
    }

    public virtual double Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }

    public virtual bool IsAnimation
    {
        get => _isAnimation;
        set { OnPropertyChanging(nameof(IsAnimation)); _isAnimation = value; OnPropertyChanged(nameof(IsAnimation)); }
    }

    public override InputStoryContentType Type => InputStoryContentType.Video;

    public required virtual TelegramInputFile Video
    {
        get => _video;
        set { OnPropertyChanging(nameof(Video)); _video = value; OnPropertyChanged(nameof(Video)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("Video")]
    public virtual Guid? VideoID
    {
        get => _videoId;
        set { OnPropertyChanging(nameof(VideoID)); _videoId = value; OnPropertyChanged(nameof(VideoID)); }
    }
}
