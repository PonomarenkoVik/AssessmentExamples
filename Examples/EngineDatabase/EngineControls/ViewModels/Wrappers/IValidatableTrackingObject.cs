using System.ComponentModel;

namespace EngineUIComponents.ViewModels.Wrappers
{
    public interface IValidatableTrackingObject :
    IRevertibleChangeTracking,
    INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}
