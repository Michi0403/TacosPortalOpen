//-----------------------------------------------------------------------
// <copyright file="TelegramChatShared.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatShared : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private long _chatId;

    private int _requestId;
    private string _title = string.Empty;
    private string _username = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [LongIntIDSanity]
    public virtual long ChatId
    {
        get => _chatId;
        set { OnPropertyChanging(nameof(ChatId)); _chatId = value; OnPropertyChanged(nameof(ChatId)); }
    }

    [InverseProperty(nameof(TelegramMessage.ChatShared))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisChatSharedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramPhotoSize.SharedChats))]
    [JsonIgnore]
    public virtual IList<TelegramPhotoSize>? Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int RequestId
    {
        get => _requestId;
        set { OnPropertyChanging(nameof(RequestId)); _requestId = value; OnPropertyChanged(nameof(RequestId)); }
    }

    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }

    public virtual string Username
    {
        get => _username;
        set { OnPropertyChanging(nameof(Username)); _username = value; OnPropertyChanged(nameof(Username)); }
    }
}
