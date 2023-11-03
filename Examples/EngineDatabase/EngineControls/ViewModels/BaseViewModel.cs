using EngineDataAccess.Repositories;
using EngineUIComponents.Services;
using Prism.Events;

namespace EngineUIComponents.ViewModels
{
    public abstract class BaseViewModel : Observable
    {
        private protected readonly IEventAggregator _eventAggregator;
        private protected readonly IMessageDialogService _messageDialogService;
        private protected readonly IRepositories _repositories;

        public BaseViewModel(IRepositories repositories, IMessageDialogService messageDialogService, IEventAggregator eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            _repositories = repositories;
        }


        public virtual string Title => string.Empty; 
    }
}
