namespace EngineUIComponents.ViewModels.LookupItems
{
    public class EngineLookupItem : ILookupItem
    {
        public int Id { get; set; }

        public string FabricNumber { get; set; }

        public string EngineType { get; set; }

        public string Owner { get; set; }

        public int NumberOfOrders { get; set; }

        public bool IsVisibleByFilter(string filterText)
        {
            return string.IsNullOrEmpty(filterText) || Id.ToString().Contains(filterText) ||
                          EngineType.Contains(filterText) ||
                          FabricNumber.ToString().Contains(filterText) ||
                          Owner.Contains(filterText);
        }
    }
}
