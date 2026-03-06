//-----------------------------------------------------------------------
// <copyright file="TelegramSharedUser.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramSharedUser : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    private long _userId;
    private string _username = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string FirstName
    {
        get => _firstName;
        set { OnPropertyChanging(nameof(FirstName)); _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }

    public virtual string LastName
    {
        get => _lastName;
        set { OnPropertyChanging(nameof(LastName)); _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    public virtual IList<TelegramPhotoSize>? Photo { get; set; } = new ObservableCollection<TelegramPhotoSize>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [LongIntIDSanity]
    public virtual long UserId
    {
        get => _userId;
        set { OnPropertyChanging(nameof(UserId)); _userId = value; OnPropertyChanged(nameof(UserId)); }
    }

    public virtual string Username
    {
        get => _username;
        set { OnPropertyChanging(nameof(Username)); _username = value; OnPropertyChanged(nameof(Username)); }
    }

    [InverseProperty(nameof(TelegramUsersShared.Users))]
    public virtual IList<TelegramUsersShared>? UsersSharedThisUsersSharedsBelongsTo { get; set; } = new ObservableCollection<TelegramUsersShared>();
}