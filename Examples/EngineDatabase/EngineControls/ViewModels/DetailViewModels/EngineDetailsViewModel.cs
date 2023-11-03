using EngineDataAccess.Repositories;
using EngineModels.EngineModels;
using EngineModels.UserModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.Events;
using EngineUIComponents.ViewModels.Helpers;
using EngineUIComponents.ViewModels.LookupItems;
using EngineUIComponents.ViewModels.Wrappers;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels.DetailViewModels
{
    public class EngineDetailsViewModel : DetailViewModelBase<EngineWrapper, Engine>
    {
        public EngineDetailsViewModel(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repositories, messageDialogService, eventAggregator)
        {
         
        }


        public override string Title => "Engine details";

        public async override Task LoadAsync(int? id)
        {
            Engine engine = !id.HasValue ? CreateNewEngine() : 
                await _repositories.EngineRepository.GetByIdAsync(id.Value);

            await Initialize(engine);
        }
        
        public IEnumerable<UserLookupItem> Users { get; set; }
        public IEnumerable<EngineTypeLookupItem> EngineTypes { get; set; }

        private async Task Initialize(Engine engine)
        {
            Users = await _repositories.UserRepository.GetLookupItems(_repositories.OrderRepository).AsNoTracking().ToListAsync();
            EngineTypes = await _repositories.EngineTypeRepository.GetLookupItems().AsNoTracking().ToListAsync();

            var m = new EngineWrapper(engine);

            UpdateSelectedItems(m);


            m.PropertyChanged += (s, e) =>
             {
                if (!HasChanges)
                {
                    HasChanges = _repositories.EngineRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Model.IsChanged) || e.PropertyName == nameof(Model.IsValid))
                {
                    InvalidateCommands();
                }
             };

            Model = m;

            Model.Validate();
            InvalidateCommands();
        }

        private void UpdateSelectedItems(EngineWrapper wrapper)
        {
            var engType = EngineTypes.FirstOrDefault(x => x.Id == wrapper.Model.EngineTypeId);
            if (wrapper.SelectedEngineType != engType)
                wrapper.SelectedEngineType = engType;

            var selUser = Users.FirstOrDefault(x => x.Id == wrapper.Model.UserId);
            if (wrapper.SelectedUser != selUser)
                wrapper.SelectedUser = selUser;
        }

        private Engine CreateNewEngine()
        {
            var engine = new Engine();
            _repositories.EngineRepository.Add(engine);
            return engine;
        }

        private async protected override Task OnDeleteExecuteAsync()
        {
            if (ShowDeleteDialog() == MessageDialogResult.OK)
            {
                _repositories.EngineRepository.Remove(Model.Model);
                await _repositories.EngineRepository.SaveAsync();
                RaiseDetailDeletedEvent(Model.Id);
            }
        }

        private protected async override Task OnSaveExecuteAsync()
        {
            if (ShowSaveDialog() == MessageDialogResult.OK)
            {
                try
                {
                    await _repositories.EngineRepository.SaveAsync();
                    if (Model.Model.User == null)
                        await LoadAsync(Model.Id);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    await ProcessConcurrencyIssue(ex);
                }

                Model.AcceptChanges();

                _eventAggregator.GetEvent<AfterDetailSavedEvent<Engine>>().Publish(new AfterDetailSavedEventArgs<Engine>(Model.Model));
                InvalidateCommands();
            }
        }

        private protected override void OnResetExecute()
        {
            base.OnResetExecute();
            UpdateSelectedItems(Model);
            Model.Validate();
        }

        private protected override void UpdateCommands()
        {
            base.UpdateCommands();
            var onDeleteCanExecute = Model.WhenAnyValue(x => x.Id, x => x > 0);
            DeleteCommand = ReactiveCommand.CreateFromTask(OnDeleteExecuteAsync, onDeleteCanExecute);
        }
    }
}
