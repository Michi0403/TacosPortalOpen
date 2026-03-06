//-----------------------------------------------------------------------
// <copyright file="TelegramCallbackQuery.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramCallbackQuery : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _callbackQueryId = string.Empty;
    private string _chatInstance = string.Empty;
    private string? _data;
    private TelegramUser? _from;
    private Guid? _fromID;
    private string? _gameShortName;
    private string? _inlineMessageId;
    private TelegramMessage? _message;
    private Guid? _telegramCallbackQueryMessageID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string CallbackQueryId
    {
        get => _callbackQueryId;
        set { OnPropertyChanging(nameof(CallbackQueryId)); _callbackQueryId = value; OnPropertyChanged(nameof(CallbackQueryId)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ChatInstance
    {
        get => _chatInstance;
        set { OnPropertyChanging(nameof(ChatInstance)); _chatInstance = value; OnPropertyChanged(nameof(ChatInstance)); }
    }


    public virtual string? Data
    {
        get => _data;
        set { OnPropertyChanging(nameof(Data)); _data = value; OnPropertyChanged(nameof(Data)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.CallbackQueryThisUserBelongsTo))]
    public virtual TelegramUser? From
    {
        get => _from;
        set { OnPropertyChanging(nameof(From)); _from = value; OnPropertyChanged(nameof(From)); }
    }

    [ForeignKey("From")]
    public virtual Guid? FromID
    {
        get => _fromID;
        set { OnPropertyChanging(nameof(FromID)); _fromID = value; OnPropertyChanged(nameof(FromID)); }
    }


    public virtual string? GameShortName
    {
        get => _gameShortName;
        set { OnPropertyChanging(nameof(GameShortName)); _gameShortName = value; OnPropertyChanged(nameof(GameShortName)); }
    }


    public virtual string? InlineMessageId
    {
        get => _inlineMessageId;
        set { OnPropertyChanging(nameof(InlineMessageId)); _inlineMessageId = value; OnPropertyChanged(nameof(InlineMessageId)); }
    }


    [InverseProperty(nameof(TelegramMessage.CallbackQueryThisMessageBelongsTo))]
    public virtual TelegramMessage? Message
    {
        get => _message;
        set { OnPropertyChanging(nameof(Message)); _message = value; OnPropertyChanged(nameof(Message)); }
    }

    [ForeignKey("Message")]
    public virtual Guid? TelegramCallbackQueryMessageID
    {
        get => _telegramCallbackQueryMessageID;
        set { OnPropertyChanging(nameof(TelegramCallbackQueryMessageID)); _telegramCallbackQueryMessageID = value; OnPropertyChanged(nameof(TelegramCallbackQueryMessageID)); }
    }
}
