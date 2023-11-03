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
    public class UsersViewModel : ElementTabViewModel<UserLookupItem>
    {
        private readonly Func<IRepositories> _repoFunc;

        public UsersViewModel(Func<IRepositories> repoFunc, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repoFunc(), messageDialogService, eventAggregator)
        {
            this._repoFunc = repoFunc;
        }

        public override string Title => "Orders";

        public override async Task LoadAsync()
        {
            var items = await _repositories.UserRepository.GetLookupItems(_repositories.OrderRepository).AsNoTracking().ToListAsync();
            Items = new ObservableCollection<UserLookupItem>(items);
        }
    }
}
