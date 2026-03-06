//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessMessagesDeleted.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;


[Authorize]
[DefaultClassOptions]
public partial class TelegramBusinessMessagesDeleted : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _businessConnectionId = string.Empty;
    private TelegramChat _chat = null!;
    private Guid? _chatID;
    private IList<int>? _messageIds = new ObservableCollection<int>();

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string BusinessConnectionId
    {
        get => _businessConnectionId;
        set
        {
            OnPropertyChanging(nameof(BusinessConnectionId));
            _businessConnectionId = value;
            OnPropertyChanged(nameof(BusinessConnectionId));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.BusinessMessagesDeletedThisChatBelongsTo))]
    public virtual TelegramChat Chat
    {
        get => _chat;
        set
        {
            OnPropertyChanging(nameof(Chat));
            _chat = value;
            OnPropertyChanged(nameof(Chat));
        }
    }





    [ForeignKey("Chat")]
    public virtual Guid? ChatID
    {
        get => _chatID;
        set
        {
            OnPropertyChanging(nameof(ChatID));
            _chatID = value;
            OnPropertyChanged(nameof(ChatID));
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<int>? MessageIds
    {
        get => _messageIds;
        set
        {
            OnPropertyChanging(nameof(MessageIds));
            _messageIds = value;
            OnPropertyChanged(nameof(MessageIds));
        }
    }
}
