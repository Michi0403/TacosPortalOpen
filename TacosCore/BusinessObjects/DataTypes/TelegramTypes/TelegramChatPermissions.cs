using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;

[Authorize]
[DefaultClassOptions]
public partial class TelegramChatPermissions : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private bool? _canSendMessages;
    private bool? _canSendAudios;
    private bool? _canSendDocuments;
    private bool? _canSendPhotos;
    private bool? _canSendVideos;
    private bool? _canSendVideoNotes;
    private bool? _canSendVoiceNotes;
    private bool? _canSendPolls;
    private bool? _canSendOtherMessages;
    private bool? _canAddWebPagePreviews;
    private bool? _canChangeInfo;
    private bool? _canInviteUsers;
    private bool? _canPinMessages;
    private bool? _canManageTopics;

    public virtual bool? CanSendMessages
    {
        get => _canSendMessages;
        set { OnPropertyChanging(nameof(CanSendMessages)); _canSendMessages = value; OnPropertyChanged(nameof(CanSendMessages)); }
    }

    public virtual bool? CanSendAudios
    {
        get => _canSendAudios;
        set { OnPropertyChanging(nameof(CanSendAudios)); _canSendAudios = value; OnPropertyChanged(nameof(CanSendAudios)); }
    }

    public virtual bool? CanSendDocuments
    {
        get => _canSendDocuments;
        set { OnPropertyChanging(nameof(CanSendDocuments)); _canSendDocuments = value; OnPropertyChanged(nameof(CanSendDocuments)); }
    }

    public virtual bool? CanSendPhotos
    {
        get => _canSendPhotos;
        set { OnPropertyChanging(nameof(CanSendPhotos)); _canSendPhotos = value; OnPropertyChanged(nameof(CanSendPhotos)); }
    }

    public virtual bool? CanSendVideos
    {
        get => _canSendVideos;
        set { OnPropertyChanging(nameof(CanSendVideos)); _canSendVideos = value; OnPropertyChanged(nameof(CanSendVideos)); }
    }

    public virtual bool? CanSendVideoNotes
    {
        get => _canSendVideoNotes;
        set { OnPropertyChanging(nameof(CanSendVideoNotes)); _canSendVideoNotes = value; OnPropertyChanged(nameof(CanSendVideoNotes)); }
    }

    public virtual bool? CanSendVoiceNotes
    {
        get => _canSendVoiceNotes;
        set { OnPropertyChanging(nameof(CanSendVoiceNotes)); _canSendVoiceNotes = value; OnPropertyChanged(nameof(CanSendVoiceNotes)); }
    }

    public virtual bool? CanSendPolls
    {
        get => _canSendPolls;
        set { OnPropertyChanging(nameof(CanSendPolls)); _canSendPolls = value; OnPropertyChanged(nameof(CanSendPolls)); }
    }

    public virtual bool? CanSendOtherMessages
    {
        get => _canSendOtherMessages;
        set { OnPropertyChanging(nameof(CanSendOtherMessages)); _canSendOtherMessages = value; OnPropertyChanged(nameof(CanSendOtherMessages)); }
    }

    public virtual bool? CanAddWebPagePreviews
    {
        get => _canAddWebPagePreviews;
        set { OnPropertyChanging(nameof(CanAddWebPagePreviews)); _canAddWebPagePreviews = value; OnPropertyChanged(nameof(CanAddWebPagePreviews)); }
    }

    public virtual bool? CanChangeInfo
    {
        get => _canChangeInfo;
        set { OnPropertyChanging(nameof(CanChangeInfo)); _canChangeInfo = value; OnPropertyChanged(nameof(CanChangeInfo)); }
    }

    public virtual bool? CanInviteUsers
    {
        get => _canInviteUsers;
        set { OnPropertyChanging(nameof(CanInviteUsers)); _canInviteUsers = value; OnPropertyChanged(nameof(CanInviteUsers)); }
    }

    public virtual bool? CanPinMessages
    {
        get => _canPinMessages;
        set { OnPropertyChanging(nameof(CanPinMessages)); _canPinMessages = value; OnPropertyChanged(nameof(CanPinMessages)); }
    }

    public virtual bool? CanManageTopics
    {
        get => _canManageTopics;
        set { OnPropertyChanging(nameof(CanManageTopics)); _canManageTopics = value; OnPropertyChanged(nameof(CanManageTopics)); }
    }
}
