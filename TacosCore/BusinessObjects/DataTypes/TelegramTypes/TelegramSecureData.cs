//-----------------------------------------------------------------------
// <copyright file="TelegramSecureData.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramSecureData : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramSecureValue _address = null!;
    private TelegramSecureValue _bankStatement = null!;
    private TelegramSecureValue _driverLicense = null!;
    private TelegramSecureValue _identityCard = null!;
    private TelegramSecureValue _internalPassport = null!;
    private TelegramSecureValue _passport = null!;
    private TelegramSecureValue _passportRegistration = null!;

    private TelegramSecureValue _personalDetails = null!;
    private TelegramSecureValue _rentalAgreement = null!;
    private TelegramSecureValue _temporaryRegistration = null!;
    private TelegramSecureValue _utilityBill = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramSecureValue Address
    {
        get => _address;
        set { OnPropertyChanging(nameof(Address)); _address = value; OnPropertyChanged(nameof(Address)); }
    }

    [ForeignKey("Address")]
    public virtual Guid? AddressID { get; set; }

    public virtual TelegramSecureValue BankStatement
    {
        get => _bankStatement;
        set { OnPropertyChanging(nameof(BankStatement)); _bankStatement = value; OnPropertyChanged(nameof(BankStatement)); }
    }

    [ForeignKey("BankStatement")]
    public virtual Guid? BankStatementID { get; set; }

    public virtual TelegramSecureValue DriverLicense
    {
        get => _driverLicense;
        set { OnPropertyChanging(nameof(DriverLicense)); _driverLicense = value; OnPropertyChanged(nameof(DriverLicense)); }
    }

    [ForeignKey("DriverLicense")]
    public virtual Guid? DriverLicenseID { get; set; }

    public virtual TelegramSecureValue IdentityCard
    {
        get => _identityCard;
        set { OnPropertyChanging(nameof(IdentityCard)); _identityCard = value; OnPropertyChanged(nameof(IdentityCard)); }
    }

    [ForeignKey("IdentityCard")]
    public virtual Guid? IdentityCardID { get; set; }

    public virtual TelegramSecureValue InternalPassport
    {
        get => _internalPassport;
        set { OnPropertyChanging(nameof(InternalPassport)); _internalPassport = value; OnPropertyChanged(nameof(InternalPassport)); }
    }

    [ForeignKey("InternalPassport")]
    public virtual Guid? InternalPassportID { get; set; }

    public virtual TelegramSecureValue Passport
    {
        get => _passport;
        set { OnPropertyChanging(nameof(Passport)); _passport = value; OnPropertyChanged(nameof(Passport)); }
    }

    [ForeignKey("Passport")]
    public virtual Guid? PassportID { get; set; }

    public virtual TelegramSecureValue PassportRegistration
    {
        get => _passportRegistration;
        set { OnPropertyChanging(nameof(PassportRegistration)); _passportRegistration = value; OnPropertyChanged(nameof(PassportRegistration)); }
    }

    [ForeignKey("PassportRegistration")]
    public virtual Guid? PassportRegistrationID { get; set; }

    public virtual TelegramSecureValue PersonalDetails
    {
        get => _personalDetails;
        set { OnPropertyChanging(nameof(PersonalDetails)); _personalDetails = value; OnPropertyChanged(nameof(PersonalDetails)); }
    }

    [ForeignKey("PersonalDetails")]
    public virtual Guid? PersonalDetailsID { get; set; }

    public virtual TelegramSecureValue RentalAgreement
    {
        get => _rentalAgreement;
        set { OnPropertyChanging(nameof(RentalAgreement)); _rentalAgreement = value; OnPropertyChanged(nameof(RentalAgreement)); }
    }

    [ForeignKey("RentalAgreement")]
    public virtual Guid? RentalAgreementID { get; set; }

    public virtual TelegramSecureValue TemporaryRegistration
    {
        get => _temporaryRegistration;
        set { OnPropertyChanging(nameof(TemporaryRegistration)); _temporaryRegistration = value; OnPropertyChanged(nameof(TemporaryRegistration)); }
    }

    [ForeignKey("TemporaryRegistration")]
    public virtual Guid? TemporaryRegistrationID { get; set; }

    public virtual TelegramSecureValue UtilityBill
    {
        get => _utilityBill;
        set { OnPropertyChanging(nameof(UtilityBill)); _utilityBill = value; OnPropertyChanged(nameof(UtilityBill)); }
    }

    [ForeignKey("UtilityBill")]
    public virtual Guid? UtilityBillID { get; set; }
}