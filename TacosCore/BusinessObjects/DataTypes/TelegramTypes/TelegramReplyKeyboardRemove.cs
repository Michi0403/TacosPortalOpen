//-----------------------------------------------------------------------
// <copyright file="TelegramReplyKeyboardRemove.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramReplyKeyboardRemove : TelegramReplyMarkup, INotifyPropertyChanging, INotifyPropertyChanged
{

    private bool _removeKeyboard;
    private bool _selective;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    public virtual bool RemoveKeyboard
    {
        get => _removeKeyboard;
        set { OnPropertyChanging(nameof(RemoveKeyboard)); _removeKeyboard = value; OnPropertyChanged(nameof(RemoveKeyboard)); }
    }




    public virtual bool Selective
    {
        get => _selective;
        set { OnPropertyChanging(nameof(Selective)); _selective = value; OnPropertyChanged(nameof(Selective)); }
    }
}