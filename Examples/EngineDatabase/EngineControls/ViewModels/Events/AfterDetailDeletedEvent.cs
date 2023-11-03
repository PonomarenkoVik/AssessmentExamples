using Prism.Events;

namespace EngineUIComponents.ViewModels.Events
{
    public class AfterDetailDeletedEvent<T> : PubSubEvent<AfterDetailDeletedEventArgs<T>>
    {
    }
    public class AfterDetailDeletedEventArgs<T>
    {
        public T Model { get; set; }

        public AfterDetailDeletedEventArgs(T model)
        {
            Model = model;
        }
    }
}
