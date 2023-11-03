using EngineDataAccess.Repositories;
using EngineModels.UserModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.Wrappers;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels.DetailViewModels
{
    public class UserDetailsModelView : DetailViewModelBase<UserWrapper, User>
    {
        public UserDetailsModelView(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repositories, messageDialogService, eventAggregator)
        {
        }

        public override Task LoadAsync(int? id)
        {
            throw new NotImplementedException();
        }

        private protected async override Task OnDeleteExecuteAsync()
        {
            throw new NotImplementedException();
        }

        private protected async override Task OnSaveExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
