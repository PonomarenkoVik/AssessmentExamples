using EngineDataAccess.Repositories;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.Helpers;
using EngineUIComponents.ViewModels.LookupItems;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels
{
    public class EngineTypesViewModel : ElementTabViewModel<EngineTypeLookupItem>
    {
        private readonly Func<IRepositories> _repoFunc;

        public EngineTypesViewModel(Func<IRepositories> repoFunc, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repoFunc(), messageDialogService, eventAggregator)
        {
            this._repoFunc = repoFunc;
        }

        public override string Title => "Engine Types";

        public override async Task LoadAsync()
        {
            var items = await _repositories.EngineTypeRepository.GetLookupItems().AsNoTracking().ToListAsync();
            Items = new ObservableCollection<EngineTypeLookupItem>(items);
        }
    }
}
