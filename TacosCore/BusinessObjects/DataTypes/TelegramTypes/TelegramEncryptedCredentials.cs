//-----------------------------------------------------------------------
// <copyright file="TelegramEncryptedCredentials.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramEncryptedCredentials : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _data = string.Empty;
    private string _hash = string.Empty;
    private string _secret = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Data
    {
        get => _data;
        set { OnPropertyChanging(nameof(Data)); _data = value; OnPropertyChanged(nameof(Data)); }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Hash
    {
        get => _hash;
        set { OnPropertyChanging(nameof(Hash)); _hash = value; OnPropertyChanged(nameof(Hash)); }
    }

    [InverseProperty(nameof(TelegramPassportData.Credentials))]
    public virtual IList<TelegramPassportData>? PassportDataThisEncryptedCredentialsBelongsTo { get; set; } = new ObservableCollection<TelegramPassportData>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Secret
    {
        get => _secret;
        set { OnPropertyChanging(nameof(Secret)); _secret = value; OnPropertyChanged(nameof(Secret)); }
    }
}
