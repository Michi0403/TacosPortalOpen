//-----------------------------------------------------------------------
// <copyright file="TelegramBusinessBotRights.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramBusinessBotRights : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _canChangeGiftSettings;
    private bool _canConvertGiftsToStars;
    private bool _canDeleteAllMessages;
    private bool _canDeleteSentMessages;
    private bool _canEditBio;
    private bool _canEditName;
    private bool _canEditProfilePhoto;
    private bool _canEditUsername;
    private bool _canManageStories;
    private bool _canReadMessages;

    private bool _canReply;
    private bool _canTransferAndUpgradeGifts;
    private bool _canTransferStars;
    private bool _canViewGiftsAndStars;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramBusinessConnection.Rights))]
    public virtual IList<TelegramBusinessConnection>? BusinessConnectionThisBusinessBotRightsBelongsTo { get; set; } = new ObservableCollection<TelegramBusinessConnection>();

    public virtual bool CanChangeGiftSettings
    {
        get => _canChangeGiftSettings;
        set
        {
            OnPropertyChanging(nameof(CanChangeGiftSettings));
            _canChangeGiftSettings = value;
            OnPropertyChanged(nameof(CanChangeGiftSettings));
        }
    }

    public virtual bool CanConvertGiftsToStars
    {
        get => _canConvertGiftsToStars;
        set
        {
            OnPropertyChanging(nameof(CanConvertGiftsToStars));
            _canConvertGiftsToStars = value;
            OnPropertyChanged(nameof(CanConvertGiftsToStars));
        }
    }

    public virtual bool CanDeleteAllMessages
    {
        get => _canDeleteAllMessages;
        set
        {
            OnPropertyChanging(nameof(CanDeleteAllMessages));
            _canDeleteAllMessages = value;
            OnPropertyChanged(nameof(CanDeleteAllMessages));
        }
    }

    public virtual bool CanDeleteSentMessages
    {
        get => _canDeleteSentMessages;
        set
        {
            OnPropertyChanging(nameof(CanDeleteSentMessages));
            _canDeleteSentMessages = value;
            OnPropertyChanged(nameof(CanDeleteSentMessages));
        }
    }

    public virtual bool CanEditBio
    {
        get => _canEditBio;
        set
        {
            OnPropertyChanging(nameof(CanEditBio));
            _canEditBio = value;
            OnPropertyChanged(nameof(CanEditBio));
        }
    }

    public virtual bool CanEditName
    {
        get => _canEditName;
        set
        {
            OnPropertyChanging(nameof(CanEditName));
            _canEditName = value;
            OnPropertyChanged(nameof(CanEditName));
        }
    }

    public virtual bool CanEditProfilePhoto
    {
        get => _canEditProfilePhoto;
        set
        {
            OnPropertyChanging(nameof(CanEditProfilePhoto));
            _canEditProfilePhoto = value;
            OnPropertyChanged(nameof(CanEditProfilePhoto));
        }
    }

    public virtual bool CanEditUsername
    {
        get => _canEditUsername;
        set
        {
            OnPropertyChanging(nameof(CanEditUsername));
            _canEditUsername = value;
            OnPropertyChanged(nameof(CanEditUsername));
        }
    }

    public virtual bool CanManageStories
    {
        get => _canManageStories;
        set
        {
            OnPropertyChanging(nameof(CanManageStories));
            _canManageStories = value;
            OnPropertyChanged(nameof(CanManageStories));
        }
    }

    public virtual bool CanReadMessages
    {
        get => _canReadMessages;
        set
        {
            OnPropertyChanging(nameof(CanReadMessages));
            _canReadMessages = value;
            OnPropertyChanged(nameof(CanReadMessages));
        }
    }

    public virtual bool CanReply
    {
        get => _canReply;
        set
        {
            OnPropertyChanging(nameof(CanReply));
            _canReply = value;
            OnPropertyChanged(nameof(CanReply));
        }
    }

    public virtual bool CanTransferAndUpgradeGifts
    {
        get => _canTransferAndUpgradeGifts;
        set
        {
            OnPropertyChanging(nameof(CanTransferAndUpgradeGifts));
            _canTransferAndUpgradeGifts = value;
            OnPropertyChanged(nameof(CanTransferAndUpgradeGifts));
        }
    }

    public virtual bool CanTransferStars
    {
        get => _canTransferStars;
        set
        {
            OnPropertyChanging(nameof(CanTransferStars));
            _canTransferStars = value;
            OnPropertyChanged(nameof(CanTransferStars));
        }
    }

    public virtual bool CanViewGiftsAndStars
    {
        get => _canViewGiftsAndStars;
        set
        {
            OnPropertyChanging(nameof(CanViewGiftsAndStars));
            _canViewGiftsAndStars = value;
            OnPropertyChanged(nameof(CanViewGiftsAndStars));
        }
    }
}
