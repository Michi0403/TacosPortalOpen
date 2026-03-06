//-----------------------------------------------------------------------
// <copyright file="TelegramChatBoostUpdated.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramChatBoostUpdated : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramChatBoost _boost = null!;
    private Guid? _boostID;
    private TelegramChat _chat = null!;

    private Guid? _chatID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChatBoost.ChatBoostUpdatedThisChatBoostBelongsTo))]
    public virtual TelegramChatBoost Boost
    {
        get => _boost;
        set { OnPropertyChanging(nameof(Boost)); _boost = value; OnPropertyChanged(nameof(Boost)); }
    }

    [ForeignKey("Boost")]
    public virtual Guid? BoostID
    {
        get => _boostID;
        set { OnPropertyChanging(nameof(BoostID)); _boostID = value; OnPropertyChanged(nameof(BoostID)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramChat.ChatBoostUpdatedThisChatBelongsTo))]
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
}
