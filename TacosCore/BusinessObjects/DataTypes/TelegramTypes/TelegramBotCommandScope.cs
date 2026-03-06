//-----------------------------------------------------------------------
// <copyright file="TelegramBotCommandScope.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TacosCore.Attributes;
using Telegram.Bot.Types.Enums;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes
{
    public abstract partial class TelegramBotCommandScope : BaseObject
    {
        public abstract BotCommandScopeType Type { get; }
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeDefault : TelegramBotCommandScope
    {
        public override BotCommandScopeType Type => BotCommandScopeType.Default;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeAllPrivateChats : TelegramBotCommandScope
    {
        public override BotCommandScopeType Type => BotCommandScopeType.AllPrivateChats;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeAllGroupChats : TelegramBotCommandScope
    {
        public override BotCommandScopeType Type => BotCommandScopeType.AllGroupChats;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeAllChatAdministrators : TelegramBotCommandScope
    {
        public override BotCommandScopeType Type => BotCommandScopeType.AllChatAdministrators;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeChat : TelegramBotCommandScope, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramChatId _chatId = null!;

        private Guid? _chatIdID;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual TelegramChatId ChatId
        {
            get => _chatId;
            set
            {
                OnPropertyChanging(nameof(ChatId));
                _chatId = value;
                OnPropertyChanged(nameof(ChatId));
            }
        }
        [ForeignKey("ChatId")]
        public virtual Guid? ChatIdID
        {
            get => _chatIdID;
            set
            {
                OnPropertyChanging(nameof(ChatIdID));
                _chatIdID = value;
                OnPropertyChanged(nameof(ChatIdID));
            }
        }

        public override BotCommandScopeType Type => BotCommandScopeType.Chat;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeChatAdministrators : TelegramBotCommandScope, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramChatId _chatId = null!;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual TelegramChatId ChatId
        {
            get => _chatId;
            set
            {
                OnPropertyChanging(nameof(ChatId));
                _chatId = value;
                OnPropertyChanged(nameof(ChatId));
            }
        }

        public override BotCommandScopeType Type => BotCommandScopeType.ChatAdministrators;
    }

    [Authorize]
    [DefaultClassOptions]
    public partial class TelegramBotCommandScopeChatMember : TelegramBotCommandScope, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private TelegramChatId _chatId = null!;

        private Guid? _chatIdID;

        private long _userId;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void OnPropertyChanging(string propertyName) =>
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public virtual TelegramChatId ChatId
        {
            get => _chatId;
            set
            {
                OnPropertyChanging(nameof(ChatId));
                _chatId = value;
                OnPropertyChanged(nameof(ChatId));
            }
        }
        [ForeignKey("ChatId")]
        public virtual Guid? ChatIdID
        {
            get => _chatIdID;
            set
            {
                OnPropertyChanging(nameof(ChatIdID));
                _chatIdID = value;
                OnPropertyChanged(nameof(ChatIdID));
            }
        }

        public override BotCommandScopeType Type => BotCommandScopeType.ChatMember;
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [LongIntIDSanity]
        public virtual long UserId
        {
            get => _userId;
            set
            {
                OnPropertyChanging(nameof(UserId));
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
    }
}
