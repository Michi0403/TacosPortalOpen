//-----------------------------------------------------------------------
// <copyright file="TelegramUserChats.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramUserChat : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private DateOnly dateCreated = DateOnly.FromDateTime(DateTime.Now);
    private TelegramUser? user = null!;

    private Guid? userID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramChat.TelegramUserChatsThisChatBelongsTo))]
    public virtual IList<TelegramChat>? ChatThisUserChatBelongsTo { get; set; } = new ObservableCollection<TelegramChat>();
    public virtual DateOnly DateCreated
    {
        get => dateCreated;

    }
    public virtual DateOnly DateUpdated
    {
        get => dateCreated;
        set { OnPropertyChanging(nameof(DateCreated)); dateCreated = value; OnPropertyChanged(nameof(DateCreated)); }
    }

    [InverseProperty(nameof(TelegramUser.UserChatsThisUserBelongsTo))]
    [DeleteBehavior(DeleteBehavior.Restrict)]

    public virtual TelegramUser? User
    {
        get => user;
        set { OnPropertyChanging(nameof(User)); user = value; OnPropertyChanged(nameof(User)); }
    }
    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => userID;
        set { OnPropertyChanging(nameof(UserID)); userID = value; OnPropertyChanged(nameof(UserID)); }
    }
}