using Prism.Events;

namespace EngineUIComponents.ViewModels.Events
{
    public class AfterDetailSavedEvent<T> : PubSubEvent<AfterDetailSavedEventArgs<T>>
    {
    }

    public class AfterDetailSavedEventArgs<T>
    {
        public T Model { get; set; }

        public AfterDetailSavedEventArgs(T model)
        {
            Model = model;
        }
    }
}
