//-----------------------------------------------------------------------
// <copyright file="TelegramUsersShared.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramUsersShared : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _requestId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramMessage.UsersShared))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisUsersSharedBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int RequestId
    {
        get => _requestId;
        set
        {
            if (_requestId != value)
            {
                OnPropertyChanging(nameof(RequestId));
                _requestId = value;
                OnPropertyChanged(nameof(RequestId));
            }
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramSharedUser>? Users { get; set; } = new ObservableCollection<TelegramSharedUser>();
}