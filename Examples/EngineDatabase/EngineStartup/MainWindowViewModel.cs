using EngineUIComponents.ViewModels;

namespace EngineStartup
{
    public class MainWindowViewModel : Observable
    {
        public MainWindowViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }
    }
}
