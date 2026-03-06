//-----------------------------------------------------------------------
// <copyright file="TelegramContact.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
using TacosCore.Attributes;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramContact : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _firstName = string.Empty;
    private string? _lastName;

    private string _phoneNumber = string.Empty;
    private long? _userId;
    private string? _vcard;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramExternalReplyInfo.Contact))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisContactBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string FirstName
    {
        get => _firstName;
        set { OnPropertyChanging(nameof(FirstName)); _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }


    public virtual string? LastName
    {
        get => _lastName;
        set { OnPropertyChanging(nameof(LastName)); _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    [InverseProperty(nameof(TelegramMessage.Contact))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisContactBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string PhoneNumber
    {
        get => _phoneNumber;
        set { OnPropertyChanging(nameof(PhoneNumber)); _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
    }


    [LongIntIDSanity]
    public virtual long? UserId
    {
        get => _userId;
        set { OnPropertyChanging(nameof(UserId)); _userId = value; OnPropertyChanged(nameof(UserId)); }
    }


    public virtual string? Vcard
    {
        get => _vcard;
        set { OnPropertyChanging(nameof(Vcard)); _vcard = value; OnPropertyChanged(nameof(Vcard)); }
    }
}
