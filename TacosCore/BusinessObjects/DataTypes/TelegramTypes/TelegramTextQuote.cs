using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[DefaultClassOptions]
[Authorize]
public partial class TelegramTextQuote : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _isManual;
    private int _position;

    private string _text = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [InverseProperty(nameof(TelegramMessageEntity.TextQuoteThisMessageEntityBelongsTo))]
    [JsonIgnore]
    public virtual IList<TelegramMessageEntity>? Entities { get; set; } = new ObservableCollection<TelegramMessageEntity>();


    public virtual bool IsManual
    {
        get => _isManual;
        set { OnPropertyChanging(nameof(IsManual)); _isManual = value; OnPropertyChanged(nameof(IsManual)); }
    }

    [InverseProperty(nameof(TelegramMessage.Quote))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisTextquoteBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Position
    {
        get => _position;
        set { OnPropertyChanging(nameof(Position)); _position = value; OnPropertyChanged(nameof(Position)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }
}
