//-----------------------------------------------------------------------
// <copyright file="TelegramPassportFile.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportFile : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{

    private DateTime _fileDate;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramEncryptedPassportElement.Files))]
    public virtual IList<TelegramEncryptedPassportElement>? EncryptedPasswordElementFiles { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();

    [InverseProperty(nameof(TelegramEncryptedPassportElement.Translation))]
    public virtual IList<TelegramEncryptedPassportElement>? EncryptedPasswordElementTranslations { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();

    [InverseProperty(nameof(TelegramEncryptedPassportElement.FrontSide))]
    public virtual IList<TelegramEncryptedPassportElement>? EncryptionElementFrontside { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();

    [InverseProperty(nameof(TelegramEncryptedPassportElement.ReverseSide))]
    public virtual IList<TelegramEncryptedPassportElement>? EncryptionElementReverseSide { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();

    [InverseProperty(nameof(TelegramEncryptedPassportElement.Selfie))]
    public virtual IList<TelegramEncryptedPassportElement>? EncryptionElementSelfie { get; set; } = new ObservableCollection<TelegramEncryptedPassportElement>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual DateTime FileDate
    {
        get => _fileDate;
        set { OnPropertyChanging(nameof(FileDate)); _fileDate = value; OnPropertyChanged(nameof(FileDate)); }
    }
}
