//-----------------------------------------------------------------------
// <copyright file="TelegramReactionCount.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
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
public partial class TelegramReactionCount : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{
    private int _totalCount;

    private TelegramReactionType _type = null!;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [InverseProperty(nameof(TelegramMessageReactionCountUpdated.Reactions))]
    public virtual IList<TelegramMessageReactionCountUpdated>? MessageReactionCountUpdatedThisReactionCountBelongsTo { get; set; } = new ObservableCollection<TelegramMessageReactionCountUpdated>();




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int TotalCount
    {
        get => _totalCount;
        set
        {
            OnPropertyChanging(nameof(TotalCount));
            _totalCount = value;
            OnPropertyChanged(nameof(TotalCount));
        }
    }




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [InverseProperty(nameof(TelegramReactionType.ReactionCountThisReactionTypeBelongsTo))]
    public virtual TelegramReactionType Type
    {
        get => _type;
        set
        {
            OnPropertyChanging(nameof(Type));
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    [ForeignKey(nameof(Type))]
    public virtual Guid? TypeID { get; set; }
}