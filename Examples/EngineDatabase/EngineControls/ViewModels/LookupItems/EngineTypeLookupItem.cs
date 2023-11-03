namespace EngineUIComponents.ViewModels.LookupItems
{
    public class EngineTypeLookupItem : ILookupItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float EnginePower { get; set; }
        public int EngineSpeed { get; set; }
        public float? Weight { get; set; }
        public float? Height { get; set; }
        public float? Width { get; set; }
        public string FullType => $"{Type} {EnginePower} kWt {EngineSpeed} 1/s";
        public bool IsVisibleByFilter(string filterText)
        {
            return string.IsNullOrEmpty(Type) || EnginePower.ToString().Contains(filterText) ||
                          EngineSpeed.ToString().Contains(filterText);
        }
    }
}
