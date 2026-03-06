//-----------------------------------------------------------------------
// <copyright file="TelegramForceReplyMarkup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramForceReplyMarkup : TelegramReplyMarkup, INotifyPropertyChanging, INotifyPropertyChanged
{

    private bool _forceReply;
    private string _inputFieldPlaceholder = string.Empty;
    private bool _selective;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual bool ForceReply
    {
        get => _forceReply;
        set { OnPropertyChanging(nameof(ForceReply)); _forceReply = value; OnPropertyChanged(nameof(ForceReply)); }
    }


    public virtual string InputFieldPlaceholder
    {
        get => _inputFieldPlaceholder;
        set { OnPropertyChanging(nameof(InputFieldPlaceholder)); _inputFieldPlaceholder = value; OnPropertyChanged(nameof(InputFieldPlaceholder)); }
    }


    public virtual bool Selective
    {
        get => _selective;
        set { OnPropertyChanging(nameof(Selective)); _selective = value; OnPropertyChanged(nameof(Selective)); }
    }
}
