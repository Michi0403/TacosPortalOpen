//-----------------------------------------------------------------------
// <copyright file="TelegramChatPhoto.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatPhoto : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _bigFileId = string.Empty;
    private string _bigFileUniqueId = string.Empty;

    private string _smallFileId = string.Empty;
    private string _smallFileUniqueId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string BigFileId
    {
        get => _bigFileId;
        set { OnPropertyChanging(nameof(BigFileId)); _bigFileId = value; OnPropertyChanged(nameof(BigFileId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string BigFileUniqueId
    {
        get => _bigFileUniqueId;
        set { OnPropertyChanging(nameof(BigFileUniqueId)); _bigFileUniqueId = value; OnPropertyChanged(nameof(BigFileUniqueId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string SmallFileId
    {
        get => _smallFileId;
        set { OnPropertyChanging(nameof(SmallFileId)); _smallFileId = value; OnPropertyChanged(nameof(SmallFileId)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string SmallFileUniqueId
    {
        get => _smallFileUniqueId;
        set { OnPropertyChanging(nameof(SmallFileUniqueId)); _smallFileUniqueId = value; OnPropertyChanged(nameof(SmallFileUniqueId)); }
    }
}
