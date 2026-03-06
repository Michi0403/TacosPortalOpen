//-----------------------------------------------------------------------
// <copyright file="TelegramInputFile.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
public abstract partial class TelegramInputFile : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
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
public partial class TelegramInputFileId : TelegramInputFile
{
    private string _fileId = string.Empty;

    public virtual string FileId
    {
        get => _fileId;
        set { OnPropertyChanging(nameof(FileId)); _fileId = value; OnPropertyChanged(nameof(FileId)); }
    }

    public virtual FileType FileType => FileType.Id;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramInputFileStream : TelegramInputFile
{
    private Stream? _content;
    private string _fileName = string.Empty;

    [NotMapped]
    public virtual Stream? Content
    {
        get => _content;
        set { OnPropertyChanging(nameof(Content)); _content = value; OnPropertyChanged(nameof(Content)); }
    }

    public virtual string FileName
    {
        get => _fileName;
        init { OnPropertyChanging(nameof(FileName)); _fileName = value; OnPropertyChanged(nameof(FileName)); }
    }

    public virtual FileType FileType => FileType.Stream;
}


[Authorize]
[DefaultClassOptions]
public partial class TelegramInputFileUrl : TelegramInputFile
{
    private Uri _url = new("https://example.com");

    public virtual FileType FileType => FileType.Url;

    [Required]
    public virtual Uri Url
    {
        get => _url;
        set { OnPropertyChanging(nameof(Url)); _url = value; OnPropertyChanged(nameof(Url)); }
    }
}
