//-----------------------------------------------------------------------
// <copyright file="TelegramPreparedInlineMessage.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPreparedInlineMessage : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private DateTime _expirationDate;

    private string _preparedInlineMessageId = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime ExpirationDate
    {
        get => _expirationDate;
        set
        {
            OnPropertyChanging(nameof(ExpirationDate));
            _expirationDate = value;
            OnPropertyChanged(nameof(ExpirationDate));
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public virtual string PreparedInlineMessageId
    {
        get => _preparedInlineMessageId;
        set
        {
            OnPropertyChanging(nameof(PreparedInlineMessageId));
            _preparedInlineMessageId = value;
            OnPropertyChanged(nameof(PreparedInlineMessageId));
        }
    }
}