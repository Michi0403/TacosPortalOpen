//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessIntro.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramBusinessIntro : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private string _message = string.Empty;
    private TelegramSticker _sticker = null!;
    private Guid? _stickerID;

    private string _title = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));


    public virtual string Message
    {
        get => _message;
        set
        {
            OnPropertyChanging(nameof(Message));
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }


    public virtual TelegramSticker Sticker
    {
        get => _sticker;
        set
        {
            OnPropertyChanging(nameof(Sticker));
            _sticker = value;
            OnPropertyChanged(nameof(Sticker));
        }
    }

    [ForeignKey("Sticker")]
    public virtual Guid? StickerID
    {
        get => _stickerID;
        set
        {
            OnPropertyChanging(nameof(StickerID));
            _stickerID = value;
            OnPropertyChanged(nameof(StickerID));
        }
    }


    public virtual string Title
    {
        get => _title;
        set
        {
            OnPropertyChanging(nameof(Title));
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
}
