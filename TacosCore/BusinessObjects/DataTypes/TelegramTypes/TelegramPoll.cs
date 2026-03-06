
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
[Index(nameof(PollId), IsUnique = true)]
public partial class TelegramPoll : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _allowsMultipleAnswers;
    private DateTime? _closeDate;
    private int? _correctOptionId;
    private string _explanation = string.Empty;
    private bool _isAnonymous;
    private bool _isClosed;
    private int? _openPeriod;

    private string _pollId = string.Empty;
    private PollType _pollType;
    private string _question = string.Empty;
    private int _totalVoterCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual bool AllowsMultipleAnswers
    {
        get => _allowsMultipleAnswers;
        set { OnPropertyChanging(nameof(AllowsMultipleAnswers)); _allowsMultipleAnswers = value; OnPropertyChanged(nameof(AllowsMultipleAnswers)); }
    }

    public virtual DateTime? CloseDate
    {
        get => _closeDate;
        set { OnPropertyChanging(nameof(CloseDate)); _closeDate = value; OnPropertyChanged(nameof(CloseDate)); }
    }

    public virtual int? CorrectOptionId
    {
        get => _correctOptionId;
        set { OnPropertyChanging(nameof(CorrectOptionId)); _correctOptionId = value; OnPropertyChanged(nameof(CorrectOptionId)); }
    }

    public virtual string Explanation
    {
        get => _explanation;
        set { OnPropertyChanging(nameof(Explanation)); _explanation = value; OnPropertyChanged(nameof(Explanation)); }
    }

    [InverseProperty(nameof(TelegramMessageEntity.PollExplanationEntitiesToThisPoll))]
    public virtual IList<TelegramMessageEntity>? ExplanationEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [InverseProperty(nameof(TelegramExternalReplyInfo.Poll))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisPollBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    public virtual bool IsAnonymous
    {
        get => _isAnonymous;
        set { OnPropertyChanging(nameof(IsAnonymous)); _isAnonymous = value; OnPropertyChanged(nameof(IsAnonymous)); }
    }

    public virtual bool IsClosed
    {
        get => _isClosed;
        set { OnPropertyChanging(nameof(IsClosed)); _isClosed = value; OnPropertyChanged(nameof(IsClosed)); }
    }

    [InverseProperty(nameof(TelegramMessage.Poll))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisPollBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual int? OpenPeriod
    {
        get => _openPeriod;
        set { OnPropertyChanging(nameof(OpenPeriod)); _openPeriod = value; OnPropertyChanged(nameof(OpenPeriod)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramPollOption.PollToPollOptions))]
    public virtual IList<TelegramPollOption>? Options { get; set; } = new ObservableCollection<TelegramPollOption>();

    [InverseProperty(nameof(TelegramPollAnswer.Poll))]
    [JsonIgnore]
    public virtual IList<TelegramPollAnswer>? PollAnswerThisPollBelongsTo { get; set; } = new ObservableCollection<TelegramPollAnswer>();

    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PollId
    {
        get => _pollId;
        set { OnPropertyChanging(nameof(PollId)); _pollId = value; OnPropertyChanged(nameof(PollId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual PollType PollType
    {
        get => _pollType;
        set { OnPropertyChanging(nameof(PollType)); _pollType = value; OnPropertyChanged(nameof(PollType)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Question
    {
        get => _question;
        set { OnPropertyChanging(nameof(Question)); _question = value; OnPropertyChanged(nameof(Question)); }
    }

    [InverseProperty(nameof(TelegramMessageEntity.PollQuestionEntitiesToThisPoll))]
    public virtual IList<TelegramMessageEntity>? QuestionEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TotalVoterCount
    {
        get => _totalVoterCount;
        set { OnPropertyChanging(nameof(TotalVoterCount)); _totalVoterCount = value; OnPropertyChanged(nameof(TotalVoterCount)); }
    }
}
