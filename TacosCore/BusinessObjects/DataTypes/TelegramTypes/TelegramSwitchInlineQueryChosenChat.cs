//-----------------------------------------------------------------------
// <copyright file="TelegramSwitchInlineQueryChosenChat.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramSwitchInlineQueryChosenChat : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _allowBotChats;
    private bool _allowChannelChats;
    private bool _allowGroupChats;
    private bool _allowUserChats;

    private string _query = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual bool AllowBotChats
    {
        get => _allowBotChats;
        set { OnPropertyChanging(nameof(AllowBotChats)); _allowBotChats = value; OnPropertyChanged(nameof(AllowBotChats)); }
    }


    public virtual bool AllowChannelChats
    {
        get => _allowChannelChats;
        set { OnPropertyChanging(nameof(AllowChannelChats)); _allowChannelChats = value; OnPropertyChanged(nameof(AllowChannelChats)); }
    }


    public virtual bool AllowGroupChats
    {
        get => _allowGroupChats;
        set { OnPropertyChanging(nameof(AllowGroupChats)); _allowGroupChats = value; OnPropertyChanged(nameof(AllowGroupChats)); }
    }


    public virtual bool AllowUserChats
    {
        get => _allowUserChats;
        set { OnPropertyChanging(nameof(AllowUserChats)); _allowUserChats = value; OnPropertyChanged(nameof(AllowUserChats)); }
    }





    public virtual string Query
    {
        get => _query;
        set { OnPropertyChanging(nameof(Query)); _query = value; OnPropertyChanged(nameof(Query)); }
    }
}