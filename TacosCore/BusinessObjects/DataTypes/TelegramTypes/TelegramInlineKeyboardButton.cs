//-----------------------------------------------------------------------
// <copyright file="TelegramInlineKeyboardButton.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;





[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineKeyboardButton : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged, TelegramIKeyboardButton
{
    private string _callbackData = string.Empty;
    private TelegramCallbackGame _callbackGame = null!;
    private Guid? _callbackGameId;
    private TelegramCopyTextButton _copyText = null!;
    private Guid? _copyTextId;
    private TelegramLoginUrl _loginUrl = null!;
    private Guid? _loginUrlId;
    private bool _pay;
    private string _switchInlineQuery = string.Empty;
    private TelegramSwitchInlineQueryChosenChat _switchInlineQueryChosenChat = null!;
    private Guid? _switchInlineQueryChosenChatId;
    private string _switchInlineQueryCurrentChat = string.Empty;

    private string _text = string.Empty;
    private string _url = string.Empty;
    private TelegramWebAppInfo _webApp = null!;
    private Guid? _webAppId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string CallbackData
    {
        get => _callbackData;
        set { OnPropertyChanging(nameof(CallbackData)); _callbackData = value; OnPropertyChanged(nameof(CallbackData)); }
    }

    public virtual TelegramCallbackGame CallbackGame
    {
        get => _callbackGame;
        set { OnPropertyChanging(nameof(CallbackGame)); _callbackGame = value; OnPropertyChanged(nameof(CallbackGame)); }
    }

    [ForeignKey(nameof(CallbackGame))]
    public virtual Guid? CallbackGameID
    {
        get => _callbackGameId;
        set { OnPropertyChanging(nameof(CallbackGameID)); _callbackGameId = value; OnPropertyChanged(nameof(CallbackGameID)); }
    }

    public virtual TelegramCopyTextButton CopyText
    {
        get => _copyText;
        set { OnPropertyChanging(nameof(CopyText)); _copyText = value; OnPropertyChanged(nameof(CopyText)); }
    }

    [ForeignKey(nameof(CopyText))]
    public virtual Guid? CopyTextID
    {
        get => _copyTextId;
        set { OnPropertyChanging(nameof(CopyTextID)); _copyTextId = value; OnPropertyChanged(nameof(CopyTextID)); }
    }

    public virtual TelegramLoginUrl LoginUrl
    {
        get => _loginUrl;
        set { OnPropertyChanging(nameof(LoginUrl)); _loginUrl = value; OnPropertyChanged(nameof(LoginUrl)); }
    }

    [ForeignKey(nameof(LoginUrl))]
    public virtual Guid? LoginUrlID
    {
        get => _loginUrlId;
        set { OnPropertyChanging(nameof(LoginUrlID)); _loginUrlId = value; OnPropertyChanged(nameof(LoginUrlID)); }
    }

    public virtual bool Pay
    {
        get => _pay;
        set { OnPropertyChanging(nameof(Pay)); _pay = value; OnPropertyChanged(nameof(Pay)); }
    }

    public virtual string SwitchInlineQuery
    {
        get => _switchInlineQuery;
        set { OnPropertyChanging(nameof(SwitchInlineQuery)); _switchInlineQuery = value; OnPropertyChanged(nameof(SwitchInlineQuery)); }
    }

    public virtual TelegramSwitchInlineQueryChosenChat SwitchInlineQueryChosenChat
    {
        get => _switchInlineQueryChosenChat;
        set { OnPropertyChanging(nameof(SwitchInlineQueryChosenChat)); _switchInlineQueryChosenChat = value; OnPropertyChanged(nameof(SwitchInlineQueryChosenChat)); }
    }

    [ForeignKey(nameof(SwitchInlineQueryChosenChat))]
    public virtual Guid? SwitchInlineQueryChosenChatID
    {
        get => _switchInlineQueryChosenChatId;
        set { OnPropertyChanging(nameof(SwitchInlineQueryChosenChatID)); _switchInlineQueryChosenChatId = value; OnPropertyChanged(nameof(SwitchInlineQueryChosenChatID)); }
    }

    public virtual string SwitchInlineQueryCurrentChat
    {
        get => _switchInlineQueryCurrentChat;
        set { OnPropertyChanging(nameof(SwitchInlineQueryCurrentChat)); _switchInlineQueryCurrentChat = value; OnPropertyChanged(nameof(SwitchInlineQueryCurrentChat)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public virtual string Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }

    public virtual TelegramWebAppInfo WebApp
    {
        get => _webApp;
        set { OnPropertyChanging(nameof(WebApp)); _webApp = value; OnPropertyChanged(nameof(WebApp)); }
    }

    [ForeignKey(nameof(WebApp))]
    public virtual Guid? WebAppID
    {
        get => _webAppId;
        set { OnPropertyChanging(nameof(WebAppID)); _webAppId = value; OnPropertyChanged(nameof(WebAppID)); }
    }
}
