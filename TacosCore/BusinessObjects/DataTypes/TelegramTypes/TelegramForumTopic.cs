//-----------------------------------------------------------------------
// <copyright file="TelegramForumTopic.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramForumTopic : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _iconColor;
    private string _iconCustomEmojiId = string.Empty;

    private int _messageThreadId;
    private string _name = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int IconColor
    {
        get => _iconColor;
        set { OnPropertyChanging(nameof(IconColor)); _iconColor = value; OnPropertyChanged(nameof(IconColor)); }
    }


    public virtual string IconCustomEmojiId
    {
        get => _iconCustomEmojiId;
        set { OnPropertyChanging(nameof(IconCustomEmojiId)); _iconCustomEmojiId = value; OnPropertyChanged(nameof(IconCustomEmojiId)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int MessageThreadId
    {
        get => _messageThreadId;
        set { OnPropertyChanging(nameof(MessageThreadId)); _messageThreadId = value; OnPropertyChanged(nameof(MessageThreadId)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }
}
