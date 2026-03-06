//-----------------------------------------------------------------------
// <copyright file="TelegramUserProfilePhotos.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramUserProfilePhotos : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _totalCount;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramPhotoSizeGroup>? Photos { get; set; } = new ObservableCollection<TelegramPhotoSizeGroup>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TotalCount
    {
        get => _totalCount;
        set
        {
            if (_totalCount != value)
            {
                OnPropertyChanging(nameof(TotalCount));
                _totalCount = value;
                OnPropertyChanged(nameof(TotalCount));
            }
        }
    }
}