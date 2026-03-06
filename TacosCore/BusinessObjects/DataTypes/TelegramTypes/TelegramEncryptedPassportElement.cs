//-----------------------------------------------------------------------
// <copyright file="TelegramEncryptedPassportElement.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramEncryptedPassportElement : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _data = string.Empty;
    private string _email = string.Empty;
    private TelegramPassportFile _frontSide = null!;
    private string _hash = string.Empty;
    private string _phoneNumber = string.Empty;
    private TelegramPassportFile _reverseSide = null!;
    private TelegramPassportFile _selfie = null!;

    private EncryptedPassportElementType _type;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual string Data
    {
        get => _data;
        set { OnPropertyChanging(nameof(Data)); _data = value; OnPropertyChanged(nameof(Data)); }
    }

    public virtual string Email
    {
        get => _email;
        set { OnPropertyChanging(nameof(Email)); _email = value; OnPropertyChanged(nameof(Email)); }
    }

    [InverseProperty(nameof(TelegramPassportFile.EncryptedPasswordElementFiles))]
    public virtual IList<TelegramPassportFile>? Files { get; set; } = new ObservableCollection<TelegramPassportFile>();

    [InverseProperty(nameof(TelegramPassportFile.EncryptionElementFrontside))]
    public virtual TelegramPassportFile? FrontSide
    {
        get => _frontSide;
        set { OnPropertyChanging(nameof(FrontSide)); _frontSide = value; OnPropertyChanged(nameof(FrontSide)); }
    }

    [ForeignKey("FrontSide")]
    public virtual Guid? FrontSideID { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Hash
    {
        get => _hash;
        set { OnPropertyChanging(nameof(Hash)); _hash = value; OnPropertyChanged(nameof(Hash)); }
    }

    [InverseProperty(nameof(TelegramPassportData.Data))]
    public virtual IList<TelegramPassportData>? PassportDataThisEncryptedPassportElementBelongsTo { get; set; } = new ObservableCollection<TelegramPassportData>();

    public virtual string PhoneNumber
    {
        get => _phoneNumber;
        set { OnPropertyChanging(nameof(PhoneNumber)); _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
    }

    [InverseProperty(nameof(TelegramPassportFile.EncryptionElementReverseSide))]
    public virtual TelegramPassportFile ReverseSide
    {
        get => _reverseSide;
        set { OnPropertyChanging(nameof(ReverseSide)); _reverseSide = value; OnPropertyChanged(nameof(ReverseSide)); }
    }

    [ForeignKey("ReverseSide")]
    public virtual Guid? ReverseSideID { get; set; }

    [InverseProperty(nameof(TelegramPassportFile.EncryptionElementSelfie))]
    public virtual TelegramPassportFile Selfie
    {
        get => _selfie;
        set { OnPropertyChanging(nameof(Selfie)); _selfie = value; OnPropertyChanged(nameof(Selfie)); }
    }

    [ForeignKey("Selfie")]
    public virtual Guid? SelfieID { get; set; }

    [InverseProperty(nameof(TelegramPassportFile.EncryptedPasswordElementTranslations))]
    public virtual IList<TelegramPassportFile> Translation { get; set; } = new ObservableCollection<TelegramPassportFile>();

    [ForeignKey("Translation")]
    public virtual Guid? TranslationID { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual EncryptedPassportElementType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }
}
