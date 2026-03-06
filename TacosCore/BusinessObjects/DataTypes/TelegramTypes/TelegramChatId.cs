//-----------------------------------------------------------------------
// <copyright file="TelegramChatId.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public class TelegramChatId : BaseObject, IEquatable<TelegramChatId>, INotifyPropertyChanging, INotifyPropertyChanged
{

    private long? _identifier;
    private string _username = string.Empty;

    public TelegramChatId() { }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public bool Equals(TelegramChatId? other) =>
        other is TelegramChatId chatId &&
        Identifier == chatId.Identifier &&
        Username == chatId.Username;


    public virtual long? Identifier
    {
        get => _identifier;
        set { OnPropertyChanging(nameof(Identifier)); _identifier = value; OnPropertyChanged(nameof(Identifier)); }
    }


    public virtual string Username
    {
        get => _username;
        set { OnPropertyChanging(nameof(Username)); _username = value; OnPropertyChanged(nameof(Username)); }
    }
}
