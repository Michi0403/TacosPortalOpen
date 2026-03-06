//-----------------------------------------------------------------------
// <copyright file="TelegramInlineQuery.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramInlineQuery : TelegramUpdate
{
    private ChatType? _chatType;
    private TelegramUser _from = null!;
    private Guid? _fromID;
    private string _inlineQueryId = string.Empty;
    private TelegramLocation _location = null!;
    private Guid? _locationID;
    private string _offset = string.Empty;
    private string _query = string.Empty;

    public virtual ChatType? ChatType
    {
        get => _chatType;
        set
        {
            OnPropertyChanging(nameof(ChatType));
            _chatType = value;
            OnPropertyChanged(nameof(ChatType));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.InlineQueryThisUserBelongsTo))]
    public virtual TelegramUser From
    {
        get => _from;
        set
        {
            OnPropertyChanging(nameof(From));
            _from = value;
            OnPropertyChanged(nameof(From));
        }
    }

    [ForeignKey(nameof(From))]
    public virtual Guid? FromID
    {
        get => _fromID;
        set
        {
            OnPropertyChanging(nameof(FromID));
            _fromID = value;
            OnPropertyChanged(nameof(FromID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string InlineQueryId
    {
        get => _inlineQueryId;
        set
        {
            OnPropertyChanging(nameof(InlineQueryId));
            _inlineQueryId = value;
            OnPropertyChanged(nameof(InlineQueryId));
        }
    }

    [InverseProperty(nameof(Location.InlineQueryThisLocationBelongsTo))]
    public virtual TelegramLocation Location
    {
        get => _location;
        set
        {
            OnPropertyChanging(nameof(Location));
            _location = value;
            OnPropertyChanged(nameof(Location));
        }
    }

    [ForeignKey(nameof(Location))]
    public virtual Guid? LocationID
    {
        get => _locationID;
        set
        {
            OnPropertyChanging(nameof(LocationID));
            _locationID = value;
            OnPropertyChanged(nameof(LocationID));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Offset
    {
        get => _offset;
        set
        {
            OnPropertyChanging(nameof(Offset));
            _offset = value;
            OnPropertyChanged(nameof(Offset));
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string Query
    {
        get => _query;
        set
        {
            OnPropertyChanging(nameof(Query));
            _query = value;
            OnPropertyChanged(nameof(Query));
        }
    }
}