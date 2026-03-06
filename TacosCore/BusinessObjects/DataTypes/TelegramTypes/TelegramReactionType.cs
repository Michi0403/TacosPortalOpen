//-----------------------------------------------------------------------
// <copyright file="TelegramReactionType.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
[DefaultClassOptions]
public class TelegramMessageReactionOldJoin : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramMessageReactionUpdated.OldReactionLinks))]
    public virtual TelegramMessageReactionUpdated? TelegramMessageReactionUpdated { get; set; }
    [ForeignKey(nameof(TelegramMessageReactionUpdated))]
    public virtual Guid TelegramMessageReactionUpdatedId { get; set; }

    [InverseProperty(nameof(TelegramReactionType.OldReactionLinks))]
    public virtual TelegramReactionType? TelegramReactionType { get; set; }

    [ForeignKey(nameof(TelegramReactionType))]
    public virtual Guid TelegramReactionTypeId { get; set; }
}




[DefaultClassOptions]
public class TelegramMessageReactionNewJoin : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramMessageReactionUpdated.NewReactionLinks))]
    public virtual TelegramMessageReactionUpdated? TelegramMessageReactionUpdated { get; set; }
    [ForeignKey(nameof(TelegramMessageReactionUpdated))]
    public virtual Guid TelegramMessageReactionUpdatedId { get; set; }

    [InverseProperty(nameof(TelegramReactionType.NewReactionLinks))]
    public virtual TelegramReactionType? TelegramReactionType { get; set; }

    [ForeignKey(nameof(TelegramReactionType))]
    public virtual Guid TelegramReactionTypeId { get; set; }
}




public abstract partial class TelegramReactionType : BaseObject
{

    [InverseProperty(nameof(TelegramMessageReactionNewJoin.TelegramReactionType))]
    public virtual IList<TelegramMessageReactionNewJoin>? NewReactionLinks { get; set; } = new ObservableCollection<TelegramMessageReactionNewJoin>();
    [InverseProperty(nameof(TelegramMessageReactionOldJoin.TelegramReactionType))]
    public virtual IList<TelegramMessageReactionOldJoin>? OldReactionLinks { get; set; } = new ObservableCollection<TelegramMessageReactionOldJoin>();

    [InverseProperty(nameof(TelegramReactionCount.Type))]
    public virtual IList<TelegramReactionCount>? ReactionCountThisReactionTypeBelongsTo { get; set; } = new ObservableCollection<TelegramReactionCount>();
    public abstract ReactionTypeKind Type { get; }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramReactionTypeEmoji : TelegramReactionType, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _emoji = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Emoji
    {
        get => _emoji;
        set
        {
            OnPropertyChanging(nameof(Emoji));
            _emoji = value;
            OnPropertyChanged(nameof(Emoji));
        }
    }

    public override ReactionTypeKind Type => ReactionTypeKind.Emoji;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramReactionTypeCustomEmoji : TelegramReactionType, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _customEmojiId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string CustomEmojiId
    {
        get => _customEmojiId;
        set
        {
            OnPropertyChanging(nameof(CustomEmojiId));
            _customEmojiId = value;
            OnPropertyChanged(nameof(CustomEmojiId));
        }
    }

    public override ReactionTypeKind Type => ReactionTypeKind.CustomEmoji;
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramReactionTypePaid : TelegramReactionType
{
    public override ReactionTypeKind Type => ReactionTypeKind.Paid;


}