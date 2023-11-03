using DynamicData;
using EngineDataAccess.Repositories;
using EngineModels.EngineModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.DetailViewModels;
using EngineUIComponents.ViewModels.Events;
using EngineUIComponents.ViewModels.Helpers;
using EngineUIComponents.ViewModels.LookupItems;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels
{
    public class EnginesViewModel : ElementTabViewModel<EngineLookupItem>
    {
        private readonly Func<IRepositories> _repoFunc;

        public EnginesViewModel(Func<IRepositories> repoFunc, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repoFunc(), messageDialogService, eventAggregator)
        {
            AddNewEngineCommand = ReactiveCommand.CreateFromTask(OnAddNewEngine);
            AddNewEngineCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            EditEngineCommand = ReactiveCommand.CreateFromTask<int>(OnEditEngine);
            EditEngineCommand.ThrownExceptions.Subscribe(x => _messageDialogService.ShowErrorDialog(x));

            _eventAggregator.GetEvent<AfterDetailSavedEvent<Engine>>().Subscribe(OnEngineSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent<Engine>>().Subscribe(OnEngineDeleted);
            this._repoFunc = repoFunc;
        }

        private void OnEngineSaved(AfterDetailSavedEventArgs<Engine> args)
        {
            var item = Items?.FirstOrDefault(x => x.Id == args.Model.Id);

            if (item != null)
            {
                Items?.Replace(item, args.Model.ToLookupItem());
            }
            else
            {
                if (Items == null)
                    Items = new ObservableCollection<EngineLookupItem>();

                Items.Add(args.Model.ToLookupItem());
            }
        }

        private void OnEngineDeleted(AfterDetailDeletedEventArgs<Engine> args)
        {
            var item = Items?.FirstOrDefault(x => x.Id == args.Model.Id);

            if (item != null)
                Items?.Remove(item);
        }

        public override string Title => "Engines";

        public ReactiveCommand<Unit, Unit> AddNewEngineCommand { get; }
        public ReactiveCommand<int, Unit> EditEngineCommand { get; }

        private async Task OnEditEngine(int id)
        {
            var viewModel = new EngineDetailsViewModel(_repoFunc(), _messageDialogService, _eventAggregator);
            await viewModel.LoadAsync(id);
            OnAddDetailView(viewModel);

        }

        private async Task OnAddNewEngine()
        {
            var viewModel = new EngineDetailsViewModel(_repoFunc(), _messageDialogService, _eventAggregator);
            await viewModel.LoadAsync(null);
            OnAddDetailView(viewModel);
        }

        public override async Task LoadAsync()
        {
            var items = await _repositories.EngineRepository.GetLookupItems().AsNoTracking().ToListAsync();
            Items = new ObservableCollection<EngineLookupItem>(items);
        }

        private void OnAddDetailView(EngineDetailsViewModel viewModel)
        {
            _eventAggregator.GetEvent<AddDetailsViewEvent>().Publish(new AddDetailsViewEventArgs(viewModel));
        }
    }
}
