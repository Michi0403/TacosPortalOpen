//-----------------------------------------------------------------------
// <copyright file="TelegramInlineKeyboardMarkup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramInlineKeyboardMarkup : TelegramReplyMarkup, INotifyPropertyChanging, INotifyPropertyChanged
{

    private IList<TelegramInlineKeyboardRow>? inlineKeyboard = new ObservableCollection<TelegramInlineKeyboardRow>();
    private IList<TelegramMessage>? messageThisInlineKeyboardMarkupBelongsTo = new ObservableCollection<TelegramMessage>();

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [NotMapped]
    public virtual IList<TelegramInlineKeyboardRow>? InlineKeyboard
    {
        get => inlineKeyboard;
        set
        {
            OnPropertyChanging(nameof(InlineKeyboard));
            inlineKeyboard = value;
            OnPropertyChanged(nameof(InlineKeyboard));
        }
    }

    [InverseProperty(nameof(TelegramMessage.ReplyMarkup))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisInlineKeyboardMarkupBelongsTo
    {
        get => messageThisInlineKeyboardMarkupBelongsTo;
        set
        {
            OnPropertyChanging(nameof(MessageThisInlineKeyboardMarkupBelongsTo));
            messageThisInlineKeyboardMarkupBelongsTo = value;
            OnPropertyChanged(nameof(MessageThisInlineKeyboardMarkupBelongsTo));
        }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineKeyboardRow : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private IList<TelegramInlineKeyboardButton>? buttons = new ObservableCollection<TelegramInlineKeyboardButton>();
    private TelegramInlineKeyboardMarkup? replyKeyboardMarkup;

    private Guid? replyKeyboardMarkupID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual IList<TelegramInlineKeyboardButton>? Buttons
    {
        get => buttons;
        set
        {
            OnPropertyChanging(nameof(Buttons));
            buttons = value;
            OnPropertyChanged(nameof(Buttons));
        }
    }

    public virtual TelegramInlineKeyboardMarkup? ReplyKeyboardMarkup
    {
        get => replyKeyboardMarkup;
        set
        {
            OnPropertyChanging(nameof(ReplyKeyboardMarkup));
            replyKeyboardMarkup = value;
            OnPropertyChanged(nameof(ReplyKeyboardMarkup));
        }
    }

    [ForeignKey(nameof(ReplyKeyboardMarkup))]
    public virtual Guid? ReplyKeyboardMarkupID
    {
        get => replyKeyboardMarkupID;
        set
        {
            OnPropertyChanging(nameof(ReplyKeyboardMarkupID));
            replyKeyboardMarkupID = value;
            OnPropertyChanged(nameof(ReplyKeyboardMarkupID));
        }
    }
}
