using EngineDataAccess;
using EngineDataAccess.Repositories;
using EngineModels.OrderModels;
using EngineUIComponents.Services;
using EngineUIComponents.ViewModels.Wrappers;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels.DetailViewModels
{
    public class OrderDetailsViewModel : DetailViewModelBase<OrderWrapper, Order>
    {
        public OrderDetailsViewModel(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator) : base(repositories, messageDialogService, eventAggregator)
        {

        }

        public override string Title => base.Title;

        public override Task LoadAsync(int? id)
        {
            throw new System.NotImplementedException();
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
