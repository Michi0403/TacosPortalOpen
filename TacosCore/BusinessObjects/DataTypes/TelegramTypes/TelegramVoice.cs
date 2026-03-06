using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramVoice : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _duration;
    private string _mimeType = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Duration
    {
        get => _duration;
        set
        {
            if (_duration != value)
            {
                OnPropertyChanging(nameof(Duration));
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Voice))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisVoiceBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.Voice))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisVoiceBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    public virtual string MimeType
    {
        get => _mimeType;
        set
        {
            if (_mimeType != value)
            {
                OnPropertyChanging(nameof(MimeType));
                _mimeType = value;
                OnPropertyChanged(nameof(MimeType));
            }
        }
    }
}