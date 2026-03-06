//-----------------------------------------------------------------------
// <copyright file="TelegramKeyboardButtonRequestChat.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramKeyboardButtonRequestChat : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChatAdministratorRights? _botAdministratorRights;
    private Guid? _botAdministratorRightsID;
    private bool _botIsMember;
    private bool? _chatHasUsername;
    private bool _chatIsChannel;
    private bool _chatIsCreated;
    private bool? _chatIsForum;

    private int _requestId;
    private bool _requestPhoto;
    private bool _requestTitle;
    private bool _requestUsername;
    private TelegramChatAdministratorRights? _userAdministratorRights;
    private Guid? _userAdministratorRightsID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramChatAdministratorRights.KeyboardButtonsToBotAdministratorRights))]
    public virtual TelegramChatAdministratorRights? BotAdministratorRights
    {
        get => _botAdministratorRights;
        set { OnPropertyChanging(nameof(BotAdministratorRights)); _botAdministratorRights = value; OnPropertyChanged(nameof(BotAdministratorRights)); }
    }

    [ForeignKey("BotAdministratorRights")]
    public virtual Guid? BotAdministratorRightsID
    {
        get => _botAdministratorRightsID;
        set { OnPropertyChanging(nameof(BotAdministratorRightsID)); _botAdministratorRightsID = value; OnPropertyChanged(nameof(BotAdministratorRightsID)); }
    }

    public virtual bool BotIsMember
    {
        get => _botIsMember;
        set { OnPropertyChanging(nameof(BotIsMember)); _botIsMember = value; OnPropertyChanged(nameof(BotIsMember)); }
    }

    public virtual bool? ChatHasUsername
    {
        get => _chatHasUsername;
        set { OnPropertyChanging(nameof(ChatHasUsername)); _chatHasUsername = value; OnPropertyChanged(nameof(ChatHasUsername)); }
    }

    public virtual bool ChatIsChannel
    {
        get => _chatIsChannel;
        set { OnPropertyChanging(nameof(ChatIsChannel)); _chatIsChannel = value; OnPropertyChanged(nameof(ChatIsChannel)); }
    }

    public virtual bool ChatIsCreated
    {
        get => _chatIsCreated;
        set { OnPropertyChanging(nameof(ChatIsCreated)); _chatIsCreated = value; OnPropertyChanged(nameof(ChatIsCreated)); }
    }

    public virtual bool? ChatIsForum
    {
        get => _chatIsForum;
        set { OnPropertyChanging(nameof(ChatIsForum)); _chatIsForum = value; OnPropertyChanged(nameof(ChatIsForum)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int RequestId
    {
        get => _requestId;
        set { OnPropertyChanging(nameof(RequestId)); _requestId = value; OnPropertyChanged(nameof(RequestId)); }
    }

    public virtual bool RequestPhoto
    {
        get => _requestPhoto;
        set { OnPropertyChanging(nameof(RequestPhoto)); _requestPhoto = value; OnPropertyChanged(nameof(RequestPhoto)); }
    }

    public virtual bool RequestTitle
    {
        get => _requestTitle;
        set { OnPropertyChanging(nameof(RequestTitle)); _requestTitle = value; OnPropertyChanged(nameof(RequestTitle)); }
    }

    public virtual bool RequestUsername
    {
        get => _requestUsername;
        set { OnPropertyChanging(nameof(RequestUsername)); _requestUsername = value; OnPropertyChanged(nameof(RequestUsername)); }
    }

    [InverseProperty(nameof(TelegramChatAdministratorRights.KeyboardButtonsToUserAdministratorRights))]
    public virtual TelegramChatAdministratorRights? UserAdministratorRights
    {
        get => _userAdministratorRights;
        set { OnPropertyChanging(nameof(UserAdministratorRights)); _userAdministratorRights = value; OnPropertyChanged(nameof(UserAdministratorRights)); }
    }

    [ForeignKey("UserAdministratorRights")]
    public virtual Guid? UserAdministratorRightsID
    {
        get => _userAdministratorRightsID;
        set { OnPropertyChanging(nameof(UserAdministratorRightsID)); _userAdministratorRightsID = value; OnPropertyChanged(nameof(UserAdministratorRightsID)); }
    }
}
