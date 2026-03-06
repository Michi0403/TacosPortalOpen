//-----------------------------------------------------------------------
// <copyright file="TelegramKeyboardButton.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramKeyboardButton : BaseObject, TelegramIKeyboardButton, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramKeyboardButtonRequestChat _requestChat = null!;
    private Guid? _requestChatID;
    private bool _requestContact;
    private bool _requestLocation;
    private TelegramKeyboardButtonPollType _requestPoll = null!;
    private Guid? _requestPollID;
    private TelegramKeyboardButtonRequestUsers _requestUsers = null!;
    private Guid? _requestUsersID;

    private string _text = string.Empty;
    private TelegramWebAppInfo _webApp = null!;
    private Guid? _webAppID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramKeyboardButtonRequestChat RequestChat
    {
        get => _requestChat;
        set { OnPropertyChanging(nameof(RequestChat)); _requestChat = value; OnPropertyChanged(nameof(RequestChat)); }
    }

    [ForeignKey("RequestChat")]
    public virtual Guid? RequestChatID
    {
        get => _requestChatID;
        set { OnPropertyChanging(nameof(RequestChatID)); _requestChatID = value; OnPropertyChanged(nameof(RequestChatID)); }
    }

    public virtual bool RequestContact
    {
        get => _requestContact;
        set { OnPropertyChanging(nameof(RequestContact)); _requestContact = value; OnPropertyChanged(nameof(RequestContact)); }
    }

    public virtual bool RequestLocation
    {
        get => _requestLocation;
        set { OnPropertyChanging(nameof(RequestLocation)); _requestLocation = value; OnPropertyChanged(nameof(RequestLocation)); }
    }

    public virtual TelegramKeyboardButtonPollType RequestPoll
    {
        get => _requestPoll;
        set { OnPropertyChanging(nameof(RequestPoll)); _requestPoll = value; OnPropertyChanged(nameof(RequestPoll)); }
    }

    [ForeignKey("RequestPoll")]
    public virtual Guid? RequestPollID
    {
        get => _requestPollID;
        set { OnPropertyChanging(nameof(RequestPollID)); _requestPollID = value; OnPropertyChanged(nameof(RequestPollID)); }
    }

    public virtual TelegramKeyboardButtonRequestUsers RequestUsers
    {
        get => _requestUsers;
        set { OnPropertyChanging(nameof(RequestUsers)); _requestUsers = value; OnPropertyChanged(nameof(RequestUsers)); }
    }

    [ForeignKey("RequestUsers")]
    public virtual Guid? RequestUsersID
    {
        get => _requestUsersID;
        set { OnPropertyChanging(nameof(RequestUsersID)); _requestUsersID = value; OnPropertyChanged(nameof(RequestUsersID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }

    public virtual TelegramWebAppInfo WebApp
    {
        get => _webApp;
        set { OnPropertyChanging(nameof(WebApp)); _webApp = value; OnPropertyChanged(nameof(WebApp)); }
    }

    [ForeignKey("WebApp")]
    public virtual Guid? WebAppID
    {
        get => _webAppID;
        set { OnPropertyChanging(nameof(WebAppID)); _webAppID = value; OnPropertyChanged(nameof(WebAppID)); }
    }
}
