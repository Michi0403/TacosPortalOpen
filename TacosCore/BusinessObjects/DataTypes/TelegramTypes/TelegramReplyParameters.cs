
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramReplyParameters : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _allowSendingWithoutReply;
    private TelegramChatId _chatId = null!;

    private int _messageId;
    private string _quote = string.Empty;
    private ParseMode _quoteParseMode;
    private int? _quotePosition;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual bool AllowSendingWithoutReply
    {
        get => _allowSendingWithoutReply;
        set { OnPropertyChanging(nameof(AllowSendingWithoutReply)); _allowSendingWithoutReply = value; OnPropertyChanged(nameof(AllowSendingWithoutReply)); }
    }

    public virtual TelegramChatId ChatId
    {
        get => _chatId;
        set { OnPropertyChanging(nameof(ChatId)); _chatId = value; OnPropertyChanged(nameof(ChatId)); }
    }


    [ForeignKey("ChatId")]
    public virtual Guid? ChatIdID { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int MessageId
    {
        get => _messageId;
        set { OnPropertyChanging(nameof(MessageId)); _messageId = value; OnPropertyChanged(nameof(MessageId)); }
    }


    public virtual string Quote
    {
        get => _quote;
        set { OnPropertyChanging(nameof(Quote)); _quote = value; OnPropertyChanged(nameof(Quote)); }
    }


    public virtual IList<TelegramMessageEntity>? QuoteEntities { get; set; } = new ObservableCollection<TelegramMessageEntity>();


    public virtual ParseMode QuoteParseMode
    {
        get => _quoteParseMode;
        set { OnPropertyChanging(nameof(QuoteParseMode)); _quoteParseMode = value; OnPropertyChanged(nameof(QuoteParseMode)); }
    }


    public virtual int? QuotePosition
    {
        get => _quotePosition;
        set { OnPropertyChanging(nameof(QuotePosition)); _quotePosition = value; OnPropertyChanged(nameof(QuotePosition)); }
    }
}