//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessConnection.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;


[Authorize]
[DefaultClassOptions]
public partial class TelegramBusinessConnection : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _businessConnectionId = string.Empty;
    private DateTime _date;
    private bool _isEnabled;
    private TelegramBusinessBotRights _rights = null!;
    private Guid? _rightsID;
    private TelegramUser _user = null!;
    private long _userChatId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string BusinessConnectionId
    {
        get => _businessConnectionId;
        set
        {
            OnPropertyChanging(nameof(BusinessConnectionId));
            _businessConnectionId = value;
            OnPropertyChanged(nameof(BusinessConnectionId));
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime Date
    {
        get => _date;
        set
        {
            OnPropertyChanging(nameof(Date));
            _date = value;
            OnPropertyChanged(nameof(Date));
        }
    }


    public virtual bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            OnPropertyChanging(nameof(IsEnabled));
            _isEnabled = value;
            OnPropertyChanged(nameof(IsEnabled));
        }
    }


    [InverseProperty(nameof(TelegramBusinessBotRights.BusinessConnectionThisBusinessBotRightsBelongsTo))]
    public virtual TelegramBusinessBotRights Rights
    {
        get => _rights;
        set
        {
            OnPropertyChanging(nameof(Rights));
            _rights = value;
            OnPropertyChanged(nameof(Rights));
        }
    }

    [ForeignKey("Rights")]
    public virtual Guid? RightsID
    {
        get => _rightsID;
        set
        {
            OnPropertyChanging(nameof(RightsID));
            _rightsID = value;
            OnPropertyChanged(nameof(RightsID));
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(User.BusinessConnectionThisUserBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set
        {
            OnPropertyChanging(nameof(User));
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [LongIntIDSanity]
    public virtual long UserChatId
    {
        get => _userChatId;
        set
        {
            OnPropertyChanging(nameof(UserChatId));
            _userChatId = value;
            OnPropertyChanged(nameof(UserChatId));
        }
    }
}
