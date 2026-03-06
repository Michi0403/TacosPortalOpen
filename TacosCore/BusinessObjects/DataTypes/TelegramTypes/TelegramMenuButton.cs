
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramMenuButton : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public abstract MenuButtonType Type { get; }
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramMenuButtonCommands : TelegramMenuButton
{
    public override MenuButtonType Type => MenuButtonType.Commands;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramMenuButtonWebApp : TelegramMenuButton
{
    private string _text = string.Empty;
    private TelegramWebAppInfo _webApp = null!;
    private Guid? _webAppID;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public override MenuButtonType Type => MenuButtonType.WebApp;

    public virtual TelegramWebAppInfo WebApp
    {
        get => _webApp;
        set { OnPropertyChanging(nameof(WebApp)); _webApp = value; OnPropertyChanged(nameof(WebApp)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [ForeignKey("WebApp")]
    public virtual Guid? WebAppID
    {
        get => _webAppID;
        set { OnPropertyChanging(nameof(WebAppID)); _webAppID = value; OnPropertyChanged(nameof(WebAppID)); }
    }
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramMenuButtonDefault : TelegramMenuButton
{
    public override MenuButtonType Type => MenuButtonType.Default;
}