//-----------------------------------------------------------------------
// <copyright file="TelegramCredentials.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramCredentials : BaseObject, IDecryptedValue, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _nonce = string.Empty;
    private TelegramSecureData _secureData = null!;

    private Guid? _secureDataID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Nonce
    {
        get => _nonce;
        set { OnPropertyChanging(nameof(Nonce)); _nonce = value; OnPropertyChanged(nameof(Nonce)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual TelegramSecureData SecureData
    {
        get => _secureData;
        set { OnPropertyChanging(nameof(SecureData)); _secureData = value; OnPropertyChanged(nameof(SecureData)); }
    }

    [ForeignKey("SecureData")]
    public virtual Guid? SecureDataID
    {
        get => _secureDataID;
        set { OnPropertyChanging(nameof(SecureDataID)); _secureDataID = value; OnPropertyChanged(nameof(SecureDataID)); }
    }
}
