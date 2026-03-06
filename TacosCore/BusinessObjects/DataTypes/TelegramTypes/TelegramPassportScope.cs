
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[Authorize]
[DefaultClassOptions]
public partial class TelegramPassportScope : BaseObject, INotifyPropertyChanging, INotifyPropertyChanged
{

    private int _v = 1;

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected void OnPropertyChanging(string propertyName) =>
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));




    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual IList<TelegramPassportScopeElement>? Data { get; set; } = new ObservableCollection<TelegramPassportScopeElement>();


    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public virtual int V
    {
        get => _v;
        set { OnPropertyChanging(nameof(V)); _v = value; OnPropertyChanged(nameof(V)); }
    }
}
