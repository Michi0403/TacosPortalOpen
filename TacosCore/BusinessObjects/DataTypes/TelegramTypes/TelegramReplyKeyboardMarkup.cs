//-----------------------------------------------------------------------
// <copyright file="TelegramReplyKeyboardMarkup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramReplyKeyboardMarkup : TelegramReplyMarkup, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _inputFieldPlaceholder = string.Empty;

    private bool _isPersistent;
    private bool _oneTimeKeyboard;
    private bool _resizeKeyboard;
    private bool _selective;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string InputFieldPlaceholder
    {
        get => _inputFieldPlaceholder;
        set { OnPropertyChanging(nameof(InputFieldPlaceholder)); _inputFieldPlaceholder = value; OnPropertyChanged(nameof(InputFieldPlaceholder)); }
    }

    public virtual bool IsPersistent
    {
        get => _isPersistent;
        set { OnPropertyChanging(nameof(IsPersistent)); _isPersistent = value; OnPropertyChanged(nameof(IsPersistent)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramKeyboardRow>? Keyboard { get; set; } = new ObservableCollection<TelegramKeyboardRow>();

    public virtual bool OneTimeKeyboard
    {
        get => _oneTimeKeyboard;
        set { OnPropertyChanging(nameof(OneTimeKeyboard)); _oneTimeKeyboard = value; OnPropertyChanged(nameof(OneTimeKeyboard)); }
    }

    public virtual bool ResizeKeyboard
    {
        get => _resizeKeyboard;
        set { OnPropertyChanging(nameof(ResizeKeyboard)); _resizeKeyboard = value; OnPropertyChanged(nameof(ResizeKeyboard)); }
    }

    public virtual bool Selective
    {
        get => _selective;
        set { OnPropertyChanging(nameof(Selective)); _selective = value; OnPropertyChanged(nameof(Selective)); }
    }
}

[Authorize]
[DefaultClassOptions]
public class TelegramKeyboardRow : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramReplyKeyboardMarkup? _replyKeyboardMarkup;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual IList<TelegramKeyboardButton>? Buttons { get; set; } = new ObservableCollection<TelegramKeyboardButton>();

    public virtual TelegramReplyKeyboardMarkup ReplyKeyboardMarkup
    {
        get => _replyKeyboardMarkup;
        set { OnPropertyChanging(nameof(ReplyKeyboardMarkup)); _replyKeyboardMarkup = value; OnPropertyChanged(nameof(ReplyKeyboardMarkup)); }
    }

    [ForeignKey("ReplyKeyboardMarkup")]
    public virtual Guid? ReplyKeyboardMarkupID { get; set; }
}