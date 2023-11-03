using EngineUIComponents.ViewModels.LookupItems;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels
{
    public interface IElementViewModel<T> : IElementViewModel where T : ILookupItem
    {
        ObservableCollection<T>? Items { get; }
    }

    public interface IElementViewModel
    {
        string Title { get; }
        string FilterText { set; }
        Task LoadAsync();
    }
}
