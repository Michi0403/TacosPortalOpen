//-----------------------------------------------------------------------
// <copyright file="TelegramTGFile.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramTGFile : TelegramFileBase, INotifyPropertyChanging, INotifyPropertyChanged
{

    private string _filePath = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));





    public virtual string FilePath
    {
        get => _filePath;
        set { OnPropertyChanging(nameof(FilePath)); _filePath = value; OnPropertyChanged(nameof(FilePath)); }
    }

    [InverseProperty(nameof(TelegramSticker.PremiumAnimation))]
    public virtual IList<TelegramSticker>? StickerThisTGFileBelongsTo { get; set; } = new ObservableCollection<TelegramSticker>();
}
