using EngineDataAccess.Repositories;
using EngineModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.Events;
using EngineUIComponents.ViewModels.Wrappers;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EngineUIComponents.ViewModels.DetailViewModels
{
    public abstract class DetailViewModelBase<T, TItem> : BaseViewModel, IDetailViewModel where T : ModelWrapper<TItem> where TItem : IEntity
    {
        private bool _hasChanges;
        private T _model;
        private ReactiveCommand<Unit, Unit> _deleteCommand;
        private ReactiveCommand<Unit, Unit> _saveCommand;

        protected DetailViewModelBase(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repositories, messageDialogService, eventAggregator)
        {
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
        }

        public T Model
        {
            get { return _model; }
            private protected set
            {
                _model = value;
                UpdateCommands();
                OnPropertyChanged();
            }
        }

        private protected virtual void UpdateCommands()
        {
            var onSaveCanExecute = Model.WhenAnyValue(x => x.IsChanged, y => y.HasErrors, (isChanged, hasArrors) => isChanged && !hasArrors);

            SaveCommand = ReactiveCommand.CreateFromTask(OnSaveExecuteAsync, onSaveCanExecute);
            SaveCommand.CanExecute.Catch(onSaveCanExecute);
        }

        public abstract Task LoadAsync(int? id);

        public ICommand ResetCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> SaveCommand
        {
            get => _saveCommand;
            private protected set
            {
                _saveCommand = value;
                OnPropertyChanged();
            }
        }

        public ReactiveCommand<Unit, Unit> DeleteCommand
        {
            get => _deleteCommand;
            private protected set
            {
                _deleteCommand = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    //SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

       

        private protected abstract Task OnDeleteExecuteAsync();

        private protected virtual bool OnSaveCanExecute()
        {
            return Model != null && !Model.HasErrors && HasChanges;
        }

        private protected abstract Task OnSaveExecuteAsync();

        private protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            _eventAggregator.GetEvent<AfterDetailDeletedEvent<TItem>>().Publish(new AfterDetailDeletedEventArgs<TItem>(Model.Model));
        }

        private protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            _eventAggregator.GetEvent<AfterDetailSavedEvent<TItem>>().Publish(new AfterDetailSavedEventArgs<TItem>(Model.Model));
        }

        private protected void InvalidateCommands()
        {
            ((DelegateCommand)ResetCommand).RaiseCanExecuteChanged();
           CommandManager.InvalidateRequerySuggested();
        }

        private protected virtual void OnResetExecute()
        {
            Model.RejectChanges();
        }

        private protected MessageDialogResult ShowSaveDialog()
        {
            return _messageDialogService.ShowOkCancelDialog($"Do you want to save {typeof(TItem).Name}", $"Save {typeof(TItem).Name}");
        }

        private protected MessageDialogResult ShowDeleteDialog()
        {
            return _messageDialogService.ShowOkCancelDialog($"Do you want to delete the {typeof(TItem).Name} '{Model.Model.Id}'", $"Delete {typeof(TItem).Name}");
        }

        private protected MessageDialogResult ShowConcurrencyIssueDialog()
        {
            return _messageDialogService.ShowOkCancelDialog($"The {typeof(TItem).Name} has been changed in the meantime by someone else. Click OK to save your changes anyway, click Cancel to reload the {typeof(TItem).Name} from the database.\"", "Error");
        }

        private protected async Task ProcessConcurrencyIssue(DbUpdateConcurrencyException ex)
        {
            if (ShowConcurrencyIssueDialog() == MessageDialogResult.OK)
            {
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues()); ;
                await _repositories.EngineRepository.SaveAsync();
            }
            else
            {
                await ex.Entries.Single().ReloadAsync();
                await LoadAsync(Model.Id); ;
            }
        }

        private bool OnResetCanExecute()
        {
            CommandManager.InvalidateRequerySuggested();
            return Model.IsChanged;
        }
    }
}
