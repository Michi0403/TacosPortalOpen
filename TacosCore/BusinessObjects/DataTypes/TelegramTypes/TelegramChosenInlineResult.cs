//-----------------------------------------------------------------------
// <copyright file="TelegramChosenInlineResult.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChosenInlineResult : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private string _inlineMessageId = string.Empty;
    private TelegramLocation _location = null!;
    private Guid? _locationID;
    private string _query = string.Empty;

    private string _resultId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ChosenInlineResultThisUserBelongsTo))]
    public virtual TelegramUser From
    {
        get => _from;
        set { OnPropertyChanging(nameof(From)); _from = value; OnPropertyChanged(nameof(From)); }
    }

    [ForeignKey("From")]
    public virtual Guid? FromID
    {
        get => _fromID;
        set { OnPropertyChanging(nameof(FromID)); _fromID = value; OnPropertyChanged(nameof(FromID)); }
    }


    public virtual string InlineMessageId
    {
        get => _inlineMessageId;
        set { OnPropertyChanging(nameof(InlineMessageId)); _inlineMessageId = value; OnPropertyChanged(nameof(InlineMessageId)); }
    }


    [InverseProperty(nameof(Location.ChosenInlineResultThisLocationBelongsTo))]
    public virtual TelegramLocation Location
    {
        get => _location;
        set { OnPropertyChanging(nameof(Location)); _location = value; OnPropertyChanged(nameof(Location)); }
    }

    [ForeignKey("Location")]
    public virtual Guid? LocationID
    {
        get => _locationID;
        set { OnPropertyChanging(nameof(LocationID)); _locationID = value; OnPropertyChanged(nameof(LocationID)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Query
    {
        get => _query;
        set { OnPropertyChanging(nameof(Query)); _query = value; OnPropertyChanged(nameof(Query)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ResultId
    {
        get => _resultId;
        set { OnPropertyChanging(nameof(ResultId)); _resultId = value; OnPropertyChanged(nameof(ResultId)); }
    }
}
