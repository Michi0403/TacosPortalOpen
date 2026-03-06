//-----------------------------------------------------------------------
// <copyright file="TelegramFileCredentials.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramFileCredentials : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _fileHash = string.Empty;
    private string _secret = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash
    {
        get => _fileHash;
        set { OnPropertyChanging(nameof(FileHash)); _fileHash = value; OnPropertyChanged(nameof(FileHash)); }
    }


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Secret
    {
        get => _secret;
        set { OnPropertyChanging(nameof(Secret)); _secret = value; OnPropertyChanged(nameof(Secret)); }
    }

    [InverseProperty(nameof(TelegramSecureValue.Files))]
    public virtual IList<TelegramSecureValue>? SecureFiles { get; set; } = new ObservableCollection<TelegramSecureValue>();

    [InverseProperty(nameof(TelegramSecureValue.FrontSide))]
    public virtual IList<TelegramSecureValue>? SecureFrontSideValues { get; set; } = new ObservableCollection<TelegramSecureValue>();

    [InverseProperty(nameof(TelegramSecureValue.ReverseSide))]
    public virtual IList<TelegramSecureValue>? SecureReverseSideValues { get; set; } = new ObservableCollection<TelegramSecureValue>();

    [InverseProperty(nameof(TelegramSecureValue.Selfie))]
    public virtual IList<TelegramSecureValue>? SecureSelfieValues { get; set; } = new ObservableCollection<TelegramSecureValue>();

    [InverseProperty(nameof(TelegramSecureValue.Translation))]
    public virtual IList<TelegramSecureValue>? SecureTranslations { get; set; } = new ObservableCollection<TelegramSecureValue>();
}
