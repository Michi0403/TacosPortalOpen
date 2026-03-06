//-----------------------------------------------------------------------
// <copyright file="TelegramForumTopicCreated.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramForumTopicCreated : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _iconColor;
    private string _iconCustomEmojiId = string.Empty;

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
    [InverseProperty(nameof(TelegramMessage.ForumTopicCreated))]
    [JsonIgnore]

    public virtual IList<TelegramMessage>? MessageThisForumTopicCreatedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }
}
