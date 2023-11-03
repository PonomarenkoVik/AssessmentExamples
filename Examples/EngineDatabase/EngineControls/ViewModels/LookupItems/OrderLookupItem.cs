using EngineModels.OrderModels;

namespace EngineUIComponents.ViewModels.LookupItems
{
    public class OrderLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public int EngineId { get; set; }
        public string Creator { get; set; }
        public string EngineOwner { get; set; }
        public string EngineDisplayType { get; set; }
        public RepairType RepairType { get; set; }
        public string Description { get; set; }
        public OrderState State { get; set; }

        public bool IsVisibleByFilter(string filterText)
        {
            return string.IsNullOrEmpty(filterText) ||
                    Id.ToString().Contains(filterText) ||
                    EngineId.ToString().Contains(filterText) ||
                    EngineDisplayType.Contains(filterText) ||
                    Creator.Contains(filterText) ||
                    EngineOwner.Contains(filterText) ||
                    RepairType.ToString().Contains(filterText) ||
                    State.ToString().Contains(filterText);
        }
    }
}
