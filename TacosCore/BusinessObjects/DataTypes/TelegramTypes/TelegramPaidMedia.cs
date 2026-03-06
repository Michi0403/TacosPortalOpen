
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramPaidMedia : BaseObject
{
    public abstract PaidMediaType Type { get; }
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPaidMediaPreview : TelegramPaidMedia, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _duration;

    private int _height;

    private int _width;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual int Duration
    {
        get => _duration;
        set { OnPropertyChanging(nameof(Duration)); _duration = value; OnPropertyChanged(nameof(Duration)); }
    }
    public virtual int Height
    {
        get => _height;
        set { OnPropertyChanging(nameof(Height)); _height = value; OnPropertyChanged(nameof(Height)); }
    }

    public override PaidMediaType Type => PaidMediaType.Preview;
    public virtual int Width
    {
        get => _width;
        set { OnPropertyChanging(nameof(Width)); _width = value; OnPropertyChanged(nameof(Width)); }
    }
}



[Authorize]
[DefaultClassOptions]
public partial class TelegramPaidMediaPhoto : TelegramPaidMedia
{

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramPhotoSize>? Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();
    public override PaidMediaType Type => PaidMediaType.Photo;
}



[Authorize]
[DefaultClassOptions]
public partial class TelegramPaidMediaVideo : TelegramPaidMedia, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramVideo _video = new();

    private Guid? _videoId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public override PaidMediaType Type => PaidMediaType.Video;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramVideo Video
    {
        get => _video;
        set { OnPropertyChanging(nameof(Video)); _video = value; OnPropertyChanged(nameof(Video)); }
    }

    [ForeignKey("Video")]
    public virtual Guid? VideoID
    {
        get => _videoId;
        set { OnPropertyChanging(nameof(VideoID)); _videoId = value; OnPropertyChanged(nameof(VideoID)); }
    }
}

