
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public abstract partial class TelegramPassportElementError : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _message = string.Empty;

    private EncryptedPassportElementType _type;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Message
    {
        get => _message;
        set { OnPropertyChanging(nameof(Message)); _message = value; OnPropertyChanged(nameof(Message)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual EncryptedPassportElementType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorDataField : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string DataHash { get; set; } = string.Empty;


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FieldName { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.Data;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorFrontSide : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.FrontSide;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorReverseSide : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.ReverseSide;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorSelfie : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.Selfie;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorFile : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.File;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorFiles : TelegramPassportElementError
{




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<string>? FileHashes { get; set; } = new ObservableCollection<string>();

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.Files;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorTranslationFile : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FileHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.TranslationFile;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorTranslationFiles : TelegramPassportElementError
{




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<string>? FileHashes { get; set; } = new ObservableCollection<string>();

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.TranslationFiles;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportElementErrorUnspecified : TelegramPassportElementError
{


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string ElementHash { get; set; } = string.Empty;

    public virtual PassportElementErrorSource Source => PassportElementErrorSource.Unspecified;
}
