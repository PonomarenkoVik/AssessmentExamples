namespace EngineUIComponents.ViewModels.LookupItems
{
    public class UserLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrdersCount { get; set; }
        public int OpenOrdersCount => OrdersCount - ReadyOrdersCount;
        public int ReadyOrdersCount { get; set; }
        public int EnginesCount { get; set; }

        public bool IsVisibleByFilter(string filterText)
        {
            return string.IsNullOrEmpty(filterText) ||
                    Name.Contains(filterText) ||
                    OpenOrdersCount.ToString().Contains(filterText) ||
                    ReadyOrdersCount.ToString().Contains(filterText) ||
                    EnginesCount.ToString().Contains(filterText);
        }
    }
}
