//-----------------------------------------------------------------------
// <copyright file="TelegramForumTopicEdited.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramForumTopicEdited : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _iconCustomEmojiId = string.Empty;

    private string _name = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    public virtual string IconCustomEmojiId
    {
        get => _iconCustomEmojiId;
        set { OnPropertyChanging(nameof(IconCustomEmojiId)); _iconCustomEmojiId = value; OnPropertyChanged(nameof(IconCustomEmojiId)); }
    }
    [InverseProperty(nameof(TelegramMessage.ForumTopicEdited))]

    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisForumTopicEditedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }
}
