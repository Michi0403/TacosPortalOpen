//-----------------------------------------------------------------------
// <copyright file="TelegramIdDocumentData.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramIdDocumentData : BaseObject, IDecryptedValue, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _documentNo = string.Empty;
    private string _expiryDate = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string DocumentNo
    {
        get => _documentNo;
        set
        {
            OnPropertyChanging(nameof(DocumentNo));
            _documentNo = value;
            OnPropertyChanged(nameof(DocumentNo));
        }
    }


    public virtual string ExpiryDate
    {
        get => _expiryDate;
        set
        {
            OnPropertyChanging(nameof(ExpiryDate));
            _expiryDate = value;
            OnPropertyChanged(nameof(ExpiryDate));
        }
    }
}
