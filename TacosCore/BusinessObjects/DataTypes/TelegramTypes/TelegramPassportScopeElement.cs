//-----------------------------------------------------------------------
// <copyright file="TelegramPassportScopeElement.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Passport;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramPassportScopeElement : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportScopeElementOneOfSeveral : TelegramPassportScopeElement
{
    private bool _selfie;
    private bool _translation;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramPassportScopeElementOne>? OneOf { get; set; } = new ObservableCollection<TelegramPassportScopeElementOne>();

    public virtual bool Selfie
    {
        get => _selfie;
        set { OnPropertyChanging(nameof(Selfie)); _selfie = value; OnPropertyChanged(nameof(Selfie)); }
    }

    public virtual bool Translation
    {
        get => _translation;
        set { OnPropertyChanging(nameof(Translation)); _translation = value; OnPropertyChanged(nameof(Translation)); }
    }
}

[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportScopeElementOne : TelegramPassportScopeElement
{
    private bool _nativeNames;
    private bool _selfie;
    private bool _translation;
    private EncryptedPassportElementType _type;

    public virtual bool NativeNames
    {
        get => _nativeNames;
        set { OnPropertyChanging(nameof(NativeNames)); _nativeNames = value; OnPropertyChanged(nameof(NativeNames)); }
    }

    public virtual bool Selfie
    {
        get => _selfie;
        set { OnPropertyChanging(nameof(Selfie)); _selfie = value; OnPropertyChanged(nameof(Selfie)); }
    }

    public virtual bool Translation
    {
        get => _translation;
        set { OnPropertyChanging(nameof(Translation)); _translation = value; OnPropertyChanged(nameof(Translation)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual EncryptedPassportElementType Type
    {
        get => _type;
        set { OnPropertyChanging(nameof(Type)); _type = value; OnPropertyChanged(nameof(Type)); }
    }
}
