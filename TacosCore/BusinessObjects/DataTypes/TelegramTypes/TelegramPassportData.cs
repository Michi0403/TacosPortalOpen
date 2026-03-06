//-----------------------------------------------------------------------
// <copyright file="TelegramPassportData.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramPassportData : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramEncryptedCredentials? _credentials;
    private Guid? _credentialsId;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramEncryptedCredentials.PassportDataThisEncryptedCredentialsBelongsTo))]
    public virtual TelegramEncryptedCredentials? Credentials
    {
        get => _credentials;
        set { OnPropertyChanging(nameof(Credentials)); _credentials = value; OnPropertyChanged(nameof(Credentials)); }
    }

    [ForeignKey("Credentials")]
    public virtual Guid? CredentialsID
    {
        get => _credentialsId;
        set { OnPropertyChanging(nameof(CredentialsID)); _credentialsId = value; OnPropertyChanged(nameof(CredentialsID)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramEncryptedPassportElement.PassportDataThisEncryptedPassportElementBelongsTo))]
    public virtual IList<TelegramEncryptedPassportElement>? Data { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();




    [InverseProperty(nameof(TelegramMessage.PassportData))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisPassportDataBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();
}
