using System.ComponentModel.DataAnnotations;

namespace EngineModels.OrderModels
{
    public enum OrderState
    {
        Created,
        Queued,
        InWork,
        Repaired,
        Disposed
    }
}
