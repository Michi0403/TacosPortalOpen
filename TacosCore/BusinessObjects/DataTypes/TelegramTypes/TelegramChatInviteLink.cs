//-----------------------------------------------------------------------
// <copyright file="TelegramChatInviteLink.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramChatInviteLink : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private bool _createsJoinRequest;
    private TelegramUser _creator = null!;
    private Guid? _creatorID;
    private DateTime? _expireDate;

    private string _inviteLink = string.Empty;
    private bool _isPrimary;
    private bool _isRevoked;
    private int? _memberLimit;
    private string _name = string.Empty;
    private int? _pendingJoinRequestCount;
    private int? _subscriptionPeriod;
    private int? _subscriptionPrice;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    [InverseProperty(nameof(TelegramChatJoinRequest.InviteLink))]
    public virtual IList<TelegramChatJoinRequest>? ChatJoinRequestThisChatInviteLinkBelongsTo { get; set; } = new ObservableCollection<TelegramChatJoinRequest>();

    [InverseProperty(nameof(TelegramChatMemberUpdated.InviteLink))]
    public virtual IList<TelegramChatMemberUpdated>? ChatMemberUpdatedThisChatInviteLinkBelongsTo { get; set; } = new ObservableCollection<TelegramChatMemberUpdated>();

    public virtual bool CreatesJoinRequest
    {
        get => _createsJoinRequest;
        set { OnPropertyChanging(nameof(CreatesJoinRequest)); _createsJoinRequest = value; OnPropertyChanged(nameof(CreatesJoinRequest)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramUser.ChatInviteLinkThisUserBelongsTo))]
    public virtual TelegramUser Creator
    {
        get => _creator;
        set { OnPropertyChanging(nameof(Creator)); _creator = value; OnPropertyChanged(nameof(Creator)); }
    }

    [ForeignKey("Creator")]
    public virtual Guid? CreatorID
    {
        get => _creatorID;
        set { OnPropertyChanging(nameof(CreatorID)); _creatorID = value; OnPropertyChanged(nameof(CreatorID)); }
    }

    public virtual DateTime? ExpireDate
    {
        get => _expireDate;
        set { OnPropertyChanging(nameof(ExpireDate)); _expireDate = value; OnPropertyChanged(nameof(ExpireDate)); }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual string InviteLink
    {
        get => _inviteLink;
        set { OnPropertyChanging(nameof(InviteLink)); _inviteLink = value; OnPropertyChanged(nameof(InviteLink)); }
    }

    public virtual bool IsPrimary
    {
        get => _isPrimary;
        set { OnPropertyChanging(nameof(IsPrimary)); _isPrimary = value; OnPropertyChanged(nameof(IsPrimary)); }
    }

    public virtual bool IsRevoked
    {
        get => _isRevoked;
        set { OnPropertyChanging(nameof(IsRevoked)); _isRevoked = value; OnPropertyChanged(nameof(IsRevoked)); }
    }

    public virtual int? MemberLimit
    {
        get => _memberLimit;
        set { OnPropertyChanging(nameof(MemberLimit)); _memberLimit = value; OnPropertyChanged(nameof(MemberLimit)); }
    }

    public virtual string Name
    {
        get => _name;
        set { OnPropertyChanging(nameof(Name)); _name = value; OnPropertyChanged(nameof(Name)); }
    }

    public virtual int? PendingJoinRequestCount
    {
        get => _pendingJoinRequestCount;
        set { OnPropertyChanging(nameof(PendingJoinRequestCount)); _pendingJoinRequestCount = value; OnPropertyChanged(nameof(PendingJoinRequestCount)); }
    }

    public virtual int? SubscriptionPeriod
    {
        get => _subscriptionPeriod;
        set { OnPropertyChanging(nameof(SubscriptionPeriod)); _subscriptionPeriod = value; OnPropertyChanged(nameof(SubscriptionPeriod)); }
    }

    public virtual int? SubscriptionPrice
    {
        get => _subscriptionPrice;
        set { OnPropertyChanging(nameof(SubscriptionPrice)); _subscriptionPrice = value; OnPropertyChanged(nameof(SubscriptionPrice)); }
    }
}
