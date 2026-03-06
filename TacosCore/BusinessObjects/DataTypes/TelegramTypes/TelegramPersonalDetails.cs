
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPersonalDetails : BaseObject, IDecryptedValue, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _middleName = string.Empty;
    private string _birthDate = string.Empty;
    private string _gender = string.Empty;
    private string _countryCode = string.Empty;
    private string _residenceCountryCode = string.Empty;
    private string _firstNameNative = string.Empty;
    private string _lastNameNative = string.Empty;
    private string _middleNameNative = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FirstName
    {
        get => _firstName;
        set { OnPropertyChanging(nameof(FirstName)); _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string LastName
    {
        get => _lastName;
        set { OnPropertyChanging(nameof(LastName)); _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    public virtual string MiddleName
    {
        get => _middleName;
        set { OnPropertyChanging(nameof(MiddleName)); _middleName = value; OnPropertyChanged(nameof(MiddleName)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string BirthDate
    {
        get => _birthDate;
        set { OnPropertyChanging(nameof(BirthDate)); _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Gender
    {
        get => _gender;
        set { OnPropertyChanging(nameof(Gender)); _gender = value; OnPropertyChanged(nameof(Gender)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string CountryCode
    {
        get => _countryCode;
        set { OnPropertyChanging(nameof(CountryCode)); _countryCode = value; OnPropertyChanged(nameof(CountryCode)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ResidenceCountryCode
    {
        get => _residenceCountryCode;
        set { OnPropertyChanging(nameof(ResidenceCountryCode)); _residenceCountryCode = value; OnPropertyChanged(nameof(ResidenceCountryCode)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FirstNameNative
    {
        get => _firstNameNative;
        set { OnPropertyChanging(nameof(FirstNameNative)); _firstNameNative = value; OnPropertyChanged(nameof(FirstNameNative)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string LastNameNative
    {
        get => _lastNameNative;
        set { OnPropertyChanging(nameof(LastNameNative)); _lastNameNative = value; OnPropertyChanged(nameof(LastNameNative)); }
    }

    public virtual string MiddleNameNative
    {
        get => _middleNameNative;
        set { OnPropertyChanging(nameof(MiddleNameNative)); _middleNameNative = value; OnPropertyChanged(nameof(MiddleNameNative)); }
    }
}
