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
public partial class TelegramWriteAccessAllowed : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _fromAttachmentMenu;

    private bool _fromRequest;
    private string _webAppName = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual bool FromAttachmentMenu
    {
        get => _fromAttachmentMenu;
        set
        {
            if (_fromAttachmentMenu != value)
            {
                OnPropertyChanging(nameof(FromAttachmentMenu));
                _fromAttachmentMenu = value;
                OnPropertyChanged(nameof(FromAttachmentMenu));
            }
        }
    }


    public virtual bool FromRequest
    {
        get => _fromRequest;
        set
        {
            if (_fromRequest != value)
            {
                OnPropertyChanging(nameof(FromRequest));
                _fromRequest = value;
                OnPropertyChanged(nameof(FromRequest));
            }
        }
    }


    [InverseProperty(nameof(TelegramMessage.WriteAccessAllowed))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisWriteAccessAllowedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    public virtual string WebAppName
    {
        get => _webAppName;
        set
        {
            if (_webAppName != value)
            {
                OnPropertyChanging(nameof(WebAppName));
                _webAppName = value;
                OnPropertyChanged(nameof(WebAppName));
            }
        }
    }
}