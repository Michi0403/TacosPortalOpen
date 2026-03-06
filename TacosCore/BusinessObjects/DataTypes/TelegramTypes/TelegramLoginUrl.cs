
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramLoginUrl : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _url = string.Empty;
    private string _forwardText = string.Empty;
    private string _botUsername = string.Empty;
    private bool _requestWriteAccess;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Url
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                OnPropertyChanging(nameof(Url));
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }
    }

    public virtual string ForwardText
    {
        get => _forwardText;
        set
        {
            if (_forwardText != value)
            {
                OnPropertyChanging(nameof(ForwardText));
                _forwardText = value;
                OnPropertyChanged(nameof(ForwardText));
            }
        }
    }

    public virtual string BotUsername
    {
        get => _botUsername;
        set
        {
            if (_botUsername != value)
            {
                OnPropertyChanging(nameof(BotUsername));
                _botUsername = value;
                OnPropertyChanged(nameof(BotUsername));
            }
        }
    }

    public virtual bool RequestWriteAccess
    {
        get => _requestWriteAccess;
        set
        {
            if (_requestWriteAccess != value)
            {
                OnPropertyChanging(nameof(RequestWriteAccess));
                _requestWriteAccess = value;
                OnPropertyChanged(nameof(RequestWriteAccess));
            }
        }
    }
}