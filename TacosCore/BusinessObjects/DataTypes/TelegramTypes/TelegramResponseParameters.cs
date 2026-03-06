//-----------------------------------------------------------------------
// <copyright file="TelegramResponseParameters.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramResponseParameters : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private long? _migrateToChatId;
    private int? _retryAfter;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual long? MigrateToChatId
    {
        get => _migrateToChatId;
        set { OnPropertyChanging(nameof(MigrateToChatId)); _migrateToChatId = value; OnPropertyChanged(nameof(MigrateToChatId)); }
    }


    public virtual int? RetryAfter
    {
        get => _retryAfter;
        set { OnPropertyChanging(nameof(RetryAfter)); _retryAfter = value; OnPropertyChanged(nameof(RetryAfter)); }
    }
}