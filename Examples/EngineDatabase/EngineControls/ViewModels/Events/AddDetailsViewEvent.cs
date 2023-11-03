using EngineUIComponents.ViewModels.DetailViewModels;
using Prism.Events;

namespace EngineUIComponents.ViewModels.Events
{
    public class AddDetailsViewEvent : PubSubEvent<AddDetailsViewEventArgs>
    {
    }
    public class AddDetailsViewEventArgs
    {
        public IDetailViewModel ViewModel { get; set; }

        public AddDetailsViewEventArgs(IDetailViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
