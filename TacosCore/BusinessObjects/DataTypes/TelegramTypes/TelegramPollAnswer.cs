//-----------------------------------------------------------------------
// <copyright file="TelegramPollAnswer.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPollAnswer : TelegramUpdate, INotifyPropertyChanging, INotifyPropertyChanged
{
    private TelegramPoll _poll = null!;

    private Guid? _telegramPollAnswerPollID;
    private TelegramUser _user = null!;
    private Guid? _userID;
    private TelegramChat _voterChat = null!;
    private Guid? _voterChatID;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<int>? OptionIds { get; set; } = new ObservableCollection<int>();

    [InverseProperty(nameof(TelegramPoll.PollAnswerThisPollBelongsTo))]
    public virtual TelegramPoll Poll
    {
        get => _poll;
        set { OnPropertyChanging(nameof(Poll)); _poll = value; OnPropertyChanged(nameof(Poll)); }
    }

    [ForeignKey(nameof(Poll))]
    public virtual Guid? TelegramPollAnswerPollID
    {
        get => _telegramPollAnswerPollID;
        set { OnPropertyChanging(nameof(TelegramPollAnswerPollID)); _telegramPollAnswerPollID = value; OnPropertyChanged(nameof(TelegramPollAnswerPollID)); }
    }

    [InverseProperty(nameof(TelegramUser.PollAnswerThisVoterUsersBelongsTo))]
    public virtual TelegramUser User
    {
        get => _user;
        set { OnPropertyChanging(nameof(User)); _user = value; OnPropertyChanged(nameof(User)); }
    }

    [ForeignKey(nameof(User))]
    public virtual Guid? UserID
    {
        get => _userID;
        set { OnPropertyChanging(nameof(UserID)); _userID = value; OnPropertyChanged(nameof(UserID)); }
    }

    [InverseProperty(nameof(TelegramChat.PollAnswerThisChatBelongsTo))]
    public virtual TelegramChat VoterChat
    {
        get => _voterChat;
        set { OnPropertyChanging(nameof(VoterChat)); _voterChat = value; OnPropertyChanged(nameof(VoterChat)); }
    }

    [ForeignKey(nameof(VoterChat))]
    public virtual Guid? VoterChatID
    {
        get => _voterChatID;
        set { OnPropertyChanging(nameof(VoterChatID)); _voterChatID = value; OnPropertyChanged(nameof(VoterChatID)); }
    }
}
