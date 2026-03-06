//-----------------------------------------------------------------------
// <copyright file="TelegramMessageOrigin.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramMessageOrigin : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private DateTime _date;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime Date
    {
        get => _date;
        set { OnPropertyChanging(nameof(Date)); _date = value; OnPropertyChanged(nameof(Date)); }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Origin))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisMessageOriginBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.ForwardOrigin))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageOriginBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();
    [JsonPropertyName("Type")]
    public abstract MessageOriginType Type { get; }
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageOriginUser : TelegramMessageOrigin
{
    private TelegramUser _senderUser;

    private Guid? _senderUserID;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramUser SenderUser
    {
        get => _senderUser;
        set { OnPropertyChanging(nameof(SenderUser)); _senderUser = value; OnPropertyChanged(nameof(SenderUser)); }
    }

    [ForeignKey("SenderUser")]
    public virtual Guid? SenderUserID
    {
        get => _senderUserID;
        set { OnPropertyChanging(nameof(SenderUserID)); _senderUserID = value; OnPropertyChanged(nameof(SenderUserID)); }
    }
    public override MessageOriginType Type => MessageOriginType.User;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageOriginHiddenUser : TelegramMessageOrigin
{

    private string _senderUserName = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string SenderUserName
    {
        get => _senderUserName;
        set { OnPropertyChanging(nameof(SenderUserName)); _senderUserName = value; OnPropertyChanged(nameof(SenderUserName)); }
    }
    public override MessageOriginType Type => MessageOriginType.HiddenUser;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageOriginChat : TelegramMessageOrigin
{
    private string? _authorSignature;
    private TelegramChat _senderChat = null!;

    private Guid? _senderChatID;

    public virtual string? AuthorSignature
    {
        get => _authorSignature;
        set { OnPropertyChanging(nameof(AuthorSignature)); _authorSignature = value; OnPropertyChanged(nameof(AuthorSignature)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramChat SenderChat
    {
        get => _senderChat;
        set { OnPropertyChanging(nameof(SenderChat)); _senderChat = value; OnPropertyChanged(nameof(SenderChat)); }
    }

    [ForeignKey("SenderChat")]
    public virtual Guid? SenderChatID
    {
        get => _senderChatID;
        set { OnPropertyChanging(nameof(SenderChatID)); _senderChatID = value; OnPropertyChanged(nameof(SenderChatID)); }
    }
    public override MessageOriginType Type => MessageOriginType.Chat;
}
[Authorize]
[DefaultClassOptions]
public partial class TelegramMessageOriginChannel : TelegramMessageOrigin
{
    private string? _authorSignature;
    private TelegramChat _chat;

    private Guid? _chatID;
    private int _messageId;

    public virtual string? AuthorSignature
    {
        get => _authorSignature;
        set { OnPropertyChanging(nameof(AuthorSignature)); _authorSignature = value; OnPropertyChanged(nameof(AuthorSignature)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => _chatID;
        set { OnPropertyChanging(nameof(ChatID)); _chatID = value; OnPropertyChanged(nameof(ChatID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int messageId
    {
        get => _messageId;
        set { OnPropertyChanging(nameof(messageId)); _messageId = value; OnPropertyChanged(nameof(messageId)); }
    }
    public override MessageOriginType Type => MessageOriginType.Channel;
}