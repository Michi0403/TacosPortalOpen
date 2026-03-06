//-----------------------------------------------------------------------
// <copyright file="TelegramGame.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramGame : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramAnimation? _animation;
    private Guid? _animationId;
    private string _description = string.Empty;
    private string? _text;

    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [InverseProperty(nameof(Animation.GameThisAnimationBelongsTo))]
    public virtual TelegramAnimation? Animation
    {
        get => _animation;
        set { OnPropertyChanging(nameof(Animation)); _animation = value; OnPropertyChanged(nameof(Animation)); }
    }

    [ForeignKey(nameof(Animation))]
    public virtual Guid? AnimationID
    {
        get => _animationId;
        set { OnPropertyChanging(nameof(AnimationID)); _animationId = value; OnPropertyChanged(nameof(AnimationID)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Description
    {
        get => _description;
        set { OnPropertyChanging(nameof(Description)); _description = value; OnPropertyChanged(nameof(Description)); }
    }

    [InverseProperty(nameof(TelegramExternalReplyInfo.Game))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisGameBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    [InverseProperty(nameof(TelegramMessage.Game))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisGameBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramPhotoSize.Games))]
    public virtual IList<TelegramPhotoSize>? Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();




    public virtual string? Text
    {
        get => _text;
        set { OnPropertyChanging(nameof(Text)); _text = value; OnPropertyChanged(nameof(Text)); }
    }


    [InverseProperty(nameof(TelegramMessageEntity.GameThisMessageEntityBelongsTo))]
    public virtual IList<TelegramMessageEntity>? TextEntities { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Title
    {
        get => _title;
        set { OnPropertyChanging(nameof(Title)); _title = value; OnPropertyChanged(nameof(Title)); }
    }
}
