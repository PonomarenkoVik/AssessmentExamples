using System.Threading.Tasks;

namespace EngineUIComponents.ViewModels.DetailViewModels
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int? id);
        bool HasChanges { get; }
    }
}
