
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
public partial class TelegramPollOption : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramPoll _pollToPollOptions = null!;
    private Guid? _pollToPollOptionsID;

    private string _text = string.Empty;
    private int _voterCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramPoll.Options))]
    public virtual TelegramPoll PollToPollOptions
    {
        get => _pollToPollOptions;
        set { OnPropertyChanging(nameof(PollToPollOptions)); _pollToPollOptions = value; OnPropertyChanged(nameof(PollToPollOptions)); }
    }

    [ForeignKey("PollToPollOptions")]
    public virtual Guid? PollToPollOptionsID
    {
        get => _pollToPollOptionsID;
        set { OnPropertyChanging(nameof(PollToPollOptionsID)); _pollToPollOptionsID = value; OnPropertyChanged(nameof(PollToPollOptionsID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public virtual IList<TelegramMessageEntity>? TextEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int VoterCount
    {
        get => _voterCount;
        set { OnPropertyChanging(nameof(VoterCount)); _voterCount = value; OnPropertyChanged(nameof(VoterCount)); }
    }
}
