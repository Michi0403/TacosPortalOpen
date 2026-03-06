//-----------------------------------------------------------------------
// <copyright file="TelegramStory.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramStory : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramChat? _chat;
    private int _storyId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(Chat.StoryThisChatBelongsTo))]
    public virtual TelegramChat? Chat
    {
        get => _chat;
        set { OnPropertyChanging(nameof(Chat)); _chat = value; OnPropertyChanged(nameof(Chat)); }
    }

    [ForeignKey("Chat")]
    public virtual Guid? ChatID { get; set; }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Story))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisStoryBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.ReplyToStory))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessagesStoryRepliedTo { get; set; } = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramMessage.Story))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisStoryBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual int StoryId
    {
        get => _storyId;
        set { OnPropertyChanging(nameof(StoryId)); _storyId = value; OnPropertyChanged(nameof(StoryId)); }
    }
}