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
public partial class TelegramVideoChatParticipantsInvited : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private IList<TelegramUser>? _users = new ObservableCollection<TelegramUser>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramUser>? Users
    {
        get => _users;
        set
        {
            if (!ReferenceEquals(_users, value))
            {
                OnPropertyChanging(nameof(Users));
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
    }

    private IList<TelegramMessage>? _messageThisVideoChatParticipantsInvitedBelongsTo = new ObservableCollection<TelegramMessage>();

    [InverseProperty(nameof(TelegramMessage.VideoChatParticipantsInvited))]
    [JsonIgnore]
    public virtual IList<TelegramMessage>? MessageThisVideoChatParticipantsInvitedBelongsTo
    {
        get => _messageThisVideoChatParticipantsInvitedBelongsTo;
        set
        {
            if (!ReferenceEquals(_messageThisVideoChatParticipantsInvitedBelongsTo, value))
            {
                OnPropertyChanging(nameof(MessageThisVideoChatParticipantsInvitedBelongsTo));
                _messageThisVideoChatParticipantsInvitedBelongsTo = value;
                OnPropertyChanged(nameof(MessageThisVideoChatParticipantsInvitedBelongsTo));
            }
        }
    }
}