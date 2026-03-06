//-----------------------------------------------------------------------
// <copyright file="TelegramLinkPreviewOptions.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramLinkPreviewOptions : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private bool _isDisabled;
    private bool _preferLargeMedia;
    private bool _preferSmallMedia;
    private bool _showAboveText;
    private string _url = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramExternalReplyInfo.LinkPreviewOptions))]
    [JsonIgnore]
    public virtual IList<TelegramExternalReplyInfo>? ExternalReplyThisLinkPreviewOptionsBelongsTo { get; set; } = new ObservableCollection<TelegramExternalReplyInfo>();

    public virtual bool IsDisabled
    {
        get => _isDisabled;
        set
        {
            if (_isDisabled != value)
            {
                OnPropertyChanging(nameof(IsDisabled));
                _isDisabled = value;
                OnPropertyChanged(nameof(IsDisabled));
            }
        }
    }

    [InverseProperty(nameof(TelegramMessage.LinkPreviewOptions))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisLinkPreviewOptionBelongsTo { get; set; } = new ObservableCollection<TelegramMessage>();

    public virtual bool PreferLargeMedia
    {
        get => _preferLargeMedia;
        set
        {
            if (_preferLargeMedia != value)
            {
                OnPropertyChanging(nameof(PreferLargeMedia));
                _preferLargeMedia = value;
                OnPropertyChanged(nameof(PreferLargeMedia));
            }
        }
    }

    public virtual bool PreferSmallMedia
    {
        get => _preferSmallMedia;
        set
        {
            if (_preferSmallMedia != value)
            {
                OnPropertyChanging(nameof(PreferSmallMedia));
                _preferSmallMedia = value;
                OnPropertyChanged(nameof(PreferSmallMedia));
            }
        }
    }

    public virtual bool ShowAboveText
    {
        get => _showAboveText;
        set
        {
            if (_showAboveText != value)
            {
                OnPropertyChanging(nameof(ShowAboveText));
                _showAboveText = value;
                OnPropertyChanged(nameof(ShowAboveText));
            }
        }
    }

    public virtual string Url
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                OnPropertyChanging(nameof(Url));
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }
    }
}
