//-----------------------------------------------------------------------
// <copyright file="TelegramInputSticker.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
[Authorize]
[DefaultClassOptions]
public partial class TelegramInputSticker : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private StickerFormat _format;
    private TelegramMaskPosition _maskPosition = null!;
    private Guid? _maskPositionId;

    private TelegramInputFile _sticker = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<string>? EmojiList { get; set; } = new ObservableCollection<string>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual StickerFormat Format
    {
        get => _format;
        set { OnPropertyChanging(nameof(Format)); _format = value; OnPropertyChanged(nameof(Format)); }
    }

    public virtual IList<string>? Keywords { get; set; } = new ObservableCollection<string>();

    public virtual TelegramMaskPosition MaskPosition
    {
        get => _maskPosition;
        set { OnPropertyChanging(nameof(MaskPosition)); _maskPosition = value; OnPropertyChanged(nameof(MaskPosition)); }
    }

    [ForeignKey("MaskPosition")]
    public virtual Guid? MaskPositionID
    {
        get => _maskPositionId;
        set { OnPropertyChanging(nameof(MaskPositionID)); _maskPositionId = value; OnPropertyChanged(nameof(MaskPositionID)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required virtual TelegramInputFile Sticker
    {
        get => _sticker;
        set { OnPropertyChanging(nameof(Sticker)); _sticker = value; OnPropertyChanged(nameof(Sticker)); }
    }
}
