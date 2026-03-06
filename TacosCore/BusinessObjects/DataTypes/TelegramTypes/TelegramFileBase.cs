//-----------------------------------------------------------------------
// <copyright file="TelegramFileBase.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public abstract partial class TelegramFileBase : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _fileId = string.Empty;
    private long? _fileSize;
    private string _fileUniqueId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileId
    {
        get => _fileId;
        set { OnPropertyChanging(nameof(FileId)); _fileId = value; OnPropertyChanged(nameof(FileId)); }
    }


    public virtual long? FileSize
    {
        get => _fileSize;
        set { OnPropertyChanging(nameof(FileSize)); _fileSize = value; OnPropertyChanged(nameof(FileSize)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileUniqueId
    {
        get => _fileUniqueId;
        set { OnPropertyChanging(nameof(FileUniqueId)); _fileUniqueId = value; OnPropertyChanged(nameof(FileUniqueId)); }
    }
}
