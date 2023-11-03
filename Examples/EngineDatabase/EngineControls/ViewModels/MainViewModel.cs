using DynamicData;
using EngineDataAccess;
using EngineDataAccess.Repositories;
using EngineModels.EngineModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.DetailViewModels;
using EngineUIComponents.ViewModels.Events;
using Prism.Events;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object? _selectedElement;
        private readonly Func<IRepositories> _repoFunc;

        public MainViewModel(Func<IRepositories> repoFunc, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repoFunc(), messageDialogService, eventAggregator)
        {
            ElementViewModels = new ObservableCollection<object>();

            _eventAggregator.GetEvent<AddDetailsViewEvent>().Subscribe(OnAddDetailsView);

            EnginesCommand = ReactiveCommand.CreateFromTask(OnEnginesCommand);
            EnginesCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            OrdersCommand = ReactiveCommand.CreateFromTask(OnOrdersOpen);
            OrdersCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            EngineTypesCommand = ReactiveCommand.CreateFromTask(OnEngineTypesOpen);
            EngineTypesCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            UsersCommand = ReactiveCommand.CreateFromTask(OnUsersOpen);
            UsersCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            CloseTabCommand = ReactiveCommand.Create<object>(OnCloseTab);
            CloseTabCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));
            this._repoFunc = repoFunc;

            _eventAggregator.GetEvent<AfterDetailDeletedEvent<Engine>>().Subscribe(OnEngineDeleted);
        }

        private void OnEngineDeleted(AfterDetailDeletedEventArgs<Engine> args)
        {
            var engineViewsToDelete = ElementViewModels.OfType<EngineDetailsViewModel>().Where(x => x.Model.Id == args.Model.Id);
            if (engineViewsToDelete.Any())
            {
                ElementViewModels.RemoveMany(engineViewsToDelete);
            }
        }

        private void OnAddDetailsView(AddDetailsViewEventArgs args)
        {
            Add(args.ViewModel);
        }

        public ReactiveCommand<Unit, bool> EnginesCommand { get; }
        public ReactiveCommand<Unit, bool> OrdersCommand { get; }
        public ReactiveCommand<Unit, bool> EngineTypesCommand { get; }
        public ReactiveCommand<Unit, bool> UsersCommand { get; }

        public ReactiveCommand<object, Unit> CloseTabCommand { get; }

        public object? SelectedElement
        {
            get => _selectedElement;
            set
            {
                if (_selectedElement != value)
                {
                    _selectedElement = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<object> ElementViewModels { get; }

        private async Task<bool> AddTab(IElementViewModel viewModel)
        {
            Add(viewModel);
            await viewModel.LoadAsync();
            return true;
        }

        private void Add(object viewModel)
        {
            ElementViewModels.Add(viewModel);
            SelectedElement = viewModel;
        }

        private void OnCloseTab(object obj)
        {
            ElementViewModels.Remove(obj);
        }

        private async Task<bool> OnEnginesCommand()
        {
            var viewModel = new EnginesViewModel(_repoFunc, _messageDialogService, _eventAggregator);
            return await AddTab(viewModel);
        }

        private async Task<bool> OnOrdersOpen()
        {
            var viewModel = new OrdersViewModel(_repoFunc, _messageDialogService, _eventAggregator);
            return await AddTab(viewModel);
        }

        private async Task<bool> OnEngineTypesOpen()
        {
            var viewModel = new EngineTypesViewModel(_repoFunc, _messageDialogService, _eventAggregator);
            return await AddTab(viewModel);
        }

        private async Task<bool> OnUsersOpen()
        {
            var viewModel = new UsersViewModel(_repoFunc, _messageDialogService, _eventAggregator);
            return await AddTab(viewModel);
        }

    }
}
