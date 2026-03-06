//-----------------------------------------------------------------------
// <copyright file="TelegramGameHighScore.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramGameHighScore : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _position;
    private int _score;
    private TelegramUser _user = null!;
    private Guid? _userID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Position
    {
        get => _position;
        set { OnPropertyChanging(nameof(Position)); _position = value; OnPropertyChanged(nameof(Position)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int Score
    {
        get => _score;
        set { OnPropertyChanging(nameof(Score)); _score = value; OnPropertyChanged(nameof(Score)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }


    [ForeignKey("User")]
    public virtual Guid? UserID
    {
        get => _userID;
        set { OnPropertyChanging(nameof(UserID)); _userID = value; OnPropertyChanged(nameof(UserID)); }
    }
}
