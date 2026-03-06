//-----------------------------------------------------------------------
// <copyright file="TelegramSecureValue.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramSecureValue : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private TelegramDataCredentials _data = null!;
    private TelegramFileCredentials _frontSide = null!;
    private TelegramFileCredentials _reverseSide = null!;
    private TelegramFileCredentials _selfie = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    public virtual TelegramDataCredentials Data
    {
        get => _data;
        set { OnPropertyChanging(nameof(Data)); _data = value; OnPropertyChanged(nameof(Data)); }
    }

    [ForeignKey("Data")]
    public virtual Guid? DataID { get; set; }

    [InverseProperty(nameof(TelegramFileCredentials.SecureFiles))]
    public virtual IList<TelegramFileCredentials>? Files { get; set; } = new ObservableCollection<TelegramFileCredentials>();

    [InverseProperty(nameof(TelegramFileCredentials.SecureFrontSideValues))]
    public virtual TelegramFileCredentials FrontSide
    {
        get => _frontSide;
        set { OnPropertyChanging(nameof(FrontSide)); _frontSide = value; OnPropertyChanged(nameof(FrontSide)); }
    }

    [ForeignKey("FrontSide")]
    public virtual Guid? FrontSideID { get; set; }

    [InverseProperty(nameof(TelegramFileCredentials.SecureReverseSideValues))]
    public virtual TelegramFileCredentials ReverseSide
    {
        get => _reverseSide;
        set { OnPropertyChanging(nameof(ReverseSide)); _reverseSide = value; OnPropertyChanged(nameof(ReverseSide)); }
    }

    [ForeignKey("ReverseSide")]
    public virtual Guid? ReverseSideID { get; set; }

    [InverseProperty(nameof(TelegramFileCredentials.SecureSelfieValues))]
    public virtual TelegramFileCredentials Selfie
    {
        get => _selfie;
        set { OnPropertyChanging(nameof(Selfie)); _selfie = value; OnPropertyChanged(nameof(Selfie)); }
    }

    [ForeignKey("Selfie")]
    public virtual Guid? SelfieID { get; set; }

    [InverseProperty(nameof(TelegramFileCredentials.SecureTranslations))]
    public virtual IList<TelegramFileCredentials>? Translation { get; set; } = new ObservableCollection<TelegramFileCredentials>();
}
