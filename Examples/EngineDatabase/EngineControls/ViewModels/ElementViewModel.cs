using EngineDataAccess.Repositories;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.LookupItems;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EngineUIComponents.ViewModels
{
    public abstract class ElementTabViewModel<T> : BaseViewModel, IElementViewModel<T> where T : ILookupItem
    {
        private ObservableCollection<T> _items;
        private string _filterText;

        public ElementTabViewModel(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repositories, messageDialogService, eventAggregator)
        {

        }

        public abstract Task LoadAsync();

        public ObservableCollection<T>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public string FilterText
        {
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    UpdateFilter();
                }
            }
        }

        private void UpdateFilter()
        {
            var collView = CollectionViewSource.GetDefaultView(Items);

            if (collView == null)
                return;

            if (string.IsNullOrEmpty(_filterText))
            {
                collView.Filter = null;
                return;
            }
            collView.Filter = x =>
            {
                var engine = x as ILookupItem;
                return engine?.IsVisibleByFilter(_filterText) ?? true;
            };
        }
    }
}
