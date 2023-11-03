namespace EngineUIComponents.ViewModels.LookupItems
{
    public interface ILookupItem
    {
        bool IsVisibleByFilter(string filterText);
    }
}
